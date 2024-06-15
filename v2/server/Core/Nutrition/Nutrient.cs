namespace server.Core.Nutrition;

using server.Core.Enums;
public class Nutrient
{
    public NutrientType NutrientType { get; set; }
    public float Amount { get; set; }
    public Unit Unit { get; set; }
}