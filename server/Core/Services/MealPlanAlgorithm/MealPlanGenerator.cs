using System;
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
    public MealPlanGenerator(IFoodItemRepository foodItemRepo, IMealRepository mealRepo, IRecipeRepository recipeRepo, IUserRepository userRepo)
    {
        _foodItemRepo = foodItemRepo;
        _mealRepo = mealRepo;
        _recipeRepo = recipeRepo;
        _userRepo = userRepo;
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
        List<RecipeAmountDTO> recipes = await CreateRecipeList(userID);

        //Insert recipes
        MealPlan mealPlan = await CreateInitialMealPlan(recipes);
        
        // Ideal Intake met/exceeded?
        while(!(await IsIdealIntakeMet(mealPlan)))
        {
            if (recipes.Count == 0) return await FailPlan(mealPlan);
            await RemoveLowest(mealPlan);
            await InsertNextRecipe(mealPlan);
        }

        while(await IsUpperBoundsExceeded(mealPlan))
        {
            if (recipes.Count == 0) return await FailPlan(mealPlan);
            await RemoveHighest(mealPlan);
            await InsertNextRecipe(mealPlan);
            while(!(await IsLowerBoundsMet(mealPlan)))
            {
                await RemoveLowest(mealPlan);
                await InsertNextRecipe(mealPlan);
            }
        }

        //Insert meal plan
        return await InsertPlan(mealPlan);
    }

    //Helper methods
    private async Task<bool> UserPrerequisites(int userID)
    {
        //Check if intake targets are set
        //TODO: Add upper bounds, lower bound and ideal intake to User

        //Check if user has enough recipes
        return (await _recipeRepo.ReadAllByAuthorIDAsync(userID)).Count >= MinRecipes;
    }

    //Lower Bounds for a week
    private Task<NutrientTargets> GetLowerBounds(int userID)
    {
        throw new NotImplementedException();
    }
    //Ideal intake for a week
    private Task<NutrientTargets> CalculateIdealIntake(int userID, DateTime startingDate)
    {
        throw new NotImplementedException();
    }

    //Upper Bounds for a week
    private Task<NutrientTargets> GetUpperBounds(int userID)
    {
        throw new NotImplementedException();
    }


    private Task<List<RecipeAmountDTO>> CreateRecipeList(int userID)
    {
        throw new NotImplementedException();
    }

    private Task<MealPlan> CreateInitialMealPlan(List<RecipeAmountDTO> recipes)
    {
        throw new NotImplementedException();
    }

    private Task<bool> IsIdealIntakeMet(MealPlan mealPlan)
    {
        throw new NotImplementedException();
    }

    private async Task<Response> FailPlan(MealPlan mealPlan)
    {
        await InsertPlan(mealPlan);
        return Response.Fail;
    }

    private Task<Response> RemoveLowest(MealPlan mealPlan)
    {
        throw new NotImplementedException();
    }

    private Task<Response> RemoveHighest(MealPlan mealPlan)
    {
        throw new NotImplementedException();
    }

    private Task<Response> InsertNextRecipe(MealPlan mealPlan)
    {
        throw new NotImplementedException();
    }

    private Task<bool> IsUpperBoundsExceeded(MealPlan mealPlan)
    {
        throw new NotImplementedException();
    }

    private Task<bool> IsLowerBoundsMet(MealPlan mealPlan)
    {
        throw new NotImplementedException();
    }

    private Task<Response> InsertPlan(MealPlan mealPlan)
    {
        throw new NotImplementedException();
    }

    public class NutrientTargets
    {
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
        public float VitaminK1 {get; set;} = 0f;
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
        public float Nickel {get; set;} = 0f;
        public float Selenium {get; set;} = 0f;
        public float Calcium {get; set;} = 0f;
    }
}