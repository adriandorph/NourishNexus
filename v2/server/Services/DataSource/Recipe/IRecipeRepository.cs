namespace server.Services.DataSource;

public interface IRecipeRepository
{
    Task<RecipeModel> CreateRecipe(RecipeModel recipe);
    Task UpdateRecipe(RecipeModel recipe);
    Task DeleteRecipe(string recipeId);
    Task<RecipeModel> GetRecipeById(string recipeId);
    Task<List<RecipeModel>> GetAllRecipes();
    Task<List<RecipeModel>> GetRecipesByAuthorId(string authorId);
    Task<List<RecipeModel>> GetRecipedByIds(List<string> recipeIds);
}
