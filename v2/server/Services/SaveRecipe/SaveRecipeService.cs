using server.Core.Infrastructure.DataBase;
using server.Core.Services.SavedRecipes;

namespace server.Services.SaveRecipe;

public class SaveRecipeService(
    IUserRepository userRepo,
    IRecipeRepository recipeRepo) : ISavedRecipeService
{
    private readonly IUserRepository _userRepo = userRepo;
    private readonly IRecipeRepository _recipeRepo = recipeRepo;

    public async Task<List<Recipe>?> GetSavedRecipesAsync(string userId)
    {
        var user = await _userRepo.GetUserByIdAsync(userId);
        if (user == null) return null;

        var recipeIds = user.SavedRecipeIds;

        return await GetSavedAndAuthoredRecipes(userId, recipeIds);
    }

    public async Task<List<Recipe>?> SaveRecipeAsync(string recipeId, string userId)
    {
        var user = await _userRepo.GetUserByIdAsync(userId);
        if (user == null) return null;

        var recipe = await _recipeRepo.GetRecipeById(recipeId);
        if (recipe == null) return null;

        var authoredRecipes = await _recipeRepo.GetRecipesByAuthorId(userId);

        if (authoredRecipes.Exists(ar => ar.AuthorId == userId) || user.SavedRecipeIds.Contains(recipeId))
        {
            var savedRecipes = await _recipeRepo.GetRecipeByIds(user.SavedRecipeIds);
            return [.. savedRecipes, .. authoredRecipes];
        }

        user.SavedRecipeIds.Add(recipeId);
        var updatedUser = await _userRepo.UpdateUser(user);
        if (updatedUser == null) return null;
        
        return await GetSavedAndAuthoredRecipes(userId, updatedUser.SavedRecipeIds);
    }

    public async Task<List<Recipe>?> UnsaveRecipeAsync(string userId, string recipeId)
    {
        var user = await _userRepo.GetUserByIdAsync(userId);
        if (user == null) return null;

        var recipe = await _recipeRepo.GetRecipeById(recipeId);
        if (recipe == null) return null;

        user.SavedRecipeIds.Remove(recipeId);

        var updatedUser = await _userRepo.UpdateUser(user);
        if (updatedUser == null) return null;

        return await GetSavedAndAuthoredRecipes(userId, updatedUser.SavedRecipeIds);
    }

    private async Task<List<Recipe>> GetSavedAndAuthoredRecipes(string userId, List<string> recipeIds)
    {
        var savedRecipes = await _recipeRepo.GetRecipeByIds(recipeIds);
        var authoredRecipes = await _recipeRepo.GetRecipesByAuthorId(userId);
        return [.. savedRecipes, .. authoredRecipes];
    }
}
