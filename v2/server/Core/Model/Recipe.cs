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
    public Ingredient[] Ingredients { get; set; } = [];
    public Nutrient[] TotalNutrients { get; set; } = [];
}