namespace server.Infrastructure;
public class RecipeFoodItem
{
    public int Id {get; set;}
    public Recipe recipe {get; set;}
    public FoodItem foodItem {get; set;}
    
    public RecipeFoodItem(Recipe recipe, FoodItem foodItem){
        this.recipe = recipe;
        this.foodItem = foodItem;
    }
}