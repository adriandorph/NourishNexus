using System.Globalization;
using Microsoft.Extensions.Azure;

namespace server.Core.Services.MealPlan;

public interface IMealPlanGenerator
{
    public Task<IActionResult> Generate7DayMealPlan(int userID, DateTime startingDate);
}

public enum Response 
{
    Success,
    Fail,
    Cancelled
}

public class MealPlanGenerator
{
    //Settings
    private readonly static int MinRecipes = 20;

    private readonly IFoodItemRepository _foodItemRepo;
    private readonly IMealRepository _mealRepo;
    private readonly IRecipeRepository _recipeRepo;
    private readonly IUserRepository _userRepo;
    private readonly ICategoryRepository _categoryRepo;
    public MealPlanGenerator(IFoodItemRepository foodItemRepo, IMealRepository mealRepo, IRecipeRepository recipeRepo, IUserRepository userRepo, ICategoryRepository categoryRepo)
    {
        _foodItemRepo = foodItemRepo;
        _mealRepo = mealRepo;
        _recipeRepo = recipeRepo;
        _userRepo = userRepo;
        _categoryRepo = categoryRepo;
    }

    public async Task<Response> Generate7DayMealPlan(int userID, DateTime startingDate)
    {
        //Initial checks 
        if (!(await UserPrerequisites(userID))) return Response.Cancelled;
        
        //Intake levels
        NutrientTargets LowerBounds = await GetLowerBounds(userID);
        NutrientTargets IdealIntake = await CalculateIdealIntake(userID, startingDate);
        NutrientTargets UpperBounds = await GetUpperBounds(userID);

        // Recipe selection
        List<RecipeDTO> recipes = await CreateRecipeList(userID);

        //Insert recipes
        MealPlan mealPlan = await CreateInitialMealPlan(recipes, userID);
        
        // Ideal Intake met/exceeded?
        while(!(mealPlan.CalculateNutrientSums() >= IdealIntake))
        {
            if (recipes.Count == 0) return await FailPlan(mealPlan);
            await ReplaceLowest(mealPlan, IdealIntake);
        }
        //Is UpperBounds exceeded?
        while(mealPlan.CalculateNutrientSums() > UpperBounds)
        {
            if (recipes.Count == 0) return await FailPlan(mealPlan);
            await ReplaceHighest(mealPlan, UpperBounds);
            while(!(mealPlan.CalculateNutrientSums() >= LowerBounds))
            {
                await ReplaceLowest(mealPlan, LowerBounds);
            }
        }

        //Insert meal plan
        return await InsertPlan(mealPlan);
    }

    //Helper methods
    private async Task<bool> UserPrerequisites(int userID)
    {
        //Check if intake targets are set
        if (!(await IsNutritionSet(userID))) return false;
        //TODO: Add upper bounds, lower bound and ideal intake to User

        //Check if user has enough recipes
        return (await _recipeRepo.ReadAllByAuthorIDAsync(userID)).Count >= MinRecipes;
    }

    //Lower Bounds for a week
    private async Task<NutrientTargets> GetLowerBounds(int userID)
    {
        var userResult = await _userRepo.ReadWithNutritionByIDAsync(userID);
        if (userResult.IsNone) throw new Exception($"Could not find the user with id {userID}");
        var user = userResult.Value;
        var lowerBounds = new NutrientTargets();

        lowerBounds.Protein = user.ProteinLB ?? 0f;
        lowerBounds.Carbohydrates = user.CarbohydratesLB ?? 0f;
        lowerBounds.Sugars = user.SugarsLB ?? 0f;
        lowerBounds.Fibres = user.FibresLB ?? 0f;
        lowerBounds.TotalFat = user.TotalFatLB ?? 0f;
        lowerBounds.SaturatedFat = user.SaturatedFatLB ?? 0f;
        lowerBounds.MonounsaturatedFat = user.MonounsaturatedFatLB ?? 0f;
        lowerBounds.PolyunsaturatedFat = user.PolyunsaturatedFatLB ?? 0f;
        lowerBounds.TransFat = user.TransFatLB ?? 0f;
        lowerBounds.VitaminA = user.VitaminALB ?? 0f;
        lowerBounds.VitaminB6 = user.VitaminB6LB ?? 0f;
        lowerBounds.VitaminB12 = user.VitaminB12LB ?? 0f;
        lowerBounds.VitaminC = user.VitaminCLB ?? 0f;
        lowerBounds.VitaminD = user.VitaminDLB ?? 0f;
        lowerBounds.VitaminE = user.VitaminELB ?? 0f;
        lowerBounds.VitaminK1 = user.VitaminK1LB ?? 0f;
        lowerBounds.Thiamin = user.ThiaminLB ?? 0f;
        lowerBounds.Riboflavin = user.RiboflavinLB ?? 0f;
        lowerBounds.Niacin = user.NiacinLB ?? 0f;
        lowerBounds.Folate = user.FolateLB ?? 0f;
        lowerBounds.Salt = user.SaltLB ?? 0f;
        lowerBounds.Potassium = user.PotassiumLB ?? 0f;
        lowerBounds.Magnesium = user.MagnesiumLB ?? 0f;
        lowerBounds.Iron = user.IronLB ?? 0f;
        lowerBounds.Zinc = user.ZincLB ?? 0f;
        lowerBounds.Phosphorus = user.PhosphorusLB ?? 0f;
        lowerBounds.Copper = user.CopperLB ?? 0f;
        lowerBounds.Iodine = user.IodineLB ?? 0f;
        lowerBounds.Nickel = user.NickelLB ?? 0f;
        lowerBounds.Selenium = user.SeleniumLB ?? 0f;
        lowerBounds.Calcium = user.CalciumLB ?? 0f;
        
        return lowerBounds;
    }
    //Ideal intake for a week
    private Task<NutrientTargets> CalculateIdealIntake(int userID, DateTime startingDate)
    {
        throw new NotImplementedException();
    }

    //Upper Bounds for a week
    private async Task<NutrientTargets> GetUpperBounds(int userID)
    {
        var userResult = await _userRepo.ReadWithNutritionByIDAsync(userID);
        if(userResult.IsNone) throw new Exception($"Could not find the user with id {userID}");
        var user = userResult.Value;
        var upperBounds = new NutrientTargets();
        upperBounds.Protein = user.ProteinUB ?? 0f;
        upperBounds.Carbohydrates = user.CarbohydratesUB ?? 0f;
        upperBounds.Sugars = user.SugarsUB ?? 0f;
        upperBounds.Fibres = user.FibresUB ?? 0f;
        upperBounds.TotalFat = user.TotalFatUB ?? 0f;
        upperBounds.SaturatedFat = user.SaturatedFatUB ?? 0f;
        upperBounds.MonounsaturatedFat = user.MonounsaturatedFatUB ?? 0f;
        upperBounds.PolyunsaturatedFat = user.PolyunsaturatedFatUB ?? 0f;
        upperBounds.TransFat = user.TransFatUB ?? 0f;
        upperBounds.VitaminA = user.VitaminAUB ?? 0f;
        upperBounds.VitaminB6 = user.VitaminB6UB ?? 0f;
        upperBounds.VitaminB12 = user.VitaminB12UB ?? 0f;
        upperBounds.VitaminC = user.VitaminCUB ?? 0f;
        upperBounds.VitaminD = user.VitaminDUB ?? 0f;
        upperBounds.VitaminE = user.VitaminEUB ?? 0f;
        upperBounds.VitaminK1 = user.VitaminK1UB ?? 0f;
        upperBounds.Thiamin = user.ThiaminUB ?? 0f;
        upperBounds.Riboflavin = user.RiboflavinUB ?? 0f;
        upperBounds.Niacin = user.NiacinUB ?? 0f;
        upperBounds.Folate = user.FolateUB ?? 0f;
        upperBounds.Salt = user.SaltUB ?? 0f;
        upperBounds.Potassium = user.PotassiumUB ?? 0f;
        upperBounds.Magnesium = user.MagnesiumUB ?? 0f;
        upperBounds.Iron = user.IronUB ?? 0f;
        upperBounds.Zinc = user.ZincUB ?? 0f;
        upperBounds.Phosphorus = user.PhosphorusUB ?? 0f;
        upperBounds.Copper = user.CopperUB ?? 0f;
        upperBounds.Iodine = user.IodineUB ?? 0f;
        upperBounds.Nickel = user.NickelUB ?? 0f;
        upperBounds.Selenium = user.SeleniumUB ?? 0f;
        upperBounds.Calcium = user.CalciumUB ?? 0f;

        return upperBounds;
    }


    private async Task<List<RecipeDTO>> CreateRecipeList(int userID)
    {
        //Get saved recipes
        var userResult = await _userRepo.ReadByIDAsync(userID);
        if (userResult.IsNone) throw new Exception($"Could not find the user with id {userID}");
        var user = userResult.Value;
        var savedrecipes = new List<RecipeDTO>();
        foreach (var recipeId in user.SavedRecipeIds)
        {
            var recipeResult = await _recipeRepo.ReadByIDAsync(recipeId);
            if (recipeResult.IsNone) throw new Exception("Recipe is null");
            savedrecipes.Add(recipeResult.Value);
        }

        await OrderRecipesByRecency(savedrecipes);
        
        //First half
        List<RecipeDTO> recipes = new List<RecipeDTO>();
        recipes.AddRange(
            savedrecipes
                .Take(savedrecipes.Count/2)
                .OrderBy(r => Guid.NewGuid())
        );

        //Last half
        for(int i = savedrecipes.Count/2; i<savedrecipes.Count; i++)
        {
            recipes.Add(savedrecipes[i]);
        }

        return recipes;
    }

    private async Task<Option<MealPlan>> CreateInitialMealPlan(List<RecipeDTO> recipes, int userID)
    {
        MealPlan mealPlan = new MealPlan();
        var userResult = await _userRepo.ReadWithNutritionByIDAsync(userID);
        if (userResult.IsNone) throw new Exception("User is null!");
        var user = userResult.Value;

        //Find all the meals that exists an insert in the meal plan
        var date = DateTime.Now.Date;
        for(int i = 0; i < 7; i++)
        {
            var breakfastDTO = await _mealRepo.ReadByUserIdDateAndMealTypeAsync(date, userID, MealType.BREAKFAST);
            var lunchDTO = await _mealRepo.ReadByUserIdDateAndMealTypeAsync(date, userID, MealType.LUNCH);
            var dinnerDTO = await _mealRepo.ReadByUserIdDateAndMealTypeAsync(date, userID, MealType.DINNER);
            var snackDTO = await _mealRepo.ReadByUserIdDateAndMealTypeAsync(date, userID, MealType.SNACK);

            (mealPlan.Days[i].Breakfast, mealPlan.Days[i].BreakfastLocked) = await CreatePlannedMeal(breakfastDTO, userID, MealType.BREAKFAST, date);
            (mealPlan.Days[i].Lunch, mealPlan.Days[i].LunchLocked) = await CreatePlannedMeal(lunchDTO, userID, MealType.LUNCH, date);
            (mealPlan.Days[i].Dinner, mealPlan.Days[i].DinnerLocked) = await CreatePlannedMeal(dinnerDTO, userID, MealType.DINNER, date);
            (mealPlan.Days[i].Snacks, mealPlan.Days[i].SnacksLocked) = await CreatePlannedMeal(snackDTO, userID, MealType.SNACK, date);

            //Find
            var selectedRecipeBreakfast = FindAndRemoveRecipe(recipes, mealPlan.Days[i].Breakfast!);
            if(selectedRecipeBreakfast.IsNone) return new Option<MealPlan>(null);
            
            //Adjust amount
            var breakfastFoodItems = (await _foodItemRepo.ReadAllByRecipeId(selectedRecipeBreakfast.Value.Id)).ToList();
            float breakfastAmount = (float)user.BreakfastCalories! / CountCalories(breakfastFoodItems);
            var breakfastRecipe = new RecipeAmountWithFoodItemsDTO(
                breakfastAmount,
                selectedRecipeBreakfast.Value,
                breakfastFoodItems
            );
            //Insert
            mealPlan.Days[i].Breakfast!.RecipeMeals!.Add(breakfastRecipe);

            //Find
            var selectedRecipeLunch = FindAndRemoveRecipe(recipes, mealPlan.Days[i].Lunch!);
            if(selectedRecipeLunch.IsNone) return new Option<MealPlan>(null);
            //Adjust amount
            var lunchFoodItems = (await _foodItemRepo.ReadAllByRecipeId(selectedRecipeLunch.Value.Id)).ToList();
            float lunchAmount = (float)user.LunchCalories! / CountCalories(lunchFoodItems);
            var lunchRecipe = new RecipeAmountWithFoodItemsDTO(
                lunchAmount,
                selectedRecipeLunch.Value,
                lunchFoodItems
            );
            //Insert
            mealPlan.Days[i].Lunch!.RecipeMeals!.Add(breakfastRecipe);

            //Find
            var selectedRecipeDinner = FindAndRemoveRecipe(recipes, mealPlan.Days[i].Dinner!);
            if(selectedRecipeDinner.IsNone) return new Option<MealPlan>(null);
            //Adjust amount
            var dinnerFoodItems = (await _foodItemRepo.ReadAllByRecipeId(selectedRecipeDinner.Value.Id)).ToList();
            float dinnerAmount = (float)user.BreakfastCalories! / CountCalories(dinnerFoodItems);
            var dinnerRecipe = new RecipeAmountWithFoodItemsDTO(
                dinnerAmount,
                selectedRecipeDinner.Value,
                dinnerFoodItems
            );
            //Insert
            mealPlan.Days[i].Dinner!.RecipeMeals!.Add(breakfastRecipe);
            
            //Find
            var selectedRecipeSnack = FindAndRemoveRecipe(recipes, mealPlan.Days[i].Snacks!);
            if(selectedRecipeSnack.IsNone) return new Option<MealPlan>(null);
            //Adjust amount
            var snacksFoodItems = (await _foodItemRepo.ReadAllByRecipeId(selectedRecipeSnack.Value.Id)).ToList();
            float snacksAmount = (float) user.SnackCalories! / CountCalories(snacksFoodItems);
            var snacksRecipe = new RecipeAmountWithFoodItemsDTO(
                snacksAmount,
                selectedRecipeSnack.Value,
                snacksFoodItems
            );
            //Insert
            mealPlan.Days[i].Snacks!.RecipeMeals!.Add(breakfastRecipe);
            

            date = date.AddDays(1);
        }

        return mealPlan;
    }

    private float CountCalories(List<FoodItemAmountDTO> foodItems)
    {
        float sum = 0f;
        foreach(var foodItem in foodItems)
        {
            sum += foodItem.FoodItem!.Calories * foodItem.Amount;
        }
        return sum;
    }

    private Option<RecipeDTO> FindAndRemoveRecipe(List<RecipeDTO> recipes, PlannedMeal meal)
    {
        for(int i = 0; i <  recipes.Count; i++)
        {
            var recipe = recipes[i];
            //MealType Constraints
            bool hasCorrectMealtype = false;
            switch(meal.MealType) 
            {
                case MealType.BREAKFAST:
                    hasCorrectMealtype = recipe.IsBreakfast;
                    break;
                case MealType.LUNCH:
                    hasCorrectMealtype = recipe.IsLunch;
                    break;
                case MealType.DINNER:
                    hasCorrectMealtype = recipe.IsDinner;
                    break;
                case MealType.SNACK:
                    hasCorrectMealtype = recipe.IsSnack;
                    break;
                default:
                    break;
            }
            if (!hasCorrectMealtype) continue;


            //Category Constraints   
            bool hasCategories = true;
            foreach(var categoryID in meal.CategoryIDs)
            {
                if (!recipe.CategoryIDs.Contains(categoryID)) hasCategories = false;
            }
            if(!hasCategories) continue;

            //At this point the recipe fullfills all criteria and is selected.
            //Remove
            recipes.RemoveAt(i);
            return new Option<RecipeDTO>(recipe);
        }

        return new Option<RecipeDTO>(null);
    }

    private async Task<(PlannedMeal meal, bool locked)> CreatePlannedMeal(Option<MealDTO> mealDTO, int userID, MealType mealType, DateTime date)
    {
        PlannedMeal plannedMeal;

        if (mealDTO.IsSome)
        {
            var foodItemsInMeal = await _foodItemRepo.ReadAllByMealId(mealDTO.Value.Id);
            var recipesInMeal = new List<RecipeAmountWithFoodItemsDTO>();
            foreach(var recipe in await _recipeRepo.ReadAllByMealId(mealDTO.Value.Id))
            {
                var recipeAmountWithFoodItems = new RecipeAmountWithFoodItemsDTO(
                    recipe.Amount,
                    recipe.Recipe,
                    (await _foodItemRepo.ReadAllByRecipeId(recipe.Recipe.Id)).ToList()

                );
                recipesInMeal.Add(recipeAmountWithFoodItems);
            }

            plannedMeal = new PlannedMeal
            (
                mealDTO.Value.Id,
                mealDTO.Value.MealType,
                mealDTO.Value.UserID,
                mealDTO.Value.Date,
                mealDTO.Value.CategoryIDs,
                foodItemsInMeal.ToList(),
                recipesInMeal.ToList()
            );
        }
        else
        {

            plannedMeal = new PlannedMeal
            (
                null,
                mealType,
                userID,
                date,
                new List<int>(),
                null,
                null
            );
        }
        
        //Locked if there are no fooditems or recipes in the meal
        bool locked = (plannedMeal.FoodItemMeals == null || plannedMeal.FoodItemMeals.Count == 0) &&
                      (plannedMeal.RecipeMeals == null || plannedMeal.RecipeMeals.Count == 0);

        return (plannedMeal, locked);
    }

    //Plan to be returned in case of error
    private async Task<Response> FailPlan(MealPlan mealPlan)
    {
        await InsertPlan(mealPlan);
        return Response.Fail;
    }


    //Replace meal with most nutrient scores that are lower than II
    private Task<Response> ReplaceLowest(MealPlan mealPlan, NutrientTargets reference)
    {
        MealType lowestType;
        int lowestDay;
        int higestLowerCount = 0;

        for(int i = 0; i < mealPlan.Days.Count; i++)
        {
            var day = mealPlan.Days[i];
            if (!day.BreakfastLocked)
            {
                if(day.Breakfast == null) throw new Exception("Breakfast is null");
                var lowerCount = day.Breakfast.CalculateNutrientSums().lowerCount(reference);
                if (lowerCount > higestLowerCount)
                {
                    higestLowerCount = lowerCount;
                    lowestDay = i;
                    lowestType = MealType.BREAKFAST;
                }
            }

            if (!day.LunchLocked)
            {
                if(day.Lunch == null) throw new Exception("Lunch is null");
                var lowerCount = day.Lunch.CalculateNutrientSums().lowerCount(reference);
                if (lowerCount > higestLowerCount)
                {
                    higestLowerCount = lowerCount;
                    lowestDay = i;
                    lowestType = MealType.LUNCH;
                }
            }

            if (!day.DinnerLocked)
            {
                if(day.Dinner == null) throw new Exception("Dinner is null");
                var lowerCount = day.Dinner.CalculateNutrientSums().lowerCount(reference);
                if (lowerCount > higestLowerCount)
                {
                    higestLowerCount = lowerCount;
                    lowestDay = i;
                    lowestType = MealType.DINNER;
                }
            }

            if (!day.SnacksLocked)
            {
                if(day.Snacks == null) throw new Exception("Snacks is null");
                var lowerCount = day.Snacks.CalculateNutrientSums().lowerCount(reference);
                if (lowerCount > higestLowerCount)
                {
                    higestLowerCount = lowerCount;
                    lowestDay = i;
                    lowestType = MealType.SNACK;
                }
            }
        }

        //Replace lowest day
        //Find next recipe
        
        //Insert

        throw new NotImplementedException();
    }

    //Replace meal with most nutrient scores that are higher than UB
    private Task<Response> ReplaceHighest(MealPlan mealPlan, NutrientTargets reference)
    {
        throw new NotImplementedException();
    }

    private Task<Response> InsertNextRecipe(MealPlan mealPlan)
    {
        throw new NotImplementedException();
    }

    private Task<Response> InsertPlan(MealPlan mealPlan)
    {
        throw new NotImplementedException();
    }

    private Task<Response> OrderRecipesByRecency(List<RecipeDTO> recipes)
    {
        throw new NotImplementedException();
    }

    private async Task<bool> IsNutritionSet(int userID)
    {
        var userResult = await _userRepo.ReadWithNutritionByIDAsync(userID);
        if (userResult.IsNone) return false;

        var user = userResult.Value;

        if(user.BreakfastCalories == null) return false;
        if(user.LunchCalories == null) return false;
        if(user.DinnerCalories == null) return false;
        if(user.SnackCalories == null) return false;

        if(user.ProteinLB == null) return false;
        if(user.ProteinII == null) return false;
        if(user.ProteinUB == null) return false;

        if(user.CarbohydratesLB == null) return false;
        if(user.CarbohydratesII == null) return false;
        if(user.CarbohydratesUB == null) return false;

        if(user.SugarsLB == null) return false;
        if(user.SugarsII == null) return false;
        if(user.SugarsUB == null) return false;

        if(user.FibresLB == null) return false;
        if(user.FibresII == null) return false;
        if(user.FibresUB == null) return false;

        if(user.TotalFatLB == null) return false;
        if(user.TotalFatII == null) return false;
        if(user.TotalFatUB == null) return false;

        if(user.SaturatedFatLB == null) return false;
        if(user.SaturatedFatII == null) return false;
        if(user.SaturatedFatUB == null) return false;

        if(user.MonounsaturatedFatLB == null) return false;
        if(user.MonounsaturatedFatII == null) return false;
        if(user.MonounsaturatedFatUB == null) return false;

        if(user.PolyunsaturatedFatLB == null) return false;
        if(user.PolyunsaturatedFatII == null) return false;
        if(user.PolyunsaturatedFatUB == null) return false;

        if(user.TransFatLB == null) return false;
        if(user.TransFatII == null) return false;
        if(user.TransFatUB == null) return false;

        if(user.VitaminALB == null) return false;
        if(user.VitaminAII == null) return false;
        if(user.VitaminAUB == null) return false;

        if(user.VitaminB6LB == null) return false;
        if(user.VitaminB6II == null) return false;
        if(user.VitaminB6UB == null) return false;

        if(user.VitaminB12LB == null) return false;
        if(user.VitaminB12II == null) return false;
        if(user.VitaminB12UB == null) return false;

        if(user.VitaminCLB == null) return false;
        if(user.VitaminCII == null) return false;
        if(user.VitaminCUB == null) return false;

        if(user.VitaminDLB == null) return false;
        if(user.VitaminDII == null) return false;
        if(user.VitaminDUB == null) return false;

        if(user.VitaminELB == null) return false;
        if(user.VitaminEII == null) return false;
        if(user.VitaminEUB == null) return false;

        if(user.VitaminK1LB == null) return false;
        if(user.VitaminK1II == null) return false;
        if(user.VitaminK1UB == null) return false;

        if(user.ThiaminLB == null) return false;
        if(user.ThiaminII == null) return false;
        if(user.ThiaminUB == null) return false;

        if(user.RiboflavinLB == null) return false;
        if(user.RiboflavinII == null) return false;
        if(user.RiboflavinUB == null) return false;

        if(user.NiacinLB == null) return false;
        if(user.NiacinII == null) return false;
        if(user.NiacinUB == null) return false;

        if(user.FolateLB == null) return false;
        if(user.FolateII == null) return false;
        if(user.FolateUB == null) return false;

        if(user.SaltLB == null) return false;
        if(user.SaltII == null) return false;
        if(user.SaltUB == null) return false;

        if(user.PotassiumLB == null) return false;
        if(user.PotassiumII == null) return false;
        if(user.PotassiumUB == null) return false;

        if(user.MagnesiumLB == null) return false;
        if(user.MagnesiumII == null) return false;
        if(user.MagnesiumUB == null) return false;

        if(user.IronLB == null) return false;
        if(user.IronII == null) return false;
        if(user.IronUB == null) return false;

        if(user.ZincLB == null) return false;
        if(user.ZincII == null) return false;
        if(user.ZincUB == null) return false;

        if(user.PhosphorusLB == null) return false;
        if(user.PhosphorusII == null) return false;
        if(user.PhosphorusUB == null) return false;

        if(user.CopperLB == null) return false;
        if(user.CopperII == null) return false;
        if(user.CopperUB == null) return false;

        if(user.IodineLB == null) return false;
        if(user.IodineII == null) return false;
        if(user.IodineUB == null) return false;

        if(user.NickelLB == null) return false;
        if(user.NickelII == null) return false;
        if(user.NickelUB == null) return false;

        if(user.SeleniumLB == null) return false;
        if(user.SeleniumII == null) return false;
        if(user.SeleniumUB == null) return false;

        if(user.CalciumLB == null) return false;
        if(user.CalciumII == null) return false;
        if(user.CalciumUB == null) return false;

        return true;
    }
}