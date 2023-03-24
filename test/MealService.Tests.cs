using Microsoft.AspNetCore.Mvc;
using server.Services;

namespace test;

public class MealServiceTests
{
    MealDTO _existingMeal;
    DateTime _date;

    NourishNexusContext _context;
    IMealRepository _repo;
    IFoodItemRepository _frepo;
    IRecipeRepository _rrepo;
    IMealService _service;
    public MealServiceTests()
    {
        var connection = new SqliteConnection("Data Source=:memory:");
        connection.Open();

        var builder = new DbContextOptionsBuilder<NourishNexusContext>();
        builder.UseSqlite(connection);
        builder.EnableSensitiveDataLogging();

        var context = new NourishNexusContext(builder.Options);
        context.Database.EnsureCreated();
        
        _date = DateTime.Now;
        _existingMeal = new MealDTO(1, MealType.BREAKFAST, 1, _date, new List<int>{1,2});
        _context = context;
        _repo = new MealRepository(_context);
        _rrepo = new RecipeRepository(_context);
        _frepo = new FoodItemRepository(_context);
        _service = new MealService(_repo, _frepo, _rrepo);
    }

    [Fact]
    public async void ReportMeal_BadRequest()
    {
        //Arrange
        var mealReportDTO = new MealReportDTO{};

        Mock<IMealRepository> repo = new Mock<IMealRepository>();
        Mock<IFoodItemRepository> foodItemRepo = new Mock<IFoodItemRepository>();
        Mock<IRecipeRepository> recipeRepo = new Mock<IRecipeRepository>();
        MealService service = new MealService(repo.Object, foodItemRepo.Object, recipeRepo.Object);

        //Act
        var r = await service.ReportMeal(mealReportDTO);

        //Assert
        Assert.IsType<BadRequestResult>(r);
    }

    [Fact]
    public async void ReportMeal_ExistingMeal_updated_with_new_foodItems_and_recipes()
    {
        //Arrange
        var mealUpdateDTO = new MealUpdateDTO
        {
            Id = _existingMeal.Id,
            MealType = MealType.BREAKFAST,
            UserID = 1,
            Date = _date,
            FoodItemMeals = new List<FoodItemMealCreateDTO>
            {
                new FoodItemMealCreateDTO
                {
                    Amount = 3.0f,
                    FoodItemID = 1,
                    MealID = 1
                }
            },
            RecipeMeals = new List<RecipeMealCreateDTO>()
            {
                new RecipeMealCreateDTO
                {
                    Amount = 3.0f,
                    RecipeID = 1,
                    MealID = 1
                }
            }
        };
        
        Mock<IMealRepository> repo = new Mock<IMealRepository>();
        repo.Setup(r => r.UpdateAsync(It.Is<MealUpdateDTO>(m => m.Id == 1))).ReturnsAsync(Response.Updated);
        repo.Setup(r => r.ReadByUserIdDateAndMealTypeAsync(_date, 1, MealType.BREAKFAST)).ReturnsAsync(_existingMeal);
        
        Mock<IFoodItemRepository> foodItemRepo = new Mock<IFoodItemRepository>();
        Mock<IRecipeRepository> recipeRepo = new Mock<IRecipeRepository>();
        MealService service = new MealService(repo.Object, foodItemRepo.Object, recipeRepo.Object);

        var mealReportDTO = new MealReportDTO
        {
            MealType = MealType.BREAKFAST,
            UserID = 1,
            Date = _date,
            FoodItemMeals = new List<FoodItemMealCreateDTO>
            {
                new FoodItemMealCreateDTO
                {
                    Amount = 3.0f,
                    FoodItemID = 1,
                    MealID = 1
                }
            },
            RecipeMeals = new List<RecipeMealCreateDTO>()
            {
                new RecipeMealCreateDTO
                {
                    Amount = 3.0f,
                    RecipeID = 1,
                    MealID = 1
                }
            }
        };

        //Act
        var r = await service.ReportMeal(mealReportDTO);
        //Assert
        Assert.IsType<OkResult>(r);

    }

    [Fact]
    public async void ReportMeal_ExistingMeal_updated_with_no_foodItems_and_recipes()
    {
        //Arrange
        Mock<IMealRepository> repo = new Mock<IMealRepository>();
        repo.Setup(r => r.ReadByUserIdDateAndMealTypeAsync(_date, 1, MealType.BREAKFAST)).ReturnsAsync(_existingMeal);
        repo.Setup(r => r.RemoveAsync(1)).ReturnsAsync(Response.Deleted);

        
        Mock<IFoodItemRepository> foodItemRepo = new Mock<IFoodItemRepository>();
        Mock<IRecipeRepository> recipeRepo = new Mock<IRecipeRepository>();
        MealService service = new MealService(repo.Object, foodItemRepo.Object, recipeRepo.Object);

        var mealReportDTO = new MealReportDTO
        {
            MealType = MealType.BREAKFAST,
            UserID = 1,
            Date = _date,
            FoodItemMeals = new List<FoodItemMealCreateDTO>(),
            RecipeMeals = new List<RecipeMealCreateDTO>()
        };

        //Act
        var r = await service.ReportMeal(mealReportDTO) as OkResult;
        //Assert
        Assert.IsType<OkResult>(r);
    }

    [Fact]
    public async void ReportMeal_Nonexisting_Meal_updated_with_new_foodItems_and_recipes()
    {
        //Arrange
        Mock<IMealRepository> repo = new Mock<IMealRepository>();
        repo.Setup(r => r.ReadByUserIdDateAndMealTypeAsync(_date, 1, MealType.BREAKFAST)).ReturnsAsync(new Option<MealDTO>(null));
        repo.Setup(r => r.CreateAsync(It.IsAny<MealCreateDTO>())).ReturnsAsync((Response.Created, _existingMeal));
        repo.Setup(r => r.UpdateAsync(It.Is<MealUpdateDTO>(m => m.Id == 1))).ReturnsAsync(Response.Updated);
        
        Mock<IFoodItemRepository> foodItemRepo = new Mock<IFoodItemRepository>();
        Mock<IRecipeRepository> recipeRepo = new Mock<IRecipeRepository>();
        MealService service = new MealService(repo.Object, foodItemRepo.Object, recipeRepo.Object);

        var mealReportDTO = new MealReportDTO
        {
            MealType = MealType.BREAKFAST,
            UserID = 1,
            Date = _date,
            FoodItemMeals = new List<FoodItemMealCreateDTO>
            {
                new FoodItemMealCreateDTO
                {
                    Amount = 3.0f,
                    FoodItemID = 1,
                    MealID = 1
                }
            },
            RecipeMeals = new List<RecipeMealCreateDTO>()
            {
                new RecipeMealCreateDTO
                {
                    Amount = 3.0f,
                    RecipeID = 1,
                    MealID = 1
                }
            }
        };

        //Act
        var r = await service.ReportMeal(mealReportDTO) as OkResult;
        //Assert
        Assert.IsType<OkResult>(r);
    }

    [Fact]
    public async void ReportMeal_Nonexisting_Meal_updated_with_no_foodItems_and_recipes()
    {
        //Arrange
        Mock<IMealRepository> repo = new Mock<IMealRepository>();
        repo.Setup(r => r.ReadByUserIdDateAndMealTypeAsync(_date, 1, MealType.BREAKFAST)).ReturnsAsync(new Option<MealDTO>(null));
        
        Mock<IFoodItemRepository> foodItemRepo = new Mock<IFoodItemRepository>();
        Mock<IRecipeRepository> recipeRepo = new Mock<IRecipeRepository>();
        MealService service = new MealService(repo.Object, foodItemRepo.Object, recipeRepo.Object);

        var mealReportDTO = new MealReportDTO
        {
            MealType = MealType.BREAKFAST,
            UserID = 1,
            Date = _date,
            FoodItemMeals = new List<FoodItemMealCreateDTO>(),
            RecipeMeals = new List<RecipeMealCreateDTO>()
        };

        //Act
        var r = await service.ReportMeal(mealReportDTO) as OkResult;
        
        //Assert
        Assert.IsType<OkResult>(r);
    }


    //GetMealsByDateAndUserId

    [Fact]
    public async void  GetMealsByDateAndUserId_returns_meals(){
        
        //Arrange
        //User
        var user1 = new User("Bravo", "antbr@itu.dk", new List<Recipe>());

        //Meals
        var meal1 = new Meal(MealType.BREAKFAST, user1, _date, new List<Category>());
        //3
        var meal2 = new Meal(MealType.DINNER, user1, _date, new List<Category>());
        //3 + 1.5/2 = 3.75
        
        //Recipe
        var recipe1 = new Recipe(
            "Boiled æggs", 
            true, 
            "Æggs that are boiled", 
            "Boil the æggs for 8 minutes", 
            user1.Id, 
            new List<Category>(), 
            true, 
            true, 
            false, 
            false
        );
        // values = 1

        var recipe2 = new Recipe(
            "Appetizer", 
            true, 
            "Appetizing appetizer", "Order it!", 
            user1.Id, new List<Category>(),
            false,
            false,
            false,
            true
        );
        // values = 3
        var recipe3 = new Recipe(
            "Main dish", 
            true, 
            "The main dish", 
            "Order it!", 
            user1.Id, 
            new List<Category>(),
            false,
            false,
            false,
            true
        );
        // values = 1.5

        //FoodItem
        var foodItem1 = new FoodItem("Kiwi",1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1);
        var foodItem2 = new FoodItem("Apple",1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1);
        var ægg = new FoodItem("Ægg", 1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1);
        var pasta = new FoodItem("Pasta", 1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1);
        //Relations
        var rm1 = new RecipeMeal(recipe1, meal1, 2);
        var rm2 = new RecipeMeal(recipe2, meal2, 1);
        var rm3 = new RecipeMeal(recipe3, meal2, 0.5f);

        var fim = new FoodItemMeal(foodItem1, meal1, 1);
        // values = 1

        var fir1 = new FoodItemRecipe(ægg, recipe1, 1);
        var fir2 = new FoodItemRecipe(foodItem1, recipe2, 2);
        var fir3 = new FoodItemRecipe(foodItem2, recipe2, 1);
        var fir4 = new FoodItemRecipe(pasta, recipe3, 1.5f);

        _context.Users.Add(user1);
        _context.SaveChanges();
        _context.Meals.Add(meal1);
        _context.Meals.Add(meal2);
        _context.Recipes.Add(recipe1);
        _context.Recipes.Add(recipe2);
        _context.Recipes.Add(recipe3);
        _context.FoodItems.Add(foodItem1);
        _context.FoodItems.Add(foodItem2);
        _context.FoodItems.Add(ægg);
        _context.FoodItems.Add(pasta);

        _context.SaveChanges();

        _context.RecipeMeals.Add(rm1);
        _context.RecipeMeals.Add(rm2);
        _context.RecipeMeals.Add(rm3);
        _context.FoodItemMeals.Add(fim);
        _context.FoodItemRecipes.Add(fir1);
        _context.FoodItemRecipes.Add(fir2);
        _context.FoodItemRecipes.Add(fir3);
        _context.FoodItemRecipes.Add(fir4);
        _context.SaveChanges();
        
        //Act
        var r = await _service.GetMealsByUserAndDate(user1.Id, _date);
        
        //Assert
        Assert.IsType<OkObjectResult>(r);
        var value = (r as OkObjectResult)!.Value as List<MealNutrientInfo>;

        Assert.Collection<MealNutrientInfo>
        (
            value!,
            mni => {
                Assert.Equal(meal1.Id, mni.Meal.Id);
                Assert.Equal(3, mni.Calories);
                Assert.Equal(3, mni.Protein);
                Assert.Equal(3, mni.Carbohydrates);
                Assert.Equal(3, mni.Sugars);
                Assert.Equal(3, mni.Fibres);
                Assert.Equal(3, mni.TotalFat);
                Assert.Equal(3, mni.SaturatedFat);
                Assert.Equal(3, mni.MonounsaturatedFat);
                Assert.Equal(3, mni.PolyunsaturatedFat);
                Assert.Equal(3, mni.TransFat);
                Assert.Equal(3, mni.VitaminA);
                Assert.Equal(3, mni.VitaminB6);
                Assert.Equal(3, mni.VitaminB12);
                Assert.Equal(3, mni.VitaminC);
                Assert.Equal(3, mni.VitaminD);
                Assert.Equal(3, mni.VitaminE);
                Assert.Equal(3, mni.Thiamin);
                Assert.Equal(3, mni.Riboflavin);
                Assert.Equal(3, mni.Niacin);
                Assert.Equal(3, mni.Folate);
                Assert.Equal(3, mni.Salt);
                Assert.Equal(3, mni.Potassium);
                Assert.Equal(3, mni.Magnesium);
                Assert.Equal(3, mni.Iron);
                Assert.Equal(3, mni.Zinc);
                Assert.Equal(3, mni.Phosphorus);
                Assert.Equal(3, mni.Copper);
                Assert.Equal(3, mni.Iodine);
                Assert.Equal(3, mni.Selenium);
                Assert.Equal(3, mni.Calcium);
            },
            mni => {
                Assert.Equal(meal2.Id, mni.Meal.Id);
                Assert.Equal(3.75f, mni.Calories);
                Assert.Equal(3.75f, mni.Protein);
                Assert.Equal(3.75f, mni.Carbohydrates);
                Assert.Equal(3.75f, mni.Sugars);
                Assert.Equal(3.75f, mni.Fibres);
                Assert.Equal(3.75f, mni.TotalFat);
                Assert.Equal(3.75f, mni.SaturatedFat);
                Assert.Equal(3.75f, mni.MonounsaturatedFat);
                Assert.Equal(3.75f, mni.PolyunsaturatedFat);
                Assert.Equal(3.75f, mni.TransFat);
                Assert.Equal(3.75f, mni.VitaminA);
                Assert.Equal(3.75f, mni.VitaminB6);
                Assert.Equal(3.75f, mni.VitaminB12);
                Assert.Equal(3.75f, mni.VitaminC);
                Assert.Equal(3.75f, mni.VitaminD);
                Assert.Equal(3.75f, mni.VitaminE);
                Assert.Equal(3.75f, mni.Thiamin);
                Assert.Equal(3.75f, mni.Riboflavin);
                Assert.Equal(3.75f, mni.Niacin);
                Assert.Equal(3.75f, mni.Folate);
                Assert.Equal(3.75f, mni.Salt);
                Assert.Equal(3.75f, mni.Potassium);
                Assert.Equal(3.75f, mni.Magnesium);
                Assert.Equal(3.75f, mni.Iron);
                Assert.Equal(3.75f, mni.Zinc);
                Assert.Equal(3.75f, mni.Phosphorus);
                Assert.Equal(3.75f, mni.Copper);
                Assert.Equal(3.75f, mni.Iodine);
                Assert.Equal(3.75f, mni.Selenium);
                Assert.Equal(3.75f, mni.Calcium);
            }
        );

        
    }

}