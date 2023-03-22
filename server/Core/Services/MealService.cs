namespace server.Core.Services;

public interface IMealService
{
    public Task<IActionResult> ReportMeal(MealReportDTO mealReportDTO);
    public Task<IActionResult> GetMealsByUserAndDate(int userID, DateTime date);
}

public class MealService : IMealService
{
    IMealRepository _repo;
    IFoodItemRepository _foodItemRepo;
    IRecipeRepository _recipeRepo;

    public MealService(IMealRepository repo, IFoodItemRepository foodItemRepo, IRecipeRepository recipeRepo)
    {
        _repo = repo;
        _foodItemRepo = foodItemRepo;
        _recipeRepo = recipeRepo;
    }

    public async Task<IActionResult> ReportMeal(MealReportDTO mealReportDTO)
    {
        if (mealReportDTO.Date == null || mealReportDTO.UserID == null || mealReportDTO.MealType == null)
            return new BadRequestResult();
        
        //If the meal doesnt exist, create it
        var mealDTO = await _repo.ReadByUserIdDateAndMealTypeAsync(
            (DateTime) mealReportDTO.Date, 
            (int) mealReportDTO.UserID, 
            (MealType) mealReportDTO.MealType
        );
        
        if ((mealReportDTO.FoodItemMeals ?? new List<FoodItemMealCreateDTO>()).Count == 0 && (mealReportDTO.RecipeMeals ?? new List<RecipeMealCreateDTO>()).Count == 0)
        {
            if (mealDTO.IsNone)
            {
                return new OkResult();
                //Nothing happens because there is no meal object and the contents are being set to empty.
            }
            else
            {
                //Delete the meal, since the contents are being set to empty
                var r = await _repo.RemoveAsync(mealDTO.Value.Id);
                if (r == Response.Deleted) return new OkResult();
                else return new StatusCodeResult(500);
            }
        }
        else
        {
            if (mealDTO.IsNone)
            {
                //Create the meal
                var mealCreateDTO = new MealCreateDTO
                {
                    MealType = mealReportDTO.MealType,
                    UserID = mealReportDTO.UserID,
                    Date = mealReportDTO.Date,
                    CategoryIDs = mealReportDTO.CategoryIDs
                };
                (Response r, MealDTO dto) = await _repo.CreateAsync(mealCreateDTO);
                if (r != Response.Created) return new StatusCodeResult(500);
                //Update the fooditems and recipes linked to this meal
                var mealUpdateDTO = new MealUpdateDTO
                {
                    Id = dto.Id,
                    FoodItemMeals = mealReportDTO.FoodItemMeals,
                    RecipeMeals = mealReportDTO.RecipeMeals
                };
                r = await _repo.UpdateAsync(mealUpdateDTO);
                if (r == Response.Updated) return new OkResult();
                else return new StatusCodeResult(500);
            }
            else
            {
                //Update the meal
                var mealUpdateDTO = new MealUpdateDTO
                {
                    Id = mealDTO.Value.Id,
                    MealType = mealDTO.Value.MealType,
                    UserID = mealDTO.Value.UserID,
                    Date = mealDTO.Value.Date,
                    CategoryIDs = mealReportDTO.CategoryIDs,
                    FoodItemMeals = mealReportDTO.FoodItemMeals,
                    RecipeMeals = mealReportDTO.RecipeMeals
                };
                var r = await _repo.UpdateAsync(mealUpdateDTO);
                if (r == Response.Updated) return new OkResult();
                else return new StatusCodeResult(500);
            }
        }
    }

    public async Task<IActionResult> GetMealsByUserAndDate(int userID, DateTime date)
    {
        var mealDTOs = await _repo.ReadAllByDateAndUser(date, userID);

        var meals = new List<MealNutrientInfo>();

        //Get all foodItems and add it to the total
        foreach(var meal in mealDTOs)
        {
            var mealTotal = new MealNutrientInfo(meal);

            foreach(var foodItem in await _foodItemRepo.ReadAllByMealId(meal.Id))
            {
                AddToMealTotal(mealTotal, foodItem);
            }

            foreach(var recipe in await _recipeRepo.ReadAllByMealId(meal.Id))
            {
                foreach(var foodItem in await _foodItemRepo.ReadAllByRecipeId(recipe.Recipe.Id))
                {
                    foodItem.Amount *= recipe.Amount;
                    AddToMealTotal(mealTotal, foodItem);
                }
            }

            meals.Add(mealTotal);
        }

        return new OkObjectResult(meals);
    }

    //Helper functions

    private void AddToMealTotal(MealNutrientInfo mealTotal, FoodItemAmountDTO foodItem)
    {
        if (foodItem.FoodItem == null) return;
        mealTotal.Calories += foodItem.FoodItem.Calories * foodItem.Amount;
        mealTotal.Protein += foodItem.FoodItem.Protein * foodItem.Amount;
        mealTotal.Carbohydrates += foodItem.FoodItem.Carbohydrates * foodItem.Amount;
        mealTotal.Sugars += foodItem.FoodItem.Sugars * foodItem.Amount;
        mealTotal.Fibres += foodItem.FoodItem.Fibres * foodItem.Amount;
        mealTotal.TotalFat += foodItem.FoodItem.TotalFat * foodItem.Amount;
        mealTotal.SaturatedFat += foodItem.FoodItem.SaturatedFat * foodItem.Amount;
        mealTotal.MonounsaturatedFat += foodItem.FoodItem.MonounsaturatedFat * foodItem.Amount;
        mealTotal.PolyunsaturatedFat += foodItem.FoodItem.PolyunsaturatedFat * foodItem.Amount;
        mealTotal.TransFat += foodItem.FoodItem.TransFat * foodItem.Amount;
        mealTotal.VitaminA += foodItem.FoodItem.VitaminA * foodItem.Amount;
        mealTotal.VitaminB6 += foodItem.FoodItem.VitaminB6 * foodItem.Amount;
        mealTotal.VitaminB12 += foodItem.FoodItem.VitaminB12 * foodItem.Amount;
        mealTotal.VitaminC += foodItem.FoodItem.VitaminC * foodItem.Amount;
        mealTotal.VitaminD += foodItem.FoodItem.VitaminD * foodItem.Amount;
        mealTotal.VitaminE += foodItem.FoodItem.VitaminE * foodItem.Amount;
        mealTotal.Thiamin += foodItem.FoodItem.Thiamin * foodItem.Amount;
        mealTotal.Riboflavin += foodItem.FoodItem.Riboflavin * foodItem.Amount;
        mealTotal.Niacin += foodItem.FoodItem.Niacin * foodItem.Amount;
        mealTotal.Folate += foodItem.FoodItem.Folate * foodItem.Amount;
        mealTotal.Salt += foodItem.FoodItem.Salt * foodItem.Amount;
        mealTotal.Potassium += foodItem.FoodItem.Potassium * foodItem.Amount;
        mealTotal.Magnesium += foodItem.FoodItem.Magnesium * foodItem.Amount;
        mealTotal.Iron += foodItem.FoodItem.Iron * foodItem.Amount;
        mealTotal.Zinc += foodItem.FoodItem.Zinc * foodItem.Amount;
        mealTotal.Phosphorus += foodItem.FoodItem.Phosphorus * foodItem.Amount;
        mealTotal.Copper += foodItem.FoodItem.Copper * foodItem.Amount;
        mealTotal.Iodine += foodItem.FoodItem.Iodine * foodItem.Amount;
        mealTotal.Selenium += foodItem.FoodItem.Selenium * foodItem.Amount;
        mealTotal.Calcium += foodItem.FoodItem.Calcium * foodItem.Amount;
    }
}



public class MealNutrientInfo
{
    public MealDTO Meal {get; set;}
    public float Calories {get; set;} = 0f;
    public float Protein {get; set;} = 0f;

    //Carbohydrates
    public float Carbohydrates {get; set;} = 0f;
    public float Sugars {get; set;} = 0f;
    public float Fibres {get; set;} = 0f;

    //Fats
    public float TotalFat {get; set;} = 0f;
    public float SaturatedFat {get; set;} = 0f;
    public float MonounsaturatedFat {get; set;} = 0f;
    public float PolyunsaturatedFat {get; set;} = 0f;
    public float TransFat {get; set;} = 0f;

    //Vitamins
    public float VitaminA {get; set;} = 0f;
    public float VitaminB6 {get; set;} = 0f;
    public float VitaminB12 {get; set;} = 0f;
    public float VitaminC {get; set;} = 0f;
    public float VitaminD {get; set;} = 0f;
    public float VitaminE {get; set;} = 0f;
    public float Thiamin {get; set;} = 0f;
    public float Riboflavin {get; set;} = 0f;
    public float Niacin {get; set;} = 0f;
    public float Folate {get; set;} = 0f;

    //Minerals
    public float Salt {get; set;} = 0f;
    public float Potassium {get; set;} = 0f;
    public float Magnesium {get; set;} = 0f;
    public float Iron {get; set;} = 0f;
    public float Zinc {get; set;} = 0f;
    public float Phosphorus {get; set;} = 0f;
    public float Copper {get; set;} = 0f;
    public float Iodine {get; set;} = 0f;
    public float Selenium {get; set;} = 0f;
    public float Calcium {get; set;} = 0f;

    public MealNutrientInfo(MealDTO meal)
    {
        Meal = meal;
    }

}