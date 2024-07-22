namespace server.Core.Infrastructure.DataBase;

public interface IRecipeRepository
{
    Task<Recipe?> CreateRecipe(Recipe recipe);
    Task<Recipe?> UpdateRecipe(Recipe recipe);
    Task<bool> DeleteRecipe(string recipeId);
    Task<Recipe?> GetRecipeById(string recipeId);
    Task<List<Recipe>> GetAllRecipes();
    Task<List<Recipe>> GetRecipesByAuthorId(string authorId);
    Task<List<Recipe>> GetRecipeByIds(List<string> recipeIds);
}
