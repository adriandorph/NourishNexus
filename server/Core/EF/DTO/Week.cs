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
    float Calories,
    List<FoodItemDTO> foodItems,
    List<RecipeDTO> recipes
);