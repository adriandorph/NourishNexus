using System;
using System.Formats.Tar;

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
        List<RecipeDTO> recipes = await CreateRecipeList(userID);

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

    private Task<MealPlan> CreateInitialMealPlan(List<RecipeDTO> recipes)
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