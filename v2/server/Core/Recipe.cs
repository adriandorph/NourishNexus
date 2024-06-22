using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace server.Core;

public class Recipe {
    [BsonId]
    [BsonRepresentation(BsonType.ObjectId)]
    public string Id { get; set; } = "";
    public string? AuthorId { get; set; }
    public Fork? Fork { get; set; }
    public string Title { get; set; } = "";
    public string? Description { get; set; }
    public string? ImageBase64 { get; set; }
    public string? Steps { get; set; }
    public Ingredient[] Ingredients { get; set; } = [];
    public Nutrient[] TotalNutrients { get; set; } = [];
}