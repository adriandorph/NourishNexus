using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using server.Core;

namespace server.Services.DataSource.FoodItem;

public class FoodItemModel
{
    [BsonId]
    [BsonRepresentation(BsonType.ObjectId)]
    public string Id { get; set; } = "";
    public string Name { get; set; } = "";
    public bool Verified { get; set; } = false;

    /// <summary>
    /// Source of the food item. I.e. User, Frida, USDA, etc.
    /// </summary>
    public Source Source { get; set; }
    public string? AuthorId { get; set; }
    public string? Description { get; set; }
    public bool HasNutrition { get { return Nutrients.Length > 0; } }
    
    /// <summary>
    /// Nutrients are per gram of the food item.
    /// </summary>
    public Nutrient[] Nutrients { get; set; } = [];

    /// <summary>
    /// Unit conversions are from grams to a different unit.
    /// </summary>
    public UnitConversion[] UnitConversions { get; set; } = [];
}