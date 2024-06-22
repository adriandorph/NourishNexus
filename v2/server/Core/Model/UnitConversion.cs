namespace server.Core.Model;

/// <summary>
/// Conversion multiplier from grams to a different unit.
/// </summary>
public class UnitConversion
{
    public Unit Unit { get; set; }
    public float Multiplier { get; set; }
}
