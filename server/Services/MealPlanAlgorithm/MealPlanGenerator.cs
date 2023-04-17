using System.Diagnostics;
using Microsoft.IdentityModel.Tokens;

namespace server.Services.MealPlan;

public interface IMealPlanGenerator
{
    public Task<DietReport> Generate7DayMealPlan(int userID, DateTime startingDate);
}

public class MealPlanGenerator
{
    //Settings
    private readonly static int MinRecipes = 20;
    private readonly static int PreviousWeeks = 2;

    private readonly IFoodItemRepository _foodItemRepo;
    private readonly IMealRepository _mealRepo;
    private readonly IRecipeRepository _recipeRepo;
    private readonly IUserRepository _userRepo;

    public MealPlanGenerator(IFoodItemRepository foodItemRepo, IMealRepository mealRepo, IRecipeRepository recipeRepo, IUserRepository userRepo)
    {
        _foodItemRepo = foodItemRepo;
        _mealRepo = mealRepo;
        _recipeRepo = recipeRepo;
        _userRepo = userRepo;
    }

    public async Task<DietReport> Generate7DayMealPlan(int userID, DateTime startingDate)
    {
        var userResult = await _userRepo.ReadWithNutritionByIDAsync(userID);
        if (userResult.IsNone) throw new Exception("User not found");
        UserNutritionDTO user = userResult.Value;

        NutrientTargets lowerBounds = GetLowerBounds(user);
        NutrientTargets idealIntake = await CalculateIdealIntake(user, startingDate);
        NutrientTargets upperBounds = GetUpperBounds(user);
        UserCalories calorieTargets = GetCalorieTargets(user);

        List<RecipeAmountWithFoodItemsDTO> savedRecipes = await GetSavedRecipes(user);
        await OrderRecipesByRecency(savedRecipes, startingDate, user.Id);


        DietReport? dietReport = null;

        for(int i = 0; i<50; i++) //Tries to create a meal plan 50 times
        {
            dietReport = await Create7DayMealPlan(user, startingDate, lowerBounds, idealIntake, upperBounds, calorieTargets, savedRecipes);
            if (dietReport.Response == MealPlanResponse.Success) break;
            if (dietReport.Response == MealPlanResponse.Cancelled && i == 0) return dietReport;
        }

        if(dietReport == null || dietReport.MealPlan == null) throw new Exception("Mealplan or dietReport is null");
        await InsertPlan(dietReport.MealPlan);
        return dietReport;
    }

    private async Task<DietReport> Create7DayMealPlan(UserNutritionDTO user, DateTime startingDate, NutrientTargets LowerBounds, NutrientTargets IdealIntake, NutrientTargets UpperBounds, UserCalories calorieTargets, List<RecipeAmountWithFoodItemsDTO> savedRecipes)
    {
        //Initial checks
        if (!UserPrerequisites(user)) return (new DietReport(null,null,null,null, MealPlanResponse.Cancelled, null));
        

        // Recipe selection
        List<RecipeAmountWithFoodItemsDTO> recipes = CreateRecipeList(user, startingDate, savedRecipes);

        //Insert recipes
        Stopwatch stopwatch = new Stopwatch();
        stopwatch.Start();
        var mealPlanResult = await CreateInitialMealPlan(recipes, user.Id, calorieTargets, startingDate);
        stopwatch.Stop();
        var ts = stopwatch.ElapsedMilliseconds;
        Console.WriteLine($"CreateInitialMealPlan: {ts}ms");
        if(mealPlanResult.IsNone) return (new DietReport(null,null,null,null, MealPlanResponse.Cancelled, null));
        MealPlan mealPlan = mealPlanResult.Value;
        
        // Ideal Intake met/exceeded?
        stopwatch = new Stopwatch();
        stopwatch.Start();
        var curNutrientSums = mealPlan.CalculateNutrientSums();
        while(!(curNutrientSums.lowerCount(IdealIntake * 0.95f) < 4))
        {
            var r = ReplaceLowestMeals(mealPlan, IdealIntake, recipes, calorieTargets);
            curNutrientSums = mealPlan.CalculateNutrientSums();
            if (r == MealPlanResponse.Fail)
            {
                return new DietReport
                (
                    LowerBounds,
                    IdealIntake,
                    UpperBounds,
                    curNutrientSums,
                    MealPlanResponse.Fail,
                    mealPlan
                );
            }
        }
        stopwatch.Stop();
        Console.WriteLine($"Ideal intake met: {stopwatch.ElapsedMilliseconds}ms");
        //Is UpperBounds exceeded?
        stopwatch = new Stopwatch();
        stopwatch.Start();
        while(curNutrientSums > UpperBounds || !(curNutrientSums >= LowerBounds))
        {
            if (curNutrientSums >= LowerBounds)
            {
                var r = ReplaceHighest(mealPlan, UpperBounds, recipes, calorieTargets);
                curNutrientSums = mealPlan.CalculateNutrientSums();
                if (r == MealPlanResponse.Fail) 
                {
                    return new DietReport
                    (
                        LowerBounds,
                        IdealIntake,
                        UpperBounds,
                        curNutrientSums,
                        MealPlanResponse.Fail,
                        mealPlan
                    );
                }
            }
            
            while(!(curNutrientSums >= LowerBounds))
            {
                var r = ReplaceLowestMeals(mealPlan, LowerBounds, recipes, calorieTargets);
                curNutrientSums = mealPlan.CalculateNutrientSums();
                if (r == MealPlanResponse.Fail)
                {
                    return new DietReport
                    (
                        LowerBounds,
                        IdealIntake,
                        UpperBounds,
                        curNutrientSums,
                        MealPlanResponse.Fail,
                        mealPlan
                    );
                }
            }
        }
        stopwatch.Stop();
        Console.WriteLine($"Upperbounds: {stopwatch.ElapsedMilliseconds}ms");

        return new DietReport
        (
            LowerBounds,
            IdealIntake,
            UpperBounds,
            curNutrientSums,
            MealPlanResponse.Success,
            mealPlan
        );
    }

    //Helper methods
    private bool UserPrerequisites(UserNutritionDTO user)
    {
        //Check if intake targets are set
        if (!IsNutritionSet(user)) return false;

        //Check if user has enough recipes
        var count = user.SavedRecipeIds.Count;
        Console.WriteLine($"Recipes {count}");
        return  count >= MinRecipes;
    }

    //Lower Bounds for a week
    private NutrientTargets GetLowerBounds(UserNutritionDTO user)
    {
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
        lowerBounds.Selenium = user.SeleniumLB ?? 0f;
        lowerBounds.Calcium = user.CalciumLB ?? 0f;
        
        return lowerBounds * 7; //times 7 to convert to nutrients / week
    }

    //Ideal intake for a week, based on previous weeks' intake levels.
    private async Task<NutrientTargets> CalculateIdealIntake(UserNutritionDTO user, DateTime startingDate)
    {
        //Get last 2 weeks intake
        NutrientTargets previous = await GetPreviousIntake(user.Id, startingDate, GetDailyIdealIntake(user));

        //For each nutrient, balance out, but maximum upper bound
        var idealIntake = new NutrientTargets();
        idealIntake.Protein = BalanceOut((float)user.ProteinII! * 7, previous.Protein, (float)user.ProteinLB!, (float)user.ProteinUB!);
        idealIntake.Carbohydrates = BalanceOut((float)user.CarbohydratesII! * 7, previous.Carbohydrates, (float)user.CarbohydratesLB!, (float)user.CarbohydratesUB!);
        idealIntake.Sugars = BalanceOut((float)user.SugarsII! * 7, previous.Sugars, (float)user.SugarsLB!, (float)user.SugarsUB!);
        idealIntake.Fibres = BalanceOut((float)user.FibresII! * 7, previous.Fibres, (float)user.FibresLB!, (float)user.FibresUB!);
        idealIntake.TotalFat = BalanceOut((float)user.TotalFatII! * 7, previous.TotalFat, (float)user.TotalFatLB!, (float)user.TotalFatUB!);
        idealIntake.SaturatedFat = BalanceOut((float)user.SaturatedFatII! * 7, previous.SaturatedFat, (float)user.SaturatedFatLB!, (float)user.SaturatedFatUB!);
        idealIntake.MonounsaturatedFat = BalanceOut((float)user.MonounsaturatedFatII! * 7, previous.MonounsaturatedFat, (float)user.MonounsaturatedFatLB!, (float)user.MonounsaturatedFatUB!);
        idealIntake.PolyunsaturatedFat = BalanceOut((float)user.PolyunsaturatedFatII! * 7, previous.PolyunsaturatedFat, (float)user.PolyunsaturatedFatLB!, (float)user.PolyunsaturatedFatUB!);
        idealIntake.TransFat = BalanceOut((float)user.TransFatII! * 7, previous.TransFat, (float)user.TransFatLB!, (float)user.TransFatUB!);
        idealIntake.VitaminA = BalanceOut((float)user.VitaminAII! * 7, previous.VitaminA, (float)user.VitaminALB!, (float)user.VitaminAUB!);
        idealIntake.VitaminB6 = BalanceOut((float)user.VitaminB6II! * 7, previous.VitaminB6, (float)user.VitaminB6LB!, (float)user.VitaminB6UB!);
        idealIntake.VitaminB12 = BalanceOut((float)user.VitaminB12II! * 7, previous.VitaminB12, (float)user.VitaminB12LB!, (float)user.VitaminB12UB!);
        idealIntake.VitaminC = BalanceOut((float)user.VitaminCII! * 7, previous.VitaminC, (float)user.VitaminCLB!, (float)user.VitaminCUB!);
        idealIntake.VitaminD = BalanceOut((float)user.VitaminDII! * 7, previous.VitaminD, (float)user.VitaminDLB!, (float)user.VitaminDUB!);
        idealIntake.VitaminE = BalanceOut((float)user.VitaminEII! * 7, previous.VitaminE, (float)user.VitaminELB!, (float)user.VitaminEUB!);
        idealIntake.Thiamin = BalanceOut((float)user.ThiaminII! * 7, previous.Thiamin, (float)user.ThiaminLB!, (float)user.ThiaminUB!);
        idealIntake.Riboflavin = BalanceOut((float)user.RiboflavinII! * 7, previous.Riboflavin, (float)user.RiboflavinLB!, (float)user.RiboflavinUB!);
        idealIntake.Niacin = BalanceOut((float)user.NiacinII! * 7, previous.Niacin, (float)user.NiacinLB!, (float)user.NiacinUB!);
        idealIntake.Folate = BalanceOut((float)user.FolateII! * 7, previous.Folate, (float)user.FolateLB!, (float)user.FolateUB!);
        idealIntake.Salt = BalanceOut((float)user.SaltII! * 7, previous.Salt, (float)user.SaltLB!, (float)user.SaltUB!);
        idealIntake.Potassium = BalanceOut((float)user.PotassiumII! * 7, previous.Potassium, (float)user.PotassiumLB!, (float)user.PotassiumUB!);
        idealIntake.Magnesium = BalanceOut((float)user.MagnesiumII! * 7, previous.Magnesium, (float)user.MagnesiumLB!, (float)user.MagnesiumUB!);
        idealIntake.Iron = BalanceOut((float)user.IronII! * 7, previous.Iron, (float)user.IronLB!, (float)user.IronUB!);
        idealIntake.Zinc = BalanceOut((float)user.ZincII! * 7, previous.Zinc, (float)user.ZincLB!, (float)user.ZincUB!);
        idealIntake.Phosphorus = BalanceOut((float)user.PhosphorusII! * 7, previous.Phosphorus, (float)user.PhosphorusLB!, (float)user.PhosphorusUB!);
        idealIntake.Copper = BalanceOut((float)user.CopperII! * 7, previous.Copper, (float)user.CopperLB!, (float)user.CopperUB!);
        idealIntake.Iodine = BalanceOut((float)user.IodineII! * 7, previous.Iodine, (float)user.IodineLB!, (float)user.IodineUB!);
        idealIntake.Selenium = BalanceOut((float)user.SeleniumII! * 7, previous.Selenium, (float)user.SeleniumLB!, (float)user.SeleniumUB!);
        idealIntake.Calcium = BalanceOut((float)user.CalciumII! * 7, previous.Calcium, (float)user.CalciumLB!, (float)user.CalciumUB!);

        return idealIntake;
    }

    private NutrientTargets GetDailyIdealIntake(UserNutritionDTO user)
    {
        var idealIntake = new NutrientTargets();

        idealIntake.Protein = user.ProteinII ?? 0f;
        idealIntake.Carbohydrates = user.CarbohydratesII ?? 0f;
        idealIntake.Sugars = user.SugarsII ?? 0f;
        idealIntake.Fibres = user.FibresII ?? 0f;
        idealIntake.TotalFat = user.TotalFatII ?? 0f;
        idealIntake.SaturatedFat = user.SaturatedFatII ?? 0f;
        idealIntake.MonounsaturatedFat = user.MonounsaturatedFatII ?? 0f;
        idealIntake.PolyunsaturatedFat = user.PolyunsaturatedFatII ?? 0f;
        idealIntake.TransFat = user.TransFatII ?? 0f;
        idealIntake.VitaminA = user.VitaminAII ?? 0f;
        idealIntake.VitaminB6 = user.VitaminB6II ?? 0f;
        idealIntake.VitaminB12 = user.VitaminB12II ?? 0f;
        idealIntake.VitaminC = user.VitaminCII ?? 0f;
        idealIntake.VitaminD = user.VitaminDII ?? 0f;
        idealIntake.VitaminE = user.VitaminEII ?? 0f;
        idealIntake.Thiamin = user.ThiaminII ?? 0f;
        idealIntake.Riboflavin = user.RiboflavinII ?? 0f;
        idealIntake.Niacin = user.NiacinII ?? 0f;
        idealIntake.Folate = user.FolateII ?? 0f;
        idealIntake.Salt = user.SaltII ?? 0f;
        idealIntake.Potassium = user.PotassiumII ?? 0f;
        idealIntake.Magnesium = user.MagnesiumII ?? 0f;
        idealIntake.Iron = user.IronII ?? 0f;
        idealIntake.Zinc = user.ZincII ?? 0f;
        idealIntake.Phosphorus = user.PhosphorusII ?? 0f;
        idealIntake.Copper = user.CopperII ?? 0f;
        idealIntake.Iodine = user.IodineII ?? 0f;
        idealIntake.Selenium = user.SeleniumII ?? 0f;
        idealIntake.Calcium = user.CalciumII ?? 0f;

        return idealIntake;
    }

    private float BalanceOut(float ideal, float previous, float LB, float UB)
    {
        float balanced = ideal * (PreviousWeeks + 1) - previous;
        if (ideal > UB) ideal = UB;
        if (ideal < LB) ideal = LB;
        return balanced;
    }

    private async Task<NutrientTargets> GetPreviousIntake(int userID, DateTime endDate, NutrientTargets idealIntake)
    {
        TimeSpan oneDay = new TimeSpan(1,0,0,0);
        var date = endDate.Subtract(oneDay);
        var sum = new NutrientTargets();

        for(int i = 0; i < PreviousWeeks * 7; i++)
        {
            var meals = await _mealRepo.ReadAllByDateAndUser(date, userID);

            if (meals.IsNullOrEmpty())
            {
                //Add ideal days worth of nutrients
                sum += idealIntake;
            }
            else
            {
                foreach(var meal in meals)
                {
                    //Sum all nutrients
                    var recipes = await _recipeRepo.ReadAllByMealId(meal.Id);
                    foreach(var recipe in recipes)
                    {
                        var foodItemsInRecipe = await _foodItemRepo.ReadAllByRecipeId(recipe.Recipe.Id);
                        foreach(var foodItem in foodItemsInRecipe)
                        {
                            sum += NutrientTargets.ToNutrientTargets(foodItem) * recipe.Amount;
                        }
                    }
                    var foodItems = await _foodItemRepo.ReadAllByMealId(meal.Id);
                    foreach(var foodItem in foodItems)
                    {
                        sum += NutrientTargets.ToNutrientTargets(foodItem);
                    }
                    
                }
            }
            
            date = date.Subtract(oneDay);
        }


        
        return sum;
    }

    //Upper Bounds for a week
    private NutrientTargets GetUpperBounds(UserNutritionDTO user)
    {
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
        upperBounds.Selenium = user.SeleniumUB ?? 0f;
        upperBounds.Calcium = user.CalciumUB ?? 0f;

        return upperBounds * 7;
    }

    private UserCalories GetCalorieTargets(UserNutritionDTO user)
    {
        var calories = new UserCalories
        {
            Breakfast = (float)user.BreakfastCalories!,
            Lunch = (float)user.LunchCalories!,
            Dinner = (float)user.DinnerCalories!,
            Snacks = (float)user.SnackCalories!,
        };
        return calories;
    }

    private async Task<List<RecipeAmountWithFoodItemsDTO>> GetSavedRecipes(UserNutritionDTO user)
    {
        var savedrecipes = new List<RecipeAmountWithFoodItemsDTO>();
        foreach (var recipeId in user.SavedRecipeIds)
        {
            var recipeResult = await _recipeRepo.ReadByIDAsync(recipeId);
            if (recipeResult.IsNone) throw new Exception("Recipe is null");
            var recipe = recipeResult.Value;
            var foodItems = (await _foodItemRepo.ReadAllByRecipeId(recipe.Id)).ToList();
            var recipeWithFoodItems = new RecipeAmountWithFoodItemsDTO(
                1.0f,
                recipe,
                foodItems
            );

            savedrecipes.Add(recipeWithFoodItems);
        }
        return savedrecipes;
    }


    private List<RecipeAmountWithFoodItemsDTO> CreateRecipeList(UserNutritionDTO user, DateTime date, List<RecipeAmountWithFoodItemsDTO> savedrecipes)
    {
        //First half
        List<RecipeAmountWithFoodItemsDTO> recipes = new List<RecipeAmountWithFoodItemsDTO>();
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

    private async Task<Option<MealPlan>> CreateInitialMealPlan(List<RecipeAmountWithFoodItemsDTO> recipes, int userID, UserCalories calories, DateTime date)
    {
        MealPlan mealPlan = new MealPlan();

        //Find all the meals that exists and insert in the meal plan
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

            //Find the next selected recipe in the list
            if (!mealPlan.Days[i].BreakfastLocked)
            {
                var selectedRecipeBreakfast = FindAndRemoveRecipe(recipes, mealPlan.Days[i].Breakfast!);
                if(selectedRecipeBreakfast.IsNone) return new Option<MealPlan>(null);
                
                //Adjust amount
                float breakfastAmount = calories.Breakfast / CountCalories(selectedRecipeBreakfast.Value.Fooditems);
                var breakfastRecipe = new RecipeAmountWithFoodItemsDTO(
                    breakfastAmount,
                    selectedRecipeBreakfast.Value.Recipe,
                    selectedRecipeBreakfast.Value.Fooditems
                );

                //Insert
                mealPlan.Days[i].Breakfast!.RecipeMeals!.Add(breakfastRecipe);
            }
            
            
            if (!mealPlan.Days[i].LunchLocked)
            {
                //Find
                var selectedRecipeLunch = FindAndRemoveRecipe(recipes, mealPlan.Days[i].Lunch!);
                if(selectedRecipeLunch.IsNone) return new Option<MealPlan>(null);//TODO wtf hvorfor sker det her?
                //Adjust amount
                float lunchAmount = calories.Lunch / CountCalories(selectedRecipeLunch.Value.Fooditems);
                var lunchRecipe = new RecipeAmountWithFoodItemsDTO(
                    lunchAmount,
                    selectedRecipeLunch.Value.Recipe,
                    selectedRecipeLunch.Value.Fooditems
                );
                //Insert
                mealPlan.Days[i].Lunch!.RecipeMeals!.Add(lunchRecipe);
            }

            
            if(!mealPlan.Days[i].DinnerLocked)
            {
                //Find
                var selectedRecipeDinner = FindAndRemoveRecipe(recipes, mealPlan.Days[i].Dinner!);
                if(selectedRecipeDinner.IsNone) return new Option<MealPlan>(null);
                //Adjust amount
                float dinnerAmount = calories.Dinner / CountCalories(selectedRecipeDinner.Value.Fooditems);
                var dinnerRecipe = new RecipeAmountWithFoodItemsDTO(
                    dinnerAmount,
                    selectedRecipeDinner.Value.Recipe,
                    selectedRecipeDinner.Value.Fooditems
                );
                //Insert
                mealPlan.Days[i].Dinner!.RecipeMeals!.Add(dinnerRecipe);
            }
            
            if(!mealPlan.Days[i].SnacksLocked)
            {
                //Find
                var selectedRecipeSnack = FindAndRemoveRecipe(recipes, mealPlan.Days[i].Snacks!);
                if(selectedRecipeSnack.IsNone) return new Option<MealPlan>(null);
                //Adjust amount
                float snacksAmount = calories.Snacks / CountCalories(selectedRecipeSnack.Value.Fooditems);
                var snacksRecipe = new RecipeAmountWithFoodItemsDTO(
                    snacksAmount,
                    selectedRecipeSnack.Value.Recipe,
                    selectedRecipeSnack.Value.Fooditems
                );
                //Insert
                mealPlan.Days[i].Snacks!.RecipeMeals!.Add(snacksRecipe);
            }
            

            date = date.AddDays(1); //To continue in the next iteration
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

    private Option<RecipeAmountWithFoodItemsDTO> FindAndRemoveRecipe(List<RecipeAmountWithFoodItemsDTO> recipes, PlannedMeal meal)
    {
        for(int i = 0; i <  recipes.Count; i++)
        {
            var recipe = recipes[i];
            //MealType Constraints
            bool hasCorrectMealtype = false;
            switch(meal.MealType) 
            {
                case MealType.BREAKFAST:
                    hasCorrectMealtype = recipe.Recipe.IsBreakfast;
                    break;
                case MealType.LUNCH:
                    hasCorrectMealtype = recipe.Recipe.IsLunch;
                    break;
                case MealType.DINNER:
                    hasCorrectMealtype = recipe.Recipe.IsDinner;
                    break;
                case MealType.SNACK:
                    hasCorrectMealtype = recipe.Recipe.IsSnack;
                    break;
                default:
                    break;
            }
            if (!hasCorrectMealtype) continue;


            //Category Constraints   
            bool hasCategories = true;
            foreach(var categoryID in meal.CategoryIDs)
            {
                if (!recipe.Recipe.CategoryIDs.Contains(categoryID)) hasCategories = false;
            }
            if(!hasCategories) continue;

            //At this point the recipe fullfills all criteria and is selected.
            //Remove
            recipes.RemoveAt(i);
            return new Option<RecipeAmountWithFoodItemsDTO>(recipe);
        }
        return new Option<RecipeAmountWithFoodItemsDTO>(null);
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
                new List<FoodItemAmountDTO>(),
                new List<RecipeAmountWithFoodItemsDTO>()
            );
        }
        
        //Locked if there are any fooditems or recipes in the meal
        bool locked = (plannedMeal.FoodItemMeals.Count != 0) || (plannedMeal.RecipeMeals.Count != 0);

        return (plannedMeal, locked);
    }

    //Plan to be returned in case of error
    private async Task<MealPlanResponse> FailPlan(MealPlan mealPlan)
    {
        await InsertPlan(mealPlan);
        return MealPlanResponse.Fail;
    }


    //Replace meal with most nutrient scores that are lower than II
    private MealPlanResponse ReplaceLowestMeals(MealPlan mealPlan, NutrientTargets weeklyReference, List<RecipeAmountWithFoodItemsDTO> recipes, UserCalories calorieTargets)
    {
        int mostLowerBreakfast = 0;
        int mostLowerLunch = 0;
        int mostLowerDinner = 0;
        int mostLowerSnacks = 0;

        NutrientTargets mealPlanNutrients = mealPlan.CalculateNutrientSums();

        PlannedMeal? breakfast = null;
        PlannedMeal? lunch = null;
        PlannedMeal? dinner = null;
        PlannedMeal? snacks = null;

        for(int i = 0; i < mealPlan.Days.Count; i++)
        {
            var day = mealPlan.Days[i];
            if (!day.BreakfastLocked)
            {
                if(day.Breakfast == null) throw new Exception("Breakfast is null");
                //Only the nutrients that are not met in the plan should be counted
                var defaultBreakfastReference = weeklyReference * (1f/7f) * (calorieTargets.Breakfast / (calorieTargets.Breakfast + calorieTargets.Lunch + calorieTargets.Dinner + calorieTargets.Snacks));
                var breakfastReference = FilterReferenceWhereNutrientsAreMetOrExceeds(defaultBreakfastReference, mealPlanNutrients, weeklyReference);
                var lowerCount = day.Breakfast.CalculateNutrientSums().lowerCount(breakfastReference);
                if (lowerCount > mostLowerBreakfast)
                {
                    mostLowerBreakfast = lowerCount;
                    breakfast = day.Breakfast;
                }
            }

            if (!day.LunchLocked)
            {
                if(day.Lunch == null) throw new Exception("Lunch is null");
                var defaultLunchReference = weeklyReference * (1f/7f) * (calorieTargets.Lunch / (calorieTargets.Breakfast + calorieTargets.Lunch + calorieTargets.Dinner + calorieTargets.Snacks));
                var lunchReference = FilterReferenceWhereNutrientsAreMetOrExceeds(defaultLunchReference, mealPlanNutrients, weeklyReference);
                var lowerCount = day.Lunch.CalculateNutrientSums().lowerCount(lunchReference);
                if (lowerCount > mostLowerLunch)
                {
                    mostLowerLunch = lowerCount;
                    lunch = day.Lunch;
                }
            }

            if (!day.DinnerLocked)
            {
                if(day.Dinner == null) throw new Exception("Dinner is null");
                var defaultDinnerReference = weeklyReference * (1f/7f) * (calorieTargets.Dinner / (calorieTargets.Breakfast + calorieTargets.Lunch + calorieTargets.Dinner + calorieTargets.Snacks));
                var dinnerReference = FilterReferenceWhereNutrientsAreMetOrExceeds(defaultDinnerReference, mealPlanNutrients, weeklyReference);
                var lowerCount = day.Dinner.CalculateNutrientSums().lowerCount(dinnerReference);
                if (lowerCount > mostLowerDinner)
                {
                    mostLowerDinner = lowerCount;
                    dinner = day.Dinner;
                }
            }

            if (!day.SnacksLocked)
            {
                if(day.Snacks == null) throw new Exception("Snacks is null");
                var defaultSnackReference = weeklyReference * (1f/7f) * (calorieTargets.Snacks / (calorieTargets.Breakfast + calorieTargets.Lunch + calorieTargets.Dinner + calorieTargets.Snacks));
                var snackReference = FilterReferenceWhereNutrientsAreMetOrExceeds(defaultSnackReference, mealPlanNutrients, weeklyReference);
                var lowerCount = day.Snacks.CalculateNutrientSums().lowerCount(snackReference);
                if (lowerCount > mostLowerSnacks)
                {
                    mostLowerSnacks = lowerCount;
                    snacks = day.Snacks;
                }
            }
        }
        
        //Replace lowest
        var breakfastResult = breakfast == null ? MealPlanResponse.Fail : Replace(recipes, breakfast, calorieTargets.Breakfast);
        var lunchResult = lunch == null ? MealPlanResponse.Fail : Replace(recipes, lunch, calorieTargets.Lunch);
        var dinnerResult = dinner == null ? MealPlanResponse.Fail : Replace(recipes, dinner, calorieTargets.Dinner);
        var snacksResult = snacks == null ? MealPlanResponse.Fail : Replace(recipes, snacks, calorieTargets.Snacks);

        return breakfastResult == MealPlanResponse.Success || 
               lunchResult == MealPlanResponse.Success ||
               dinnerResult == MealPlanResponse.Success ||
               snacksResult == MealPlanResponse.Success
               ? MealPlanResponse.Success : MealPlanResponse.Fail;
    }

    private NutrientTargets FilterReferenceWhereNutrientsAreMetOrExceeds(NutrientTargets reference, NutrientTargets planned, NutrientTargets toBeMetOrExceeded)
        => new NutrientTargets
        {
            Protein = planned.Protein >= toBeMetOrExceeded.Protein ? 0f : reference.Protein,
            Carbohydrates = planned.Carbohydrates >= toBeMetOrExceeded.Carbohydrates ? 0f : reference.Carbohydrates,
            Sugars = planned.Sugars >= toBeMetOrExceeded.Sugars ? 0f : reference.Sugars,
            Fibres = planned.Fibres >= toBeMetOrExceeded.Fibres ? 0f : reference.Fibres,
            TotalFat = planned.TotalFat >= toBeMetOrExceeded.TotalFat ? 0f : reference.TotalFat,
            SaturatedFat = planned.SaturatedFat >= toBeMetOrExceeded.SaturatedFat ? 0f : reference.SaturatedFat,
            MonounsaturatedFat = planned.MonounsaturatedFat >= toBeMetOrExceeded.MonounsaturatedFat ? 0f : reference.MonounsaturatedFat,
            PolyunsaturatedFat = planned.PolyunsaturatedFat >= toBeMetOrExceeded.PolyunsaturatedFat ? 0f : reference.PolyunsaturatedFat,
            TransFat = planned.TransFat >= toBeMetOrExceeded.TransFat ? 0f : reference.TransFat,
            VitaminA = planned.VitaminA >= toBeMetOrExceeded.VitaminA ? 0f : reference.VitaminA,
            VitaminB6 = planned.VitaminB6 >= toBeMetOrExceeded.VitaminB6 ? 0f : reference.VitaminB6,
            VitaminB12 = planned.VitaminB12 >= toBeMetOrExceeded.VitaminB12 ? 0f : reference.VitaminB12,
            VitaminC = planned.VitaminC >= toBeMetOrExceeded.VitaminC ? 0f : reference.VitaminC,
            VitaminD = planned.VitaminD >= toBeMetOrExceeded.VitaminD ? 0f : reference.VitaminD,
            VitaminE = planned.VitaminE >= toBeMetOrExceeded.VitaminE ? 0f : reference.VitaminE,
            Thiamin = planned.Thiamin >= toBeMetOrExceeded.Thiamin ? 0f : reference.Thiamin,
            Riboflavin = planned.Riboflavin >= toBeMetOrExceeded.Riboflavin ? 0f : reference.Riboflavin,
            Niacin = planned.Niacin >= toBeMetOrExceeded.Niacin ? 0f : reference.Niacin,
            Folate = planned.Folate >= toBeMetOrExceeded.Folate ? 0f : reference.Folate,
            Salt = planned.Salt >= toBeMetOrExceeded.Salt ? 0f : reference.Salt,
            Potassium = planned.Potassium >= toBeMetOrExceeded.Potassium ? 0f : reference.Potassium,
            Magnesium = planned.Magnesium >= toBeMetOrExceeded.Magnesium ? 0f : reference.Magnesium,
            Iron = planned.Iron >= toBeMetOrExceeded.Iron ? 0f : reference.Iron,
            Zinc = planned.Zinc >= toBeMetOrExceeded.Zinc ? 0f : reference.Zinc,
            Phosphorus = planned.Phosphorus >= toBeMetOrExceeded.Phosphorus ? 0f : reference.Phosphorus,
            Copper = planned.Copper >= toBeMetOrExceeded.Copper ? 0f : reference.Copper,
            Iodine = planned.Iodine >= toBeMetOrExceeded.Iodine ? 0f : reference.Iodine,
            Selenium = planned.Selenium >= toBeMetOrExceeded.Selenium ? 0f : reference.Selenium,
            Calcium = planned.Calcium >= toBeMetOrExceeded.Calcium ? 0f : reference.Calcium
        };

    private MealPlanResponse Replace(List<RecipeAmountWithFoodItemsDTO> recipes, PlannedMeal meal, float calorieTarget)
    {
        //Find next recipe
        var recipe = FindAndRemoveRecipe(recipes, meal);
        if (recipe.IsNone) return MealPlanResponse.Fail;
        //Adjust amount
        float amount = calorieTarget / CountCalories(recipe.Value.Fooditems);
        var recipeAmount = new RecipeAmountWithFoodItemsDTO(
            amount,
            recipe.Value.Recipe,
            recipe.Value.Fooditems
        );

        //Insert
        meal.RecipeMeals = new List<RecipeAmountWithFoodItemsDTO>
        {
            recipeAmount
        };

        return MealPlanResponse.Success;
    }

    //Replace meal with most nutrient scores that are higher than UB
    private MealPlanResponse ReplaceHighest(MealPlan mealPlan, NutrientTargets weeklyReference, List<RecipeAmountWithFoodItemsDTO> recipes, UserCalories calorieTargets)
    {
        int mostHigherBreakfast = 0;
        int mostHigherLunch = 0;
        int mostHigherDinner = 0;
        int mostHigherSnacks = 0;

        PlannedMeal? breakfast = null;
        PlannedMeal? lunch = null;
        PlannedMeal? dinner = null;
        PlannedMeal? snacks = null;

        for(int i = 0; i < mealPlan.Days.Count; i++)
        {
            var day = mealPlan.Days[i];
            if (!day.BreakfastLocked)
            {
                if(day.Breakfast == null) throw new Exception("Breakfast is null");
                var breakfastReference = weeklyReference * (1f/7f) * (calorieTargets.Breakfast / (calorieTargets.Breakfast + calorieTargets.Lunch + calorieTargets.Dinner + calorieTargets.Snacks));
                var higherCount = day.Breakfast.CalculateNutrientSums().HigherCount(breakfastReference);
                if (higherCount > mostHigherBreakfast)
                {
                    mostHigherBreakfast = higherCount;
                    breakfast = day.Breakfast;
                }
            }

            if (!day.LunchLocked)
            {
                if(day.Lunch == null) throw new Exception("Lunch is null");
                var lunchReference = weeklyReference * (1f/7f) * (calorieTargets.Lunch / (calorieTargets.Breakfast + calorieTargets.Lunch + calorieTargets.Dinner + calorieTargets.Snacks));
                var higherCount = day.Lunch.CalculateNutrientSums().HigherCount(lunchReference);
                if (higherCount > mostHigherLunch)
                {
                    mostHigherLunch = higherCount;
                    lunch = day.Lunch;
                }
            }

            if (!day.DinnerLocked)
            {
                if(day.Dinner == null) throw new Exception("Dinner is null");
                var dinnerReference = weeklyReference * (1f/7f) * (calorieTargets.Dinner / (calorieTargets.Breakfast + calorieTargets.Lunch + calorieTargets.Dinner + calorieTargets.Snacks));
                var higherCount = day.Dinner.CalculateNutrientSums().HigherCount(dinnerReference);
                if (higherCount > mostHigherDinner)
                {
                    mostHigherDinner = higherCount;
                    dinner = day.Dinner;
                }
            }

            if (!day.SnacksLocked)
            {
                if(day.Snacks == null) throw new Exception("Snacks is null");
                var snackReference = weeklyReference * (1f/7f) * (calorieTargets.Snacks / (calorieTargets.Breakfast + calorieTargets.Lunch + calorieTargets.Dinner + calorieTargets.Snacks));
                var higherCount = day.Snacks.CalculateNutrientSums().HigherCount(snackReference);
                if (higherCount > mostHigherSnacks)
                {
                    mostHigherSnacks = higherCount;
                    snacks = day.Snacks;
                }
            }
        }
        
        //Replace highest
        var breakfastResult = breakfast == null ? MealPlanResponse.Fail : Replace(recipes, breakfast, calorieTargets.Breakfast);
        var lunchResult = lunch == null ? MealPlanResponse.Fail : Replace(recipes, lunch, calorieTargets.Lunch);
        var dinnerResult = dinner == null ? MealPlanResponse.Fail : Replace(recipes, dinner, calorieTargets.Dinner);
        var snacksResult = snacks == null ? MealPlanResponse.Fail : Replace(recipes, snacks, calorieTargets.Snacks);

        return breakfastResult == MealPlanResponse.Success || 
               lunchResult == MealPlanResponse.Success ||
               dinnerResult == MealPlanResponse.Success ||
               snacksResult == MealPlanResponse.Success
               ? MealPlanResponse.Success : MealPlanResponse.Fail;
    }

    public async Task<MealPlanResponse> InsertPlan(MealPlan mealPlan)
    {
        foreach(var day in mealPlan.Days)
        {
            if(!day.BreakfastLocked) await InsertMeal(day.Breakfast!);
            if(!day.LunchLocked) await InsertMeal(day.Lunch!);
            if(!day.DinnerLocked) await InsertMeal(day.Dinner!);
            if(!day.SnacksLocked) await InsertMeal(day.Snacks!);
        }
        return MealPlanResponse.Success;
    }

    private async Task InsertMeal(PlannedMeal meal)
    {
        var mealCreate = new MealCreateDTO
        {
            MealType = meal.MealType,
            UserID = meal.UserID,
            Date = meal.Date,
            CategoryIDs = meal.CategoryIDs,
            
        };
        (Core.Response r, MealDTO dto) = await _mealRepo.CreateAsync(mealCreate);
        //Doesn't matter if the meal already exists and therefore does not get created

        //Update the fooditems and recipes linked to this meal
        var recipeMeals = new List<RecipeMealCreateDTO>();
        foreach(var recipe in meal.RecipeMeals)
        {
            recipeMeals.Add(
                new RecipeMealCreateDTO
                {
                    Amount = recipe.Amount,
                    RecipeID = recipe.Recipe.Id,
                    MealID = dto.Id
                }
            );
        }

        var mealUpdateDTO = new MealUpdateDTO
        {
            Id = dto.Id,
            RecipeMeals = recipeMeals
        };
        r = await _mealRepo.UpdateAsync(mealUpdateDTO);
        if (r != Core.Response.Updated) throw new Exception("Could not update the meal");
    }

    private async Task<MealPlanResponse> OrderRecipesByRecency(List<RecipeAmountWithFoodItemsDTO> recipes, DateTime date, int userID)
    {
        //Ascending
        var alreadyAdded = new HashSet<RecipeAmountWithFoodItemsDTO>();

        List<RecipeAmountWithFoodItemsDTO> orderedRecipesDescending = new List<RecipeAmountWithFoodItemsDTO>();

         //Most recent at the start.
        for(int i = 30; i > 0; i--)
        {
            date -= new TimeSpan(1, 0, 0, 0);
            var meals = await _mealRepo.ReadAllByDateAndUser(date, userID);
            foreach (var meal in meals)
            {
                foreach(var recipe in await _recipeRepo.ReadAllByMealId(meal.Id))
                {
                    var recipeDTO = recipes.Where(r => r.Recipe.Id == recipe.Recipe.Id).FirstOrDefault();
                    if(recipeDTO != null && !alreadyAdded.Contains(recipeDTO))
                    {
                        orderedRecipesDescending.Add(recipeDTO);
                        alreadyAdded.Add(recipeDTO);
                    }
                }
            }
        }

        foreach(var recipe in recipes) if (!alreadyAdded.Contains(recipe)) orderedRecipesDescending.Add(recipe);

        orderedRecipesDescending.Reverse();
        recipes = orderedRecipesDescending;
        return MealPlanResponse.Success;
    }

    private bool IsNutritionSet(UserNutritionDTO user)
    {

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

        if(user.SeleniumLB == null) return false;
        if(user.SeleniumII == null) return false;
        if(user.SeleniumUB == null) return false;

        if(user.CalciumLB == null) return false;
        if(user.CalciumII == null) return false;
        if(user.CalciumUB == null) return false;

        return true;
    }

    private struct UserCalories
    {
        public float Breakfast;
        public float Lunch;
        public float Dinner;
        public float Snacks;
    }
}