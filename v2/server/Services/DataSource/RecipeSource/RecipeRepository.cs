namespace server.Services.DataSource.RecipeSource;

public class RecipeRepository(IMongoDatabase mongoDB) : IRecipeRepository
{
    private readonly IMongoDatabase _mongoDB = mongoDB;

    public async Task<Recipe> CreateRecipe(Recipe recipe)
    {
        var collection = _mongoDB.GetCollection<Recipe>("Recipes");
        await collection.InsertOneAsync(recipe);

        var filter = Builders<Recipe>.Filter.Eq("Id", recipe.Id);
        return await collection.Find(filter).FirstOrDefaultAsync();
    }

    public async Task UpdateRecipe(Recipe recipe)
    {
        var collection = _mongoDB.GetCollection<Recipe>("Recipes");
        var filter = Builders<Recipe>.Filter.Eq("Id", recipe.Id);
        await collection.ReplaceOneAsync(filter, recipe);
    }

    public async Task DeleteRecipe(string recipeId)
    {
        var collection = _mongoDB.GetCollection<Recipe>("Recipes");
        var filter = Builders<Recipe>.Filter.Eq("Id", recipeId);
        await collection.DeleteOneAsync(filter);
    }

    public async Task<Recipe> GetRecipeById(string recipeId)
    {
        var collection = _mongoDB.GetCollection<Recipe>("Recipes");
        var filter = Builders<Recipe>.Filter.Eq("Id", recipeId);
        return await collection.Find(filter).FirstOrDefaultAsync();
    }

    public async Task<List<Recipe>> GetAllRecipes()
    {
        var collection = _mongoDB.GetCollection<Recipe>("Recipes");
        return await collection.Find(_ => true).ToListAsync();
    }

    public async Task<List<Recipe>> GetRecipesByAuthorId(string authorId)
    {
        var collection = _mongoDB.GetCollection<Recipe>("Recipes");
        var filter = Builders<Recipe>.Filter.Eq("AuthorId", authorId);
        return await collection.Find(filter).ToListAsync();
    }

    public async Task<List<Recipe>> GetRecipedByIds(List<string> recipeIds) {
        var collection = _mongoDB.GetCollection<Recipe>("Recipes");
        var filter = Builders<Recipe>.Filter.In("Id", recipeIds);
        return await collection.Find(filter).ToListAsync();
    }
}