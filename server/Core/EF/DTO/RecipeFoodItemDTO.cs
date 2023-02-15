
//Recipe FoodItem relation. Many to Many

public record RecipeFoodItemDTO(
    int Id,
    int RecipeId,
    int FoodItemId
);

public record RecipeFoodItemCreateDTO
{
    public int RecipeId {get; set;}
    public int FoodItemId {get; set;}
}

public record RecipeFoodItemUpdateDTO : RecipeFoodItemCreateDTO
{
    public int Id {get; set;}
}