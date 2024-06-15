using server.Core.Enums;

namespace server.Core.FoodItem;

/// <summary>
/// Conversion multiplier from grams to a different unit.
/// </summary>
public class UnitConversion
{
    public Unit Unit { get; set; }
    public float Multiplier { get; set; }
}
