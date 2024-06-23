namespace server.Core.Services.SavedRecipes
{
    public interface ISavedRecipeService
    {
        Task<List<Recipe>?> SaveRecipeAsync(string recipeId, string userId);
        Task<List<Recipe>?> GetSavedRecipesAsync(string userId);
        Task<List<Recipe>?> UnsaveRecipeAsync(string userId, string recipeId);
    }
}