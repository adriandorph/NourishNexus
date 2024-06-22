namespace server.Core.Services.RecipeManagement.DTOs;
public record UpdateRecipeImageDTO(
    string RecipeId,
    string ImageBase64
);