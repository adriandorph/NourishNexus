namespace server.Core.EF.DTO;

public record Week
(
    Day Monday,
    Day Tuesday,
    Day Wednesday,
    Day Thursday,
    Day Friday,
    Day Saturday,
    Day Sunday
);

public record Day
(
    float Calories,
    Meal Breakfast,
    Meal Lunch,
    Meal Dinner,
    Meal Snacks
);

public record Meal
(
    int? Id,
    float Calories,
    List<FoodItemAmountDTO> FoodItems,
    List<RecipeCalories> Recipes
);

public record RecipeCalories(
    RecipeDTO Recipe,
    float Calories,
    float Amount
);