namespace server.Core.Services.RecipeManagement.DTOs
{
    public record RecipeUpdateDTO(
        [Required] string Id,
        [Required] string Title,
        string? Description,
        string? Steps,
        [Required] string Accessibility,
        [Required] Ingredient[] Ingredients
    );
}