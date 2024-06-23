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
        return await _recipeRepo.GetRecipedByIds(recipeIds);
    }

    public async Task<List<Recipe>?> SaveRecipeAsync(string recipeId, string userId)
    {
        var user = await _userRepo.GetUserByIdAsync(userId);
        if (user == null) return null;

        var recipe = await _recipeRepo.GetRecipeById(recipeId);
        if (recipe == null) return null;
        
        if (user.SavedRecipeIds.Contains(recipeId))
            return await _recipeRepo.GetRecipedByIds(user.SavedRecipeIds);

        user.SavedRecipeIds.Add(recipeId);
        var updatedUser = await _userRepo.UpdateUser(user);
        if (updatedUser == null) return null;
        
        return await _recipeRepo.GetRecipedByIds(updatedUser.SavedRecipeIds);
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

        var savedRecipes = await _recipeRepo.GetRecipedByIds(updatedUser.SavedRecipeIds);
        return savedRecipes;
    }
}
