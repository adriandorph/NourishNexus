namespace server.Core;
public class Ingredient
{
    public string Name { get; set; } = "Ingredient Name";
    public float? Quantity { get; set; }
    public Unit? Unit { get; set; }
    public bool HasNutrition { get {return Nutrients.Length > 0;} }

    /// <summary>
    /// Nutrients are in total per the specified Quantity and Unit.
    /// </summary>
    public Nutrient[] Nutrients { get; set; } = [];

    public Ingredient() {}
    public Ingredient(string name, float? quantity, Unit? unit, Nutrient[] nutrients) {
        Name = name;
        Quantity = quantity;
        Unit = unit;
        Nutrients = nutrients;
    }
}