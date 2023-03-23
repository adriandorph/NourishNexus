using server.Core.Services.MealPlan;

namespace server.Core.Services.MealPlan;

public class PlannedMeal
{
    public int? Id {get; set;}
    public MealType MealType {get; set;}
    public int UserID {get; set;}
    public DateTime Date {get; set;}
    public List<int> CategoryIDs {get; set;}
    public List<FoodItemAmountDTO> FoodItemMeals {get; set;}
    public List<RecipeAmountWithFoodItemsDTO> RecipeMeals {get; set;}

    public PlannedMeal(int? id, MealType mealType, int userID, DateTime date, List<int> categoryIDs, List<FoodItemAmountDTO> foodItems, List<RecipeAmountWithFoodItemsDTO> recipeMeals) {
        this.Id = id;
        this.MealType = mealType;
        this.UserID = userID;
        this.Date = date;
        this.CategoryIDs = categoryIDs;
        this.FoodItemMeals = foodItems;
        this.RecipeMeals = recipeMeals;
    }
    
    public NutrientTargets CalculateNutrientSums()
    {
        var sums = new NutrientTargets();
        if (this.FoodItemMeals != null) sums += sumNutrientsInFoodItems(this.FoodItemMeals);
        if (this.RecipeMeals != null)
        {
            foreach(var recipe in this.RecipeMeals)
            {
                sums += sumNutrientsInFoodItems(recipe.Fooditems) * recipe.Amount;
            }
        }
        return sums;
    }

    private NutrientTargets sumNutrientsInFoodItems(List<FoodItemAmountDTO> foodItems)
    {
        var sums = new NutrientTargets();
        foreach(var foodItem in foodItems)
        {
            var nutrients = new NutrientTargets
            {
                Protein = foodItem.FoodItem!.Protein,
                Carbohydrates = foodItem.FoodItem!.Carbohydrates,
                Sugars = foodItem.FoodItem!.Sugars,
                Fibres = foodItem.FoodItem!.Fibres,
                TotalFat = foodItem.FoodItem!.TotalFat,
                SaturatedFat = foodItem.FoodItem!.SaturatedFat,
                MonounsaturatedFat = foodItem.FoodItem!.MonounsaturatedFat,
                PolyunsaturatedFat = foodItem.FoodItem!.PolyunsaturatedFat,
                TransFat = foodItem.FoodItem!.TransFat,
                VitaminA = foodItem.FoodItem!.VitaminA,
                VitaminB6 = foodItem.FoodItem!.VitaminB6,
                VitaminB12 = foodItem.FoodItem!.VitaminB12,
                VitaminC = foodItem.FoodItem!.VitaminC,
                VitaminD = foodItem.FoodItem!.VitaminD,
                VitaminE = foodItem.FoodItem!.VitaminE,
                Thiamin = foodItem.FoodItem!.Thiamin,
                Riboflavin = foodItem.FoodItem!.Riboflavin,
                Niacin = foodItem.FoodItem!.Niacin,
                Folate = foodItem.FoodItem!.Folate,
                Salt = foodItem.FoodItem!.Salt,
                Potassium = foodItem.FoodItem!.Potassium,
                Magnesium = foodItem.FoodItem!.Magnesium,
                Iron = foodItem.FoodItem!.Iron,
                Zinc = foodItem.FoodItem!.Zinc,
                Phosphorus = foodItem.FoodItem!.Phosphorus,
                Copper = foodItem.FoodItem!.Copper,
                Iodine = foodItem.FoodItem!.Iodine,
                Selenium = foodItem.FoodItem!.Selenium,
                Calcium = foodItem.FoodItem!.Calcium
            };
            sums += nutrients * foodItem.Amount;
        }
        return sums;
    }
}