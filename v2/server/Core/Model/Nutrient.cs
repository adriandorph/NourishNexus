namespace server.Core.Model;
public class Nutrient
{
    public NutrientType NutrientType { get; set; }
    public float Amount { get; set; }
    public Unit Unit { get; set; }
}