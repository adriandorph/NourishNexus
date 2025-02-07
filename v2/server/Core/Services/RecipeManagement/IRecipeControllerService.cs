using server.Core.Services.RecipeManagement.DTOs;

namespace server.Core.Services.RecipeManagement
{
    public interface IRecipeControllerService
    {
        Task<RecipeDTO?> CreateRecipeAsync(RecipeCreateDTO recipeCreateDTO);
        Task<RecipeDTO?> GetRecipeAsync(string recipeId);
        Task<RecipeDTO?> UpdateRecipeAsync(RecipeUpdateDTO recipeUpdateDTO);
        Task<bool> DeleteRecipeAsync(string recipeId);
    }
}