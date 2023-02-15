namespace server.Infrastructure;

public class RecipeRepository : IRecipeRepository
{
    private readonly NourishNexusContext _context;

    public RecipeRepository(NourishNexusContext context)
    {
        _context = context;
    }

    public async Task<(Response, RecipeDTO)> CreateAsync(RecipeCreateDTO recipe)
    {
        var conflict = await _context.Recipes
        .Where(r => r.Title == recipe.Title)
        .Select(r => r.ToDTO())
        .FirstOrDefaultAsync();

        if( conflict != null) return (Response.Conflict, conflict);

        var author = await _context.Users.Where(u => u.Id == recipe.authorId).FirstOrDefaultAsync();
        if (author == null) return (Response.NotFound, new RecipeDTO(-1, recipe.Title, recipe.isPublic ?? false, recipe.Description, recipe.Method, recipe.authorId));

        var entity = new Recipe
        (
            recipe.Title,
            recipe.isPublic ?? false,
            recipe.Description,
            recipe.Method,
            author
        );

        _context.Recipes.Add(entity);

        await _context.SaveChangesAsync();

        return (Response.Created, entity.ToDTO());

    
    }

    public async Task<IReadOnlyCollection<RecipeDTO>> ReadAllAsync()
    {
         return(await _context.Recipes
                        .Select(r => new RecipeDTO(r.Id, r.Title, r.IsPublic, r.Description, r.Method, r.Author.Id))
                        .ToListAsync())
                        .AsReadOnly();
    }

    public async Task<IReadOnlyCollection<RecipeDTO>> ReadAllByAuthorIDAsync(int authorID)
    {
        return await ( _context.Recipes
                        .Where(r => r.Author.Id == authorID)
                        .Select(r => new RecipeDTO(r.Id, r.Title, r.IsPublic, r.Description, r.Method, r.Author.Id))
                        .ToListAsync());
    }

    public async Task<Option<RecipeDTO>> ReadByIDAsync(int recipeID)
    {
        var recipes = from r in _context.Recipes
                        where r.Id == recipeID
                        select new RecipeDTO(r.Id, r.Title, r.IsPublic, r.Description, r.Method, r.Author.Id);
        return await recipes.FirstOrDefaultAsync();
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

    public async Task<Response> UpdateAsync(RecipeUpdateDTO recipe)
    {
        var recipeEntity = await _context.Recipes
            .Where (r => r.Id == recipe.Id)
            .FirstOrDefaultAsync();
        
        if(recipeEntity == null) return Response.NotFound;

        if(recipeEntity.Title != null && !recipeEntity.Title.Equals(recipe.Title) && recipe.Title != null){
            recipeEntity.Title = recipe.Title;
        }

        if(recipeEntity.Description != null && !recipeEntity.Description.Equals(recipe.Description) && recipe.Description != null){
            recipeEntity.Description = recipe.Description;
        }

        if(recipeEntity.Method != null && !recipeEntity.Method.Equals(recipe.Method) && recipe.Method != null){
            recipeEntity.Method = recipe.Method;
        }

        if(recipe.isPublic != null && recipeEntity.IsPublic != recipe.isPublic){
            recipeEntity.IsPublic = (bool) recipe.isPublic;
        }

        await _context.SaveChangesAsync();

        return Response.Updated;
    }
}
