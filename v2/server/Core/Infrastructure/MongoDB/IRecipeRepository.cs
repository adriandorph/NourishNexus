namespace server.Core.Infrastructure.MongoDB;

public interface IRecipeRepository
{
    Task<Recipe> CreateRecipe(Recipe recipe);
    Task UpdateRecipe(Recipe recipe);
    Task DeleteRecipe(string recipeId);
    Task<Recipe> GetRecipeById(string recipeId);
    Task<List<Recipe>> GetAllRecipes();
    Task<List<Recipe>> GetRecipesByAuthorId(string authorId);
    Task<List<Recipe>> GetRecipedByIds(List<string> recipeIds);
}
