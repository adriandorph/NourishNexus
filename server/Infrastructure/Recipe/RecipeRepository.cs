namespace server.Infrastructure;

public class RecipeRepository : IRecipeRepository
{
    private readonly NourishNexusContext _context;

    public RecipeRepository(NourishNexusContext context)
    {
        _context = context;
    }

    public async Task<(Response, RecipeDTO)> CreateAsync(RecipeCreateDTO recipe) //Conflict, NotFound, Created
    {
        var conflict = await _context.Recipes
        .Where(r => r.Title == recipe.Title && r.AuthorId == recipe.AuthorId)
        .Include(r => r.Categories)
        .Select(r => r.ToDTO())
        .FirstOrDefaultAsync();

        if(conflict != null) return (Response.Conflict, conflict);

        var author = await _context.Users.Where(u => u.Id == recipe.AuthorId).FirstOrDefaultAsync();
        if (author == null) return (Response.NotFound, new RecipeDTO(-1, recipe.Title ?? "", recipe.IsPublic ?? false, recipe.Description ?? "", recipe.Method ?? "", recipe.AuthorId, recipe.CategoryIDs ?? new List<int>(), true, true, true, true));


        var entity = new Recipe
        (
            recipe.Title ?? "",
            recipe.IsPublic ?? false,
            recipe.Description ?? "",
            recipe.Method ?? "",
            recipe.AuthorId,
            recipe.CategoryIDs != null ? await CategoryIDsToCategories(recipe.CategoryIDs) : new List<Category>(),
            true,
            true,
            true,
            true
        );

        _context.Recipes.Add(entity);

        await _context.SaveChangesAsync();

        return (Response.Created, entity.ToDTO());
    
    }

    public async Task<Response> UpdateAsync(RecipeUpdateDTO recipe)
    {
        var recipeEntity = await _context.Recipes
            .Where (r => r.Id == recipe.Id)
            .Include(r => r.Categories)
            .FirstOrDefaultAsync();
        
        if(recipeEntity == null) return Response.NotFound;
        
        if(_context.Recipes.Any(r => r.Title.Equals(recipe.Title) && r.AuthorId == recipeEntity.AuthorId))
            return Response.Conflict;

        if(recipeEntity.Title != null && !recipeEntity.Title.Equals(recipe.Title) && recipe.Title != null){
            recipeEntity.Title = recipe.Title;
        }

        if(recipeEntity.Description != null && !recipeEntity.Description.Equals(recipe.Description) && recipe.Description != null){
            recipeEntity.Description = recipe.Description;
        }

        if(recipeEntity.Method != null && !recipeEntity.Method.Equals(recipe.Method) && recipe.Method != null){
            recipeEntity.Method = recipe.Method;
        }

        if(recipe.IsPublic != null && recipeEntity.IsPublic != recipe.IsPublic){
            recipeEntity.IsPublic = recipe.IsPublic ?? false;
        }

        if(recipe.CategoryIDs != null)
        {
            var categories = await CategoryIDsToCategories(recipe.CategoryIDs);
            recipeEntity.Categories = categories;

            _context.UpdateRange(recipeEntity.Categories);
        }

        if(recipe.IsBreakfast != null) recipeEntity.IsBreakfast = recipe.IsBreakfast ?? true;
        if(recipe.IsLunch != null) recipeEntity.IsLunch = recipe.IsLunch ?? true;
        if(recipe.IsDinner != null) recipeEntity.IsDinner = recipe.IsDinner ?? true;
        if(recipe.IsSnack != null) recipeEntity.IsSnack = recipe.IsSnack ?? true;

        if(recipe.FoodItemRecipes != null){
            //Delete all foodItemRecipes that are linked to this recipe
            foreach(var fir in await _context.FoodItemRecipes.Where(f => f.Recipe.Id == recipe.Id).ToListAsync())
            {
                _context.FoodItemRecipes.Remove(fir);
            }
            //Create the foodItemRecipes
            foreach(var firCreateDTO in recipe.FoodItemRecipes)
            {
                var foodItem = await _context.FoodItems.Where(fir => fir.Id == firCreateDTO.FoodItemID).FirstOrDefaultAsync();
                if (foodItem == null) return Response.NotFound;
                var firEntity = new FoodItemRecipe(foodItem, recipeEntity, firCreateDTO.Amount);
                await _context.FoodItemRecipes.AddAsync(firEntity);
            }
        }
        
        await _context.SaveChangesAsync();

        return Response.Updated;
    }

    public async Task<Response> RemoveAsync(int id)
    {
        var recipeEntity = await _context.Recipes
            .Where(r => r.Id == id)
            .FirstOrDefaultAsync();
        
        if(recipeEntity == null)
        {
            return Response.NotFound;
        }

        foreach(var fir in await _context.FoodItemRecipes.Where(f => f.Recipe.Id == recipeEntity.Id).ToListAsync())
        {
            _context.FoodItemRecipes.Remove(fir);
        }

        _context.Recipes.Remove(recipeEntity);
        await _context.SaveChangesAsync();

        return Response.Deleted;
    }


    public async Task<IReadOnlyCollection<RecipeDTO>> ReadAllAsync()
    {
         return(await _context.Recipes
                        .Include(r => r.Categories)
                        .Select(r => r.ToDTO())
                        .ToListAsync())
                        .AsReadOnly();
    }

    public async Task<IReadOnlyCollection<RecipeDTO>> ReadAllPublicAsync()
    {
         return(await _context.Recipes
                        .Where(r => r.IsPublic)
                        .Include(r => r.Categories)
                        .Select(r => r.ToDTO())
                        .ToListAsync())
                        .AsReadOnly();
    }

    public async Task<IReadOnlyCollection<RecipeDTO>> ReadAllByAuthorIDAsync(int authorID)
    {
        return await ( _context.Recipes
                        .Where(r => r.AuthorId == authorID)
                        .Include(r => r.Categories)
                        .Select(r => r.ToDTO())
                        .ToListAsync());
    }

    public async Task<Option<RecipeDTO>> ReadByIDAsync(int recipeID)
    {
        return await _context.Recipes
            .Where(r => r.Id == recipeID)
            .Include(r => r.Categories)
            .Select(r => r.ToDTO())
            .FirstOrDefaultAsync();
    }

    public async Task<Option<RecipeDTO>> ReadByAuthorIDAndTitle(int authorID, string title)
        => await _context.Recipes
                .Where(r => r.AuthorId == authorID && r.Title == title)
                .Include(r => r.Categories)
                .Select(r => r.ToDTO())
                .FirstOrDefaultAsync();

    public async Task<IReadOnlyCollection<RecipeDTO>> ReadAllByCategoryIDAsync(int categoryID)
        => await _context.Recipes
            .Include(r => r.Categories)
            .Where(r => r.Categories.Any(c => c.Id == categoryID))
            .Select(r => r.ToDTO())
            .ToListAsync();

    
    public async Task<IReadOnlyCollection<RecipeAmountDTO>> ReadAllByMealId(int mealID)
        => (await _context.RecipeMeals
            .Include(r => r.Meal)
            .Where(rm => rm.Meal.Id == mealID)
            .Select(rm => new Tuple<float, int>(rm.Amount, rm.Recipe.Id))
            .ToListAsync())
            .Join(_context.Recipes, i => i.Item2, r => r.Id, (item, recipe) => new RecipeAmountDTO(item.Item1, recipe.ToDTO()))
            .ToList();

            

    //Helper methods

    private async Task<List<Category>> CategoryIDsToCategories(List<int> categoryIDs)
        => await _context.Categories
            .Where(c => categoryIDs.Any(cID => cID == c.Id))
            .ToListAsync();

    
}
