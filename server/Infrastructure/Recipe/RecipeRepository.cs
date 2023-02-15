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
        if (author == null) return (Response.NotFound, new RecipeDTO(-1, recipe.Title, recipe.IsPublic ?? false, recipe.Description, recipe.Method, recipe.AuthorId));

        var entity = new Recipe
        (
            recipe.Title,
            recipe.IsPublic ?? false,
            recipe.Description,
            recipe.Method,
            author
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
        

}
