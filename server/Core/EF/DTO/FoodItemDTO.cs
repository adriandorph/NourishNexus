namespace server.Core.EF.DTO;

public record FoodItemDTO(
    int Id,
    string Name,
    float Calories,
    float Protein,
    float Carbohydrates,
    float Sugars,
    float Fibres,
    float TotalFat,
    float SaturatedFat,
    float MonounsaturatedFat,
    float PolyunsaturatedFat,
    float TransFat,
    float VitaminA,
    float VitaminB6,
    float VitaminB12,
    float VitaminC,
    float VitaminD,
    float VitaminE,
    float VitaminK1,
    float Thiamin,
    float Riboflavin,
    float Niacin,
    float Folate,
    float Salt,
    float Potassium,
    float Magnesium,
    float Iron,
    float Zinc,
    float Phosphorus,
    float Copper,
    float Iodine,
    float Nickel,
    float Selenium,
    float Calcium
);

public record FoodItemCreateDTO
{
    public string? Name {get; set;}
    public float? Calories {get; set;}
    public float? Protein {get; set;}
    public float? Carbohydrates {get; set;}
    public float? Sugars {get; set;}
    public float? Fibres {get; set;}
    public float? TotalFat {get; set;}
    public float? SaturatedFat {get; set;}
    public float? MonounsaturatedFat {get; set;}
    public float? PolyunsaturatedFat {get; set;}
    public float? TransFat {get; set;}
    public float? VitaminA {get; set;}
    public float? VitaminB6 {get; set;}
    public float? VitaminB12 {get; set;}
    public float? VitaminC {get; set;}
    public float? VitaminD {get; set;}
    public float? VitaminE {get; set;}
    public float? VitaminK1 {get; set;}
    public float? Thiamin {get; set;}
    public float? Riboflavin {get; set;}
    public float? Niacin {get; set;}
    public float? Folate {get; set;}
    public float? Salt {get; set;}
    public float? Potassium {get; set;}
    public float? Magnesium {get; set;}
    public float? Iron {get; set;}
    public float? Zinc {get; set;}
    public float? Phosphorus {get; set;}
    public float? Copper {get; set;}
    public float? Iodine {get; set;}
    public float? Nickel {get; set;}
    public float? Selenium {get; set;}
    public float? Calcium {get; set;}
}

public record FoodItemUpdateDTO : FoodItemCreateDTO
{
    public int Id {get; set;}
}

public record FoodItemMealDTO(int Id, float Amount, int FoodItemID, int MealID);

public record FoodItemMealCreateDTO
{
    public float Amount {get; set;}
    public int FoodItemID {get; set;}
    public int MealID {get; set;}
}

public record FoodItemRecipeDTO(int Id, float Amount, int FoodItemID, int RecipeID);

public record FoodItemRecipeCreateDTO
{
    public float Amount {get; set;}
    public int FoodItemID {get; set;}
    public int RecipeID {get; set;}
}

public record FoodItemAmountDTO
{
    public float Amount {get; set;}
    public FoodItemDTO? FoodItem {get; set;}
}



