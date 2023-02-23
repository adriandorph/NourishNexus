namespace server.Infrastructure;

public class FoodItemMeal
{
    public int Id {get; set;}
    public float Amount {get; set;}
    public FoodItem FoodItem {get; set;}
    public Meal Meal {get; set;}


    public FoodItemMeal(FoodItem fooditem, Meal meal, float amount){
        this.FoodItem = fooditem;
        this.Meal = meal;
        this.Amount = amount;
    }

    
    #nullable disable
    public FoodItemMeal(){}

    public FoodItemMealDTO ToDTO()
        => new FoodItemMealDTO(this.Id, this.Amount, this.FoodItem.Id, this.Meal.Id);

}