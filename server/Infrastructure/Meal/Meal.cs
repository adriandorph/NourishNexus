namespace server.Infrastructure;

public class Meal //NOT DONE
{
    public int Id {get; set;}
    public int mealType {get; set;} // Breakfast, Lunch, Dinner, Snack
    public User User {get; set;}
    public DateOnly Date {get; set;}

    public Meal(int mealType, User user, DateOnly date)
    {
        this.mealType = mealType;
        this.User = user;
        this.Date = date;
    }

    #nullable disable
    public Meal() {}
    public MealDTO ToDTO()
        => new MealDTO(Id, mealType, User.Id, Date);
}


//Meal FoodItem relation
public class MealFoodItem 
{
    public int Id {get; set;}
    public Meal Meal {get; set;}
    public FoodItem FoodItem {get; set;}

    public MealFoodItem(Meal meal, FoodItem foodItem)
    {
        this.Meal = meal;
        this.FoodItem = foodItem;
    }
}