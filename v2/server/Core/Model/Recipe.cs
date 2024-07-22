namespace server.Core.Model;

public class Recipe {
    [BsonId]
    [BsonRepresentation(BsonType.ObjectId)]
    public string Id { get; set; } = "";
    public string? AuthorId { get; set; }
    public Fork? Fork { get; set; }
    public string Title { get; set; } = "";
    public string? Description { get; set; }
    public string? ImageId { get; set; }
    public string? Steps { get; set; }
    public Accessibility Accessibility { get; set; }
    public List<Ingredient> Ingredients { get; set; } = [];
    public List<Nutrient> TotalNutrients { get; set; } = [];

    public RecipeDTO ToDTO()
    => new (
        Id,
        AuthorId,
        Fork,
        Title,
        Description,
        ImageId,
        Steps,
        Accessibility.GetDescription(),
        Ingredients.Select(ingredient => ingredient.ToDTO()).ToList(),
        TotalNutrients.Select(nutrient => nutrient.ToDTO()).ToList()
    );

    public static Recipe FromDTO(RecipeDTO dto)
    => new()
    {
        Id = dto.Id,
        AuthorId = dto.AuthorId,
        Fork = dto.Fork,
        Title = dto.Title,
        Description = dto.Description,
        ImageId = dto.ImageId,
        Steps = dto.Steps,
        Accessibility = dto.Accessibility.ToAccessibility(),
        Ingredients = dto.Ingredients.Select(Ingredient.FromDTO).ToList(),
        TotalNutrients = dto.TotalNutrients.Select(Nutrient.FromDTO).ToList()
    };
}

public record RecipeDTO(
    [Required] string Id,
    [Required] string? AuthorId,
    Fork? Fork,
    [Required] string Title,
    string? Description,
    string? ImageId,
    string? Steps,
    [Required] string Accessibility,
    [Required] List<IngredientDTO> Ingredients,
    [Required] List<NutrientDTO> TotalNutrients
);