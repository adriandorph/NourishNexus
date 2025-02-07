using server.Core.Services.RecipeManagement;
using server.Core.Infrastructure.DataBase;
using server.Core.Services.RecipeManagement.DTOs;

namespace server.Services.RecipeManagement;

public class RecipeService(
    IRecipeRepository recipeRepository,
    IImageRepository imageRepository) : IRecipeService
{
    private readonly IRecipeRepository _recipeRepository = recipeRepository;
    private readonly IImageRepository _imageRepository = imageRepository;

    public async Task<Recipe?> CreateRecipeAsync(RecipeCreateDTO recipeCreateDTO)
    {
        var recipe = new Recipe
        {
            Title = recipeCreateDTO.Title,
            Fork = recipeCreateDTO.Fork,
            AuthorId = recipeCreateDTO.AuthorId,
            Description = recipeCreateDTO.Description,

            Steps = recipeCreateDTO.Steps,
            Accessibility = recipeCreateDTO.Accessibility.ToAccessibility(),
            Ingredients = recipeCreateDTO.Ingredients?.Select(Ingredient.FromDTO).ToList() ?? [],
        };

        
        var createdRecipe = await _recipeRepository.CreateRecipe(recipe);
        if (createdRecipe == null) return null;
        if (recipeCreateDTO.ImageBase64 == null) return createdRecipe;

        var createdImage = await CreateImage(recipeCreateDTO.ImageBase64, createdRecipe);
        return await _recipeRepository.GetRecipeById(createdRecipe.Id);
    }

    public async Task<Recipe?> UpdateRecipeAsync(RecipeUpdateDTO recipeUpdateDTO)
    {
        var recipe = await _recipeRepository.GetRecipeById(recipeUpdateDTO.Id);
        if (recipe == null) return null;

        recipe.Title = recipeUpdateDTO.Title;
        recipe.Description = recipeUpdateDTO.Description;
        recipe.Steps = recipeUpdateDTO.Steps;
        recipe.Accessibility = recipeUpdateDTO.Accessibility.ToAccessibility();
        recipe.Ingredients = recipeUpdateDTO.Ingredients.Select(Ingredient.FromDTO).ToList();

        if (recipeUpdateDTO.ImageBase64 == null && recipe.ImageId != null)
        {
            await _imageRepository.DeleteImageAsync(recipe.ImageId);
            recipe.ImageId = null;
        }
        else if (recipeUpdateDTO.ImageBase64 != null)
        {
            var image = await UpdateImageAsync(new UpdateRecipeImageDTO(
                recipe.Id,
                recipeUpdateDTO.ImageBase64
            ));
            recipe.ImageId = image?.Id;
        }

        return await _recipeRepository.UpdateRecipe(recipe);
    }

    public async Task<Recipe?> GetRecipeAsync(string recipeId)
    => await _recipeRepository.GetRecipeById(recipeId);
    

    public async Task<Image?> UpdateImageAsync(UpdateRecipeImageDTO updateRecipeImageDTO)
    {
        var recipe = await _recipeRepository.GetRecipeById(updateRecipeImageDTO.RecipeId);
        if (recipe == null) return null;

        if (recipe.ImageId != null)
        {
            //Update image
            var existingImage = await _imageRepository.GetImageByIdAsync(recipe.ImageId);
            if (existingImage == null) return await CreateImage(updateRecipeImageDTO.ImageBase64, recipe);
            else if (existingImage.ImageBase64 == updateRecipeImageDTO.ImageBase64) 
                return existingImage;

            existingImage.ImageBase64 = updateRecipeImageDTO.ImageBase64;
            return await _imageRepository.UpdateImageAsync(existingImage);
        }

        return await CreateImage(updateRecipeImageDTO.ImageBase64, recipe);
    }

    public async Task<bool> DeleteAsync(string recipeId)
    {
        var recipe = await _recipeRepository.GetRecipeById(recipeId);
        if (recipe == null) return false;


        //TODO: Remove all references to the recipe from other collections
        if (recipe.ImageId != null && !await _imageRepository.DeleteImageAsync(recipe.ImageId)) 
            return false;

        return await _recipeRepository.DeleteRecipe(recipe.Id);
    }

    public async Task<bool> DeleteImageAsync(string recipeId)
    {
        //TODO: Add authorization check so only the user can delete their profile picture
        var recipe = await _recipeRepository.GetRecipeById(recipeId);
        if (recipe == null || recipe.ImageId == null) return false;

        return await _imageRepository.DeleteImageAsync(recipe.ImageId);
    }

    private async Task<Image?> CreateImage(string imageBase64, Recipe recipe)
    {
        var image = new Image { ImageBase64 = imageBase64 };

        var createdImage = await _imageRepository.CreateImageAsync(image);
        if (createdImage == null || createdImage.Id == null) return null;

        recipe.ImageId = createdImage.Id;
        var updatedRecipe = await _recipeRepository.UpdateRecipe(recipe);

        if (updatedRecipe == null) {
            await _imageRepository.DeleteImageAsync(createdImage.Id);
            return null;
        }

        return createdImage;
    }
}