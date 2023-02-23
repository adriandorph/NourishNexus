namespace server.Infrastructure;

public class FoodItemRecipe
{
    public int Id {get; set;}
    public float Amount {get; set;}
    public FoodItem FoodItem {get; set;}
    public Recipe Recipe {get; set;}


    public FoodItemRecipe(FoodItem fooditem, Recipe recipe, float amount){
        this.FoodItem = fooditem;
        this.Recipe = recipe;
        this.Amount = amount;
    }

    
    #nullable disable
    public FoodItemRecipe(){}

    public FoodItemRecipeDTO ToDTO()
        => new FoodItemRecipeDTO(this.Id, this.Amount, this.FoodItem.Id, this.Recipe.Id);

}