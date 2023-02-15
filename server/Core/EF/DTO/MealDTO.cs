namespace server.Core.EF.DTO;

public record MealDTO(
    int Id,
    int mealType,
    int UserId,
    DateOnly Date
);

//MealFoodItem relation

public record MealFoodItemDTO(
    int Id,
    int MealId,
    int FoodItemId
);

public record MealFoodItemCreateDTO 
{
    public int MealId {get; set;}
    public int FoodItemId {get; set;}
}

public record MealFoodItemUpdateDTO : MealFoodItemCreateDTO 
{
    public int Id {get; set;}
}