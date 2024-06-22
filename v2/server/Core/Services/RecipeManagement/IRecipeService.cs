using server.Core.Services.RecipeManagement.DTOs;

namespace server.Core.Services.RecipeManagement;

public interface IRecipeService
{
    Task<Recipe?> CreateRecipeAsync(RecipeCreateDTO recipeCreateDTO);
    Task<Recipe?> UpdateRecipeAsync(RecipeUpdateDTO recipeUpdateDTO);
    Task<Recipe?> GetRecipeAsync(string recipeId);
    Task<bool> DeleteAsync(string recipeId);
    Task<Image?> UpdateImageAsync(UpdateRecipeImageDTO updateRecipeImageDTO);
    Task<bool> DeleteImageAsync(string recipeId);
}