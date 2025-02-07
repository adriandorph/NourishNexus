
using server.Core.Infrastructure.DataBase;
using server.Core.Services.RecipeManagement;
using server.Core.Services.RecipeManagement.DTOs;
using server.Core.Services.UserManagement;

namespace server.Services.RecipeManagement;

public class RecipeAssembler(IUserService userService, IImageRepository imageRepository) : IRecipeAssembler
{
    private readonly IUserService _userService = userService;
    private readonly IImageRepository _imageRepository = imageRepository;

    public async Task<RecipeDTO> AssembleAsync(Recipe recipe)
    {
        //Find user and make the AuthorDTO
        return new RecipeDTO(
            recipe.Id,
            await GetAuthorDTO(recipe.AuthorId),
            recipe.Fork,
            recipe.Title,
            recipe.Description,
            await GetImageBase64(recipe.ImageId),
            recipe.Steps,
            recipe.Accessibility.GetDescription(),
            recipe.Ingredients.Select(ingredient => ingredient.ToDTO()).ToList(),
            recipe.TotalNutrients.Select(nutrient => nutrient.ToDTO()).ToList()
        );
    }

    private async Task<AuthorDTO?> GetAuthorDTO(string? userId)
    {
        if (userId == null) return null;
        var user = await _userService.GetUserByIdAsync(userId);
        if (user == null || user.Id == null) return null;
        return new AuthorDTO(user.Id, user.Nickname ?? "", await GetImageBase64(user.ProfilePictureId));
    }

    private async Task<string?> GetImageBase64(string? imageId)
    {
        if (imageId == null) return null;
        var image = await _imageRepository.GetImageByIdAsync(imageId);
        return image?.ImageBase64;
    }
}