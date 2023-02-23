namespace server.Infrastructure;

public class Meal //NOT DONE
{
    public int Id {get; set;}
    public MealType MealType {get; set;} // Breakfast, Lunch, Dinner, Snack
    public User User {get; set;}
    public DateTime Date {get; set;}
    public List<Category> Categories {get; set;}

    public Meal(MealType mealType, User user, DateTime date, List<Category> categories)
    {
        this.MealType = mealType;
        this.User = user;
        this.Date = date;
        this.Categories = categories;
    }

    #nullable disable
    public Meal() {}

    public MealDTO ToDTO()
        => new MealDTO(this.Id, this.MealType, this.User.Id, this.Date, this.Categories.Select(c => c.Id).ToList());
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