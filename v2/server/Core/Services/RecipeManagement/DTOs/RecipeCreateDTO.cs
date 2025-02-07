namespace server.Core.Services.RecipeManagement.DTOs;
public record RecipeCreateDTO(
    [Required] string Title,
    Fork? Fork,
    [Required] string AuthorId,
    [Required] string Description,
    string? ImageBase64,
    [Required] string Steps,
    [Required] List<IngredientDTO> Ingredients,
    string Accessibility = "Public"
);