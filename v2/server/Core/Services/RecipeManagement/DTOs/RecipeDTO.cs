namespace server.Core.Services.RecipeManagement.DTOs;

public record RecipeDTO(
    [Required] string Id,
    [Required] AuthorDTO? Author,
    Fork? Fork,
    [Required] string Title,
    string? Description,
    string? ImageBase64,
    string? Steps,
    [Required] string Accessibility,
    [Required] List<IngredientDTO> Ingredients,
    [Required] List<NutrientDTO> TotalNutrients
);