namespace server.Core.Services.RecipeManagement.DTOs
{
    public record RecipeUpdateDTO(
        [Required] string Id,
        [Required] string Title,
        string Description,
        string Steps,
        string? ImageBase64,
        [Required] string Accessibility,
        [Required] List<IngredientDTO> Ingredients
    );
}