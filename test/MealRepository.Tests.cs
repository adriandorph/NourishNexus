using System.Data;
namespace test;

public class MealRepositoryTests
{
    NourishNexusContext _context;
    MealRepository _repo;
    //Setup
    User _user;

    Recipe _recipe;

    FoodItem _foodItem1;
    FoodItem _foodItem2;
    Category _category1;
    Category _category2;

    public MealRepositoryTests()
    {
        var connection = new SqliteConnection("Data Source=:memory:");
        connection.Open();

        var builder = new DbContextOptionsBuilder<NourishNexusContext>();
        builder.UseSqlite(connection);
        builder.EnableSensitiveDataLogging();

        var context = new NourishNexusContext(builder.Options);
        context.Database.EnsureCreated();

        _user = new User("user", "user@use.com", new byte[32], new byte[32], new List<Recipe>());

        _category1 = new Category("Category1");
        _category2 = new Category("Category2");

        _recipe = new Recipe("Apples and Oranges", true, "Both apples and oranges in one", "Slice and eat", 1, new List<Category>{_category1, _category2}, false, false, false, true);

        _foodItem1 = new FoodItem("Apple", 100, 2, 50, 30, 5, 0, 0, 0, 0, 0, 1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1);
        _foodItem2 = new FoodItem("Orange", 100, 2, 50, 30, 5, 0, 0, 0, 0, 0, 1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1);

        context.Users.Add(_user);
        context.Categories.Add(_category1);
        context.Categories.Add(_category2);
        context.FoodItems.Add(_foodItem1);
        context.FoodItems.Add(_foodItem2);
        context.Recipes.Add(_recipe);
        context.SaveChanges();

        _context = context;
        _repo = new MealRepository(_context);
    }

    [Fact]
    public async void Create_Created()
    {
        //Arrange
        var mealCreate = new MealCreateDTO
        {
            MealType = MealType.BREAKFAST,
            UserID = 1,
            Date = DateTime.Now,
            CategoryIDs = new List<int>{1}
        };

        //Act
        (Response r, MealDTO meal) = await _repo.CreateAsync(mealCreate);
        var mealEntity = await _context.Meals.FirstOrDefaultAsync();
        
        //Assert
        Assert.Equal(Response.Created, r);
        Assert.Equal(mealCreate.MealType, meal.MealType);
        Assert.Equal(mealCreate.UserID, meal.UserID);
        Assert.Equal(mealCreate.Date, meal.Date);
        Assert.True(Enumerable.SequenceEqual(mealCreate.CategoryIDs, meal.CategoryIDs));

        Assert.NotNull(mealEntity);
        Assert.Equal(mealCreate.MealType, mealEntity.MealType);
        Assert.Equal(mealCreate.UserID, mealEntity.User.Id);
        Assert.Equal(mealCreate.Date, mealEntity.Date);
        Assert.True(Enumerable.SequenceEqual(mealEntity.Categories.Select(c => c.Id), meal.CategoryIDs));        
    }

    [Fact]
    public async void Create_Conflict()
    {
        //Arrange
        var mealCreate = new MealCreateDTO
        {
            MealType = MealType.BREAKFAST,
            UserID = 1,
            Date = DateTime.Now,
            CategoryIDs = new List<int>{1}
        };

        //Act
        await _repo.CreateAsync(mealCreate);
        (Response r, MealDTO meal) = await _repo.CreateAsync(mealCreate);
        
        //Assert
        Assert.Equal(Response.Conflict, r);
    }

    [Fact]
    public async void Create_user_NotFound()
    {
        //Arrange
        var mealCreateDTO = new MealCreateDTO{
            MealType = MealType.DINNER,
            UserID = 1000,
            Date = new DateTime(2023, 02, 28)
        };

        //Act
        await _repo.CreateAsync(mealCreateDTO);
        (Response response, MealDTO mealDTO) = await _repo.CreateAsync(mealCreateDTO);

        //Assert
        Assert.Equal(Response.NotFound, response);
    }

    [Fact]
    public async void Create_BadRequest()
    {
        //Arrange
        var mealCreate = new MealCreateDTO
        {
            UserID = 1,
            Date = DateTime.Now,
            CategoryIDs = new List<int>{1}
        };

        //Act
        await _repo.CreateAsync(mealCreate);
        (Response r, MealDTO meal) = await _repo.CreateAsync(mealCreate);
        
        //Assert
        Assert.Equal(Response.BadRequest, r);
    }

    [Fact]
    public async void Update_Updated_MealType()
    {
        //Arrange
         var mealCreateDTO = new MealCreateDTO{
            MealType = MealType.DINNER,
            UserID = 1,
            Date = new DateTime(2023, 02, 28)

        };

         var mealUpdateDTO = new MealUpdateDTO
        {
            Id = 1,
            MealType = MealType.LUNCH
        };
        
        //Act
        await _repo.CreateAsync(mealCreateDTO);
        var response = await _repo.UpdateAsync(mealUpdateDTO);
        var entity = await _context.Meals
            .Where(r => r.Id == mealUpdateDTO.Id)
            .FirstOrDefaultAsync();
        
        //Assert
        Assert.Equal(Response.Updated, response);
        Assert.NotNull(entity);
        Assert.Equal(1, entity.Id);
        Assert.Equal(mealUpdateDTO.MealType, entity.MealType);
        Assert.Equal(mealCreateDTO.UserID, entity.User.Id);
        Assert.Equal(mealCreateDTO.Date, entity.Date);
        Assert.True(Enumerable.SequenceEqual(mealCreateDTO.CategoryIDs ?? new List<int>{}, entity.Categories.Select(c => c.Id).ToList()));
    }

    [Fact]
    public async void Update_Updated_Date()
    {
        //Arrange
         var mealCreateDTO = new MealCreateDTO{
            MealType = MealType.DINNER,
            UserID = 1,
            Date = new DateTime(2023, 02, 28)
        };

         var mealUpdateDTO = new MealUpdateDTO
        {
            Id = 1,
            Date = new DateTime(2023, 03, 23)
        };
        
        //Act
        await _repo.CreateAsync(mealCreateDTO);
        var response = await _repo.UpdateAsync(mealUpdateDTO);
        var entity = await _context.Meals
            .Where(r => r.Id == mealUpdateDTO.Id)
            .FirstOrDefaultAsync();
        
        //Assert
        Assert.Equal(Response.Updated, response);
        Assert.NotNull(entity);
        Assert.Equal(1, entity.Id);
        Assert.Equal(mealCreateDTO.MealType, entity.MealType);
        Assert.Equal(mealCreateDTO.UserID, entity.User.Id);
        Assert.Equal(mealUpdateDTO.Date, entity.Date);
        Assert.True(Enumerable.SequenceEqual(mealCreateDTO.CategoryIDs ?? new List<int>{}, entity.Categories.Select(c => c.Id).ToList()));
    }

    [Fact]
    public async void Update_Updated_Category()
    {
        //Arrange
         var mealCreateDTO = new MealCreateDTO{
            MealType = MealType.DINNER,
            UserID = 1,
            Date = new DateTime(2023, 02, 28),
            CategoryIDs = new List<int>{1}
        };

         var mealUpdateDTO = new MealUpdateDTO
        {
            Id = 1,
            CategoryIDs = new List<int>{2}
        };
        
        //Act
        await _repo.CreateAsync(mealCreateDTO);
        var response = await _repo.UpdateAsync(mealUpdateDTO);
        var entity = await _context.Meals
            .Where(r => r.Id == mealUpdateDTO.Id)
            .FirstOrDefaultAsync();
        
        //Assert
        Assert.Equal(Response.Updated, response);
        Assert.NotNull(entity);
        Assert.Equal(1, entity.Id);
        Assert.Equal(mealCreateDTO.MealType, entity.MealType);
        Assert.Equal(mealCreateDTO.UserID, entity.User.Id);
        Assert.Equal(mealCreateDTO.Date, entity.Date);
        Assert.True(Enumerable.SequenceEqual(mealUpdateDTO.CategoryIDs ?? new List<int>{}, entity.Categories.Select(c => c.Id).ToList()));
    }


    [Fact]
    public async void Update_Updated_FoodItemMeals()
    {
        //Arrange
         var mealCreateDTO = new MealCreateDTO{
            MealType = MealType.DINNER,
            UserID = 1,
            Date = new DateTime(2023, 02, 28),
            CategoryIDs = new List<int>{1}
        };

        var mealUpdateDTO = new MealUpdateDTO
        {
            Id = 1,
            FoodItemMeals = new List<FoodItemMealCreateDTO>
            {
                new FoodItemMealCreateDTO
                {
                    Amount = 1,
                    FoodItemID = 1,
                    MealID = 1
                }
            }
        };
        
        //Act
        await _repo.CreateAsync(mealCreateDTO);
        var response = await _repo.UpdateAsync(mealUpdateDTO);
        var entity = await _context.Meals
            .Where(r => r.Id == mealUpdateDTO.Id)
            .FirstOrDefaultAsync();
        
        //Assert
        Assert.Equal(Response.Updated, response);
        Assert.NotNull(entity);
        Assert.Equal(1, entity.Id);
        Assert.Equal(mealCreateDTO.MealType, entity.MealType);
        Assert.Equal(mealCreateDTO.UserID, entity.User.Id);
        Assert.Equal(mealCreateDTO.Date, entity.Date);
        Assert.True(Enumerable.SequenceEqual(mealCreateDTO.CategoryIDs ?? new List<int>{}, entity.Categories.Select(c => c.Id).ToList()));
        Assert.Collection<FoodItemMeal>(
            _context.FoodItemMeals.ToList(),
            item => {
                Assert.Equal(mealUpdateDTO.FoodItemMeals[0].Amount, item.Amount);
                Assert.Equal(mealUpdateDTO.FoodItemMeals[0].MealID, item.Meal.Id);
                Assert.Equal(mealUpdateDTO.FoodItemMeals[0].FoodItemID, item.FoodItem.Id);
            }
        );
    }


    [Fact]
    public async void Update_Updated_RecipeMeals()
    {
        //Arrange
         var mealCreateDTO = new MealCreateDTO{
            MealType = MealType.DINNER,
            UserID = 1,
            Date = new DateTime(2023, 02, 28),
            CategoryIDs = new List<int>{1}
        };

        var mealUpdateDTO = new MealUpdateDTO
        {
            Id = 1,
            RecipeMeals = new List<RecipeMealCreateDTO>
            {
                new RecipeMealCreateDTO
                {
                    Amount = 1,
                    RecipeID = 1,
                    MealID = 1
                }
            }
        };
        
        //Act
        await _repo.CreateAsync(mealCreateDTO);
        var response = await _repo.UpdateAsync(mealUpdateDTO);
        var entity = await _context.Meals
            .Where(r => r.Id == mealUpdateDTO.Id)
            .FirstOrDefaultAsync();
        
        //Assert
        Assert.Equal(Response.Updated, response);
        Assert.NotNull(entity);
        Assert.Equal(1, entity.Id);
        Assert.Equal(mealCreateDTO.MealType, entity.MealType);
        Assert.Equal(mealCreateDTO.UserID, entity.User.Id);
        Assert.Equal(mealCreateDTO.Date, entity.Date);
        Assert.True(Enumerable.SequenceEqual(mealCreateDTO.CategoryIDs ?? new List<int>{}, entity.Categories.Select(c => c.Id).ToList()));
        Assert.Collection<RecipeMeal>(
            _context.RecipeMeals.ToList(),
            item => {
                Assert.Equal(mealUpdateDTO.RecipeMeals[0].Amount, item.Amount);
                Assert.Equal(mealUpdateDTO.RecipeMeals[0].MealID, item.Meal.Id);
                Assert.Equal(mealUpdateDTO.RecipeMeals[0].RecipeID, item.Recipe.Id);
            }
        );
    }

    [Fact]
    public async void Update_NotFound()
    {
        //Arrange
        var mealUpdateDTO = new MealUpdateDTO
        {
            Id = 1,
            FoodItemMeals = new List<FoodItemMealCreateDTO>()
        };

        //Act
        Response r = await _repo.UpdateAsync(mealUpdateDTO);
        
        //Assert
        Assert.Equal(Response.NotFound, r);
    }

    [Fact]
    public async void Update_FoodItem_NotFound()
    {
        //Arrange
         var mealCreateDTO = new MealCreateDTO{
            MealType = MealType.DINNER,
            UserID = 1,
            Date = new DateTime(2023, 02, 28),
            CategoryIDs = new List<int>{1}
        };

        var mealUpdateDTO = new MealUpdateDTO
        {
            Id = 1,
            FoodItemMeals = new List<FoodItemMealCreateDTO>
            {
                new FoodItemMealCreateDTO
                {
                    Amount = 1,
                    FoodItemID = 1000,
                    MealID = 1
                }
            }
        };
        
        //Act
        await _repo.CreateAsync(mealCreateDTO);
        var response = await _repo.UpdateAsync(mealUpdateDTO);
        
        //Assert
        Assert.Equal(Response.NotFound, response);
    }

    [Fact]
    public async void Update_SameUser_SameDate_SameMealType_Returns_Conflict()
    {
        //Arrange
        var mealCreateDTO1 = new MealCreateDTO
        {
            MealType = MealType.LUNCH,
            UserID = 1,
            Date = new DateTime(2023, 02, 28),
            CategoryIDs = new List<int>{1}
        };

        var mealCreateDTO2 = new MealCreateDTO
        {
            MealType = MealType.DINNER,
            UserID = 1,
            Date = new DateTime(2023, 02, 28),
            CategoryIDs = new List<int>{1}
        };

         var mealUpdateDTO = new MealUpdateDTO
        {
            Id = 1,
            MealType = MealType.DINNER
        };
        
        //Act
        await _repo.CreateAsync(mealCreateDTO1);
        await _repo.CreateAsync(mealCreateDTO2);
        var response = await _repo.UpdateAsync(mealUpdateDTO);
        
        //Assert
        Assert.Equal(Response.Conflict, response);
    }

    [Fact]
    public async void Remove_Deleted()
    {
        //Arrange
        var mealCreateDTO = new MealCreateDTO
        {
            MealType = MealType.DINNER,
            UserID = 1,
            Date = new DateTime(2023, 02, 28),
            CategoryIDs = new List<int>{1}
        };

        //Act
        await _repo.CreateAsync(mealCreateDTO);
        var response = await _repo.RemoveAsync(1);
        
        //Assert
        Assert.Equal(Response.Deleted, response);
    }
    
     [Fact]
     public async void Remove_NotFound()
     {
        //Act
        var response = await _repo.RemoveAsync(1);

        //Assert
        Assert.Equal(Response.NotFound, response);
     }

    [Fact]
    public async void ReadByID_returns_MealDTO()
    {
         var mealCreateDTO = new MealCreateDTO
         {
            MealType = MealType.DINNER,
            UserID = 1,
            Date = new DateTime(2023, 02, 28),
            CategoryIDs = new List<int>{1,2}
         };
         
        //Act
        await _repo.CreateAsync(mealCreateDTO);
        var result = await _repo.ReadByIDAsync(1);

        //Assert
        Assert.True(result.IsSome);
        Assert.Equal(1, result.Value.Id);
        Assert.Equal(mealCreateDTO.MealType, result.Value.MealType);
        Assert.Equal(mealCreateDTO.UserID, result.Value.UserID);
        Assert.Equal(mealCreateDTO.Date, result.Value.Date);
        Assert.True(Enumerable.SequenceEqual(mealCreateDTO.CategoryIDs ?? new List<int>{}, result.Value.CategoryIDs));
    }
    
    [Fact]
    public async void ReadByID_returns_null()
    {
        //Act
        var result = await _repo.ReadByIDAsync(1);
        
        //Assert
        Assert.True(result.IsNone);
    }

    [Fact]
    public async void ReadAllByDateAndUser_returns_MealDTOs()
    {
        //Arrange
        var mealCreateDTO1 = new MealCreateDTO
        {
            MealType = MealType.DINNER,
            UserID = 1,
            Date = new DateTime(2023, 02, 28),
            CategoryIDs = new List<int>{}
         };
        
        var mealCreateDTO2 = new MealCreateDTO
        {
            MealType = MealType.LUNCH,
            UserID = 1,
            Date = new DateTime(2023, 02, 28),
            CategoryIDs = new List<int>{}
         };
         
         var mealCreateDTO3 = new MealCreateDTO
         {
            MealType = MealType.LUNCH,
            UserID = 2,
            Date = new DateTime(2023, 03, 03),
            CategoryIDs = new List<int>{}
         };

        //Act
        await _repo.CreateAsync(mealCreateDTO1);
        await _repo.CreateAsync(mealCreateDTO2);
        await _repo.CreateAsync(mealCreateDTO3);
        var list = await _repo.ReadAllByDateAndUser(new DateTime(2023, 02, 28), 1);
    
        //Assert
        Assert.NotNull(list);
        Assert.Collection<MealDTO>
        (
            list,
            item => 
            {
                Assert.Equal(1, item.Id);
                Assert.Equal(mealCreateDTO1.MealType, item.MealType);
                Assert.Equal(mealCreateDTO1.Date, item.Date);
                Assert.Equal(mealCreateDTO1.UserID, item.UserID);
                Assert.True(Enumerable.SequenceEqual(mealCreateDTO1.CategoryIDs ?? new List<int>{}, item.CategoryIDs));
                
            },
            item => 
            {
                Assert.Equal(2, item.Id);
                Assert.Equal(mealCreateDTO2.MealType, item.MealType);
                Assert.Equal(mealCreateDTO2.Date, item.Date);
                Assert.Equal(mealCreateDTO2.UserID, item.UserID);
                Assert.True(Enumerable.SequenceEqual(mealCreateDTO2.CategoryIDs ?? new List<int>{}, item.CategoryIDs));
 
            }
        );
    }

    [Fact]
    public async void ReadAllByUserAndDate_returns_empty_list()
    {
        //Act
        var list = await _repo.ReadAllByDateAndUser(new DateTime(2024, 12, 03), 4394);

        //Assert
        Assert.Empty(list.AsEnumerable());
    }
}