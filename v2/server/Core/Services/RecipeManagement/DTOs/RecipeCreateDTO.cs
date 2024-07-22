namespace server.Core.Services.RecipeManagement.DTOs
{
    public record RecipeCreateDTO(
        [Required] string Title,
        [Required] string AuthorId,
        string? Description,
        string? Steps,
        [Required] string Accessibility,
        [Required] List<IngredientDTO> Ingredients
    );
}