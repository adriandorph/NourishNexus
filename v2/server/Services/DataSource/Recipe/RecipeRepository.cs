using MongoDB.Driver;

namespace server.Services.DataSource;

public class RecipeRepository(IMongoDatabase mongoDB) : IRecipeRepository
{
    private readonly IMongoDatabase _mongoDB = mongoDB;

    public async Task<RecipeModel> CreateRecipe(RecipeModel recipe)
    {
        var collection = _mongoDB.GetCollection<RecipeModel>("Recipes");
        await collection.InsertOneAsync(recipe);

        var filter = Builders<RecipeModel>.Filter.Eq("Id", recipe.Id);
        return await collection.Find(filter).FirstOrDefaultAsync();
    }

    public async Task UpdateRecipe(RecipeModel recipe)
    {
        var collection = _mongoDB.GetCollection<RecipeModel>("Recipes");
        var filter = Builders<RecipeModel>.Filter.Eq("Id", recipe.Id);
        await collection.ReplaceOneAsync(filter, recipe);
    }

    public async Task DeleteRecipe(string recipeId)
    {
        var collection = _mongoDB.GetCollection<RecipeModel>("Recipes");
        var filter = Builders<RecipeModel>.Filter.Eq("Id", recipeId);
        await collection.DeleteOneAsync(filter);
    }

    public async Task<RecipeModel> GetRecipeById(string recipeId)
    {
        var collection = _mongoDB.GetCollection<RecipeModel>("Recipes");
        var filter = Builders<RecipeModel>.Filter.Eq("Id", recipeId);
        return await collection.Find(filter).FirstOrDefaultAsync();
    }

    public async Task<List<RecipeModel>> GetAllRecipes()
    {
        var collection = _mongoDB.GetCollection<RecipeModel>("Recipes");
        return await collection.Find(_ => true).ToListAsync();
    }

    public async Task<List<RecipeModel>> GetRecipesByAuthorId(string authorId)
    {
        var collection = _mongoDB.GetCollection<RecipeModel>("Recipes");
        var filter = Builders<RecipeModel>.Filter.Eq("AuthorId", authorId);
        return await collection.Find(filter).ToListAsync();
    }

    public async Task<List<RecipeModel>> GetRecipedByIds(List<string> recipeIds) {
        var collection = _mongoDB.GetCollection<RecipeModel>("Recipes");
        var filter = Builders<RecipeModel>.Filter.In("Id", recipeIds);
        return await collection.Find(filter).ToListAsync();
    }
}