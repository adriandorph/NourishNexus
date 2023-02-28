namespace server.Infrastructure;

public class RecipeMeal
{
    public int Id {get; set;}
    public float Amount {get; set;}
    public Recipe Recipe {get; set;}
    public Meal Meal {get; set;}


    public RecipeMeal(Recipe recipe, Meal meal, float amount){
        this.Recipe = recipe;
        this.Meal = meal;
        this.Amount = amount;
    }

    
    #nullable disable
    public RecipeMeal(){}

    public RecipeMealDTO ToDTO()
        => new RecipeMealDTO(this.Id, this.Amount, this.Recipe.Id, this.Meal.Id);

}