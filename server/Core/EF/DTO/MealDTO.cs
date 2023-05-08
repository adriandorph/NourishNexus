namespace server.Core.EF.DTO;

public record MealDTO
(
    int Id,
    MealType MealType,
    int UserID,
    DateTime Date,
    List<int> CategoryIDs
);

public record MealCreateDTO
{
    public MealType? MealType {get; set;}
    public int? UserID {get; set;}
    public DateTime? Date {get; set;}
    public List<int>? CategoryIDs {get; set;}
}

public record MealUpdateDTO : MealCreateDTO
{
    public int Id {get; set;}
    public List<FoodItemMealCreateDTO>? FoodItemMeals {get; set;}
    public List<RecipeMealCreateDTO>? RecipeMeals {get; set;}
}

public record MealReportDTO : MealCreateDTO
{
    public List<FoodItemMealCreateDTO>? FoodItemMeals {get; set;}
    public List<RecipeMealCreateDTO>? RecipeMeals {get; set;}
}

public record MealWithFoodDTO
(
    MealDTO Meal,
    List<FoodItemAmountDTO> FoodItems, 
    List<RecipeAmountDTO> Recipes
);

public record MealWithAllFoodDTO
(
    MealDTO Meal,
    List<FoodItemAmountDTO> FoodItems, 
    List<RecipeAmountWithFoodItemsDTO> Recipes
);
