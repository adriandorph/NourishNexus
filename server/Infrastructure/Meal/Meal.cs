namespace server.Infrastructure;

public class Meal
{
    public int Id {get; set;}
    public MealType MealType {get; set;}
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