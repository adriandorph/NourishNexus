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
        .Where(r => r.Title == recipe.Title && r.Author.Id == recipe.AuthorId)
        .Select(r => r.ToDTO())
        .FirstOrDefaultAsync();

        if(conflict != null) return (Response.Conflict, conflict);

        var author = await _context.Users.Where(u => u.Id == recipe.AuthorId).FirstOrDefaultAsync();
        if (author == null) return (Response.NotFound, new RecipeDTO(-1, recipe.Title ?? "", recipe.IsPublic ?? false, recipe.Description ?? "", recipe.Method ?? "", recipe.AuthorId, recipe.CategoryIDs ?? new List<int>(), recipe.FoodItemIDs ?? new List<int>()));



        var entity = new Recipe
        (
            recipe.Title ?? "",
            recipe.IsPublic ?? false,
            recipe.Description ?? "",
            recipe.Method ?? "",
            author,
            recipe.CategoryIDs != null ? await CategoryIDsToCategories(recipe.CategoryIDs) : new List<Category>(),
            recipe.FoodItemIDs != null ? await FoodItemIDsToFoodItems(recipe.FoodItemIDs) : new List<FoodItem>()
        );

        _context.Recipes.Add(entity);

        await _context.SaveChangesAsync();

        return (Response.Created, entity.ToDTO());

    
    }

    public async Task<Response> UpdateAsync(RecipeUpdateDTO recipe)
    {
        var recipeEntity = await _context.Recipes
            .Where (r => r.Id == recipe.Id)
            .FirstOrDefaultAsync();
        
        if(recipeEntity == null) return Response.NotFound;
        
        if(_context.Recipes.Any(r => r.Title.Equals(recipe.Title) && r.Author.Id == recipeEntity.Author.Id))
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
        }

        if(recipe.FoodItemIDs != null){
             var foodItems = await FoodItemIDsToFoodItems(recipe.FoodItemIDs);
             recipeEntity.FoodItems = foodItems;
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

        _context.Recipes.Remove(recipeEntity);
        await _context.SaveChangesAsync();

        return Response.Deleted;
    }


    public async Task<IReadOnlyCollection<RecipeDTO>> ReadAllAsync()
    {
         return(await _context.Recipes
                        .Select(r => r.ToDTO())
                        .ToListAsync())
                        .AsReadOnly();
    }

    public async Task<IReadOnlyCollection<RecipeDTO>> ReadAllByAuthorIDAsync(int authorID)
    {
        return await ( _context.Recipes
                        .Where(r => r.Author.Id == authorID)
                        .Select(r => r.ToDTO())
                        .ToListAsync());
    }

    public async Task<Option<RecipeDTO>> ReadByIDAsync(int recipeID)
    {
        var recipes = from r in _context.Recipes
                        where r.Id == recipeID
                        select r.ToDTO();
        return await recipes.FirstOrDefaultAsync();
    }

    public async Task<Option<RecipeDTO>> ReadByAuthorIDAndTitle(int authorID, string title)
        => await _context.Recipes
                .Where(r => r.Author.Id == authorID && r.Title == title)
                .Select(r => r.ToDTO())
                .FirstOrDefaultAsync();

    public async Task<IReadOnlyCollection<RecipeDTO>> ReadAllByCategoryIDAsync(int categoryID)
    {
        var category = await _context.Categories
            .Where(c => c.Id == categoryID)
            .FirstOrDefaultAsync();
        
        if (category == null) return new List<RecipeDTO>{};
        else return category.Recipes
            .Select(r => r.ToDTO())
            .ToList();
    }

    //Helper methods

    private async Task<List<Category>> CategoryIDsToCategories(List<int> categoryIDs)
        => await _context.Categories
            .Where(c => categoryIDs.Any(cID => cID == c.Id))
            .ToListAsync();

    private async Task<List<FoodItem>> FoodItemIDsToFoodItems(List<int> foodItemIDs)
        => await _context.FoodItems
            .Where(fi => foodItemIDs.Any(fiID => fiID == fi.Id))
            .ToListAsync();
}
