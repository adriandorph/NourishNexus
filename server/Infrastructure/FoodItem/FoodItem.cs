namespace server.Infrastructure;
using server.Core.EF;

public class FoodItem 
{
    public int Id {get; set;}
    public string Name {get; set;}
    public Unit Unit {get; set;}
    public float Calories {get; set;}
    public float Protein {get; set;}
    
    //Add the remaining nutrient data

    public FoodItem(
        string name, 
        Unit unit, 
        float calories,
        float protein
    )
    {
        this.Name = name;
        this.Unit = unit;
        this.Calories = calories;
        this.Protein = protein;
    }
}