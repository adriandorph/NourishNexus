using server.Core.Services.RecipeManagement;
using server.Core.Services.RecipeManagement.DTOs;

namespace server.Services.RecipeManagement;

public class RecipeControllerService(IRecipeService recipeService, IRecipeAssembler recipeAssembler) : IRecipeControllerService
{
    private readonly IRecipeService _recipeService = recipeService;
    private readonly IRecipeAssembler _recipeAssembler = recipeAssembler;

    public async Task<RecipeDTO?> CreateRecipeAsync(RecipeCreateDTO recipeCreateDTO)
    {
        //Save Recipe
        var recipe = await _recipeService.CreateRecipeAsync(recipeCreateDTO);
        return await AssembleOrDefault(recipe);
    }

    public async Task<RecipeDTO?> GetRecipeAsync(string recipeId)
    {
        var recipe = await _recipeService.GetRecipeAsync(recipeId);
        return await AssembleOrDefault(recipe);
    }

    public async Task<RecipeDTO?> UpdateRecipeAsync(RecipeUpdateDTO recipeUpdateDTO)
    {
        var updatedRecipe = await _recipeService.UpdateRecipeAsync(recipeUpdateDTO);
        return await AssembleOrDefault(updatedRecipe);
    }

    public Task<bool> DeleteRecipeAsync(string recipeId)
    {
        return _recipeService.DeleteAsync(recipeId);
    }

    private async Task<RecipeDTO?> AssembleOrDefault(Recipe? recipe)
    {
        if (recipe == null) return null;
        return await _recipeAssembler.AssembleAsync(recipe);
    }
}