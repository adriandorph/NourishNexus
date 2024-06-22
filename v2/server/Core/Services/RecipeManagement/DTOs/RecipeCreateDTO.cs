namespace server.Core.Services.RecipeManagement.DTOs
{
    public record RecipeCreateDTO(
        [Required] string Title,
        string? Description,
        string? Steps,
        [Required] string Accessibility,
        [Required] Ingredient[] Ingredients
    );
}