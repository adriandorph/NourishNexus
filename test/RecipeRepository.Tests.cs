using System.Numerics;
namespace test;

public class RecipeRepositoryTests
{
    NourishNexusContext _context;
    RecipeRepository _repo;
    User _user1;
    User _user2;
    FoodItem _apple;
    FoodItem _orange;
    FoodItem _pear;
    Category _vegan;
    Category _fruit;

    server.Infrastructure.Meal _christmas;

    //Setup
    public RecipeRepositoryTests()
    {
        var connection = new SqliteConnection("Data Source=:memory:");
        connection.Open();

        var builder = new DbContextOptionsBuilder<NourishNexusContext>();
        builder.UseSqlite(connection);
        builder.EnableSensitiveDataLogging();

        var context = new NourishNexusContext(builder.Options);
        context.Database.EnsureCreated();

        //Add user
        _user1 = new User("John", "john@johnson.com", new byte[32], new byte[32], new List<Recipe>());
        _user2 = new User("Pablo", "pablo@pabloson.com", new byte[32], new byte[32], new List<Recipe>());
        context.Users.AddAsync(_user1);
        context.Users.AddAsync(_user2);

        //Add foodItems
        _apple = new FoodItem(
            "Apple",
            0f,
            0f,
            0f,
            0f,
            0f,
            0f,
            0f,
            0f,
            0f,
            0f,
            0f,
            0f,
            0f,
            0f,
            0f,
            0f,
            0f,
            0f,
            0f,
            0f,
            0f,
            0f,
            0f,
            0f,
            0f,
            0f,
            0f,
            0f,
            0f,
            0f
        );
        _orange = new FoodItem(
            "Orange",
            0f,
            0f,
            0f,
            0f,
            0f,
            0f,
            0f,
            0f,
            0f,
            0f,
            0f,
            0f,
            0f,
            0f,
            0f,
            0f,
            0f,
            0f,
            0f,
            0f,
            0f,
            0f,
            0f,
            0f,
            0f,
            0f,
            0f,
            0f,
            0f,
            0f
        );;
        _pear = new FoodItem(
            "Pear",
            0f,
            0f,
            0f,
            0f,
            0f,
            0f,
            0f,
            0f,
            0f,
            0f,
            0f,
            0f,
            0f,
            0f,
            0f,
            0f,
            0f,
            0f,
            0f,
            0f,
            0f,
            0f,
            0f,
            0f,
            0f,
            0f,
            0f,
            0f,
            0f,
            0f
        );
        context.FoodItems.Add(_apple);
        context.FoodItems.Add(_orange);
        context.FoodItems.Add(_pear);

        //Add categories
        _vegan = new Category("vegan");
        _fruit = new Category("fruit"); 

        context.Categories.Add(_vegan);
        context.Categories.Add(_fruit);

        //Add meal
        _christmas = new server.Infrastructure.Meal(MealType.DINNER, _user1, new DateTime(2023, 12, 24), new List<Category>{_vegan, _fruit});

        context.Meals.Add(_christmas);

        context.SaveChangesAsync();

        _context = context;
        _repo = new RecipeRepository(_context);
    }


    //Create

    [Fact]
    public async void CreateAsync_Created()
    {
        //Arrange
        //Add recipe

        var recipeCreateDTO = new RecipeCreateDTO{
            Title = "Apples and Oranges",
            IsPublic = false,
            Description = "A nice bowl of appels and oranges",
            Method = "Put the apples in a bowl. \nAdd the oranges into the bowl.",
            AuthorId = 1,
            CategoryIDs = new List<int>{1, 2}
        };

        //Act
        (Response response, RecipeDTO recipeDTO) = await _repo.CreateAsync(recipeCreateDTO);
        
        var entity = await _context.Recipes
            .Where(r => r.Id == 1)
            .FirstOrDefaultAsync();

        //Assert
        Assert.NotNull(entity);
        Assert.Equal(1, entity.Id);
        Assert.Equal(recipeCreateDTO.Title, entity.Title);
        Assert.Equal(recipeCreateDTO.IsPublic, entity.IsPublic);
        Assert.Equal(recipeCreateDTO.Description, entity.Description);
        Assert.Equal(recipeCreateDTO.Method, entity.Method);
        Assert.Equal(recipeCreateDTO.AuthorId, entity.AuthorId);
        Assert.True(Enumerable.SequenceEqual(recipeCreateDTO.CategoryIDs ?? new List<int>{}, entity.Categories.Select(c => c.Id).ToList()));
        
        Assert.Equal(entity, entity.Categories[0].Recipes[0]);
        Assert.Equal(entity, entity.Categories[1].Recipes[0]);

        Assert.Equal(Response.Created, response);
        Assert.Equal(entity.Id, recipeDTO.Id);
        Assert.Equal(recipeCreateDTO.Title, recipeDTO.Title);
        Assert.Equal(recipeCreateDTO.IsPublic, recipeDTO.IsPublic);
        Assert.Equal(recipeCreateDTO.Description, recipeDTO.Description);
        Assert.Equal(recipeCreateDTO.Method, recipeDTO.Method);
        Assert.Equal(recipeCreateDTO.AuthorId, recipeDTO.AuthorId);
        Assert.Equal(recipeCreateDTO.CategoryIDs, recipeDTO.CategoryIDs);
    }

    [Fact]
    public async void CreateAsync_Recipe_with_same_Title_and_Author_Conflict()
    {
        //Arrange
        var recipeCreateDTO = new RecipeCreateDTO{
            Title = "Apples and Oranges",
            IsPublic = false,
            Description = "A nice bowl of appels and oranges",
            Method = "Put the apples in a bowl. \nAdd the oranges into the bowl.",
            AuthorId = 1
        };

        //Act
        await _repo.CreateAsync(recipeCreateDTO);
        (Response response, RecipeDTO recipeDTO) = await _repo.CreateAsync(recipeCreateDTO);

        //Assert
        Assert.Equal(Response.Conflict, response);
    }

    [Fact]
    public async void Create_user_NotFound()
    {
        //Arrange
        var recipeCreateDTO = new RecipeCreateDTO{
            Title = "Apples and Oranges",
            IsPublic = false,
            Description = "A nice bowl of appels and oranges",
            Method = "Put the apples in a bowl. \nAdd the oranges into the bowl.",
            AuthorId = 1000
        };

        //Act
        await _repo.CreateAsync(recipeCreateDTO);
        (Response response, RecipeDTO recipeDTO) = await _repo.CreateAsync(recipeCreateDTO);

        //Assert
        Assert.Equal(Response.NotFound, response);
    }

    //Update

    [Fact]
    public async void Update_Updated_Title()
    {
        //Arrange
        var recipeCreateDTO = new RecipeCreateDTO{
            Title = "Apples and Oranges",
            IsPublic = false,
            Description = "A nice bowl of appels and oranges",
            Method = "Put the apples in a bowl. \nAdd the oranges into the bowl.",
            AuthorId = 1
        };

        var recipeUpdateDTO = new RecipeUpdateDTO
        {
            Id = 1,
            Title = "Apple and Orange bowl"
        };

        //Act
        await _repo.CreateAsync(recipeCreateDTO);
        var response = await _repo.UpdateAsync(recipeUpdateDTO);
        var entity = await _context.Recipes
            .Where(r => r.Id == recipeUpdateDTO.Id)
            .FirstOrDefaultAsync();

        //Assert
        Assert.Equal(Response.Updated, response);
        Assert.NotNull(entity);
        Assert.Equal(1, entity.Id);
        Assert.Equal(recipeUpdateDTO.Title, entity.Title);
        Assert.Equal(recipeCreateDTO.IsPublic, entity.IsPublic);
        Assert.Equal(recipeCreateDTO.Description, entity.Description);
        Assert.Equal(recipeCreateDTO.Method, entity.Method);
        Assert.Equal(recipeCreateDTO.AuthorId, entity.AuthorId);
        Assert.True(Enumerable.SequenceEqual(recipeCreateDTO.CategoryIDs ?? new List<int>{}, entity.Categories.Select(c => c.Id).ToList()));
    }

    [Fact]
    public async void Update_Updated_IsPublic()
    {
        //Arrange
        var recipeCreateDTO = new RecipeCreateDTO{
            Title = "Apples and Oranges",
            IsPublic = false,
            Description = "A nice bowl of appels and oranges",
            Method = "Put the apples in a bowl. \nAdd the oranges into the bowl.",
            AuthorId = 1
        };

        var recipeUpdateDTO = new RecipeUpdateDTO
        {
            Id = 1,
            IsPublic = true
        };

        //Act
        await _repo.CreateAsync(recipeCreateDTO);
        var response = await _repo.UpdateAsync(recipeUpdateDTO);
        var entity = await _context.Recipes
            .Where(fi => fi.Id == recipeUpdateDTO.Id)
            .FirstOrDefaultAsync();

        //Assert
        Assert.Equal(Response.Updated, response);
        Assert.NotNull(entity);
        Assert.Equal(1, entity.Id);
        Assert.Equal(recipeCreateDTO.Title, entity.Title);
        Assert.Equal(recipeUpdateDTO.IsPublic, entity.IsPublic);
        Assert.Equal(recipeCreateDTO.Description, entity.Description);
        Assert.Equal(recipeCreateDTO.Method, entity.Method);
        Assert.Equal(recipeCreateDTO.AuthorId, entity.AuthorId);
    }

    [Fact]
    public async void Update_Updated_Description()
    {
        //Arrange
        var recipeCreateDTO = new RecipeCreateDTO{
            Title = "Apples and Oranges",
            IsPublic = false,
            Description = "A nice bowl of appels and oranges.",
            Method = "Put the apples in a bowl. \nAdd the oranges into the bowl.",
            AuthorId = 1
        };

        var recipeUpdateDTO = new RecipeUpdateDTO
        {
            Id = 1,
            Description = "A horrible bland and boring bowl of apples and oranges."
        };

        //Act
        await _repo.CreateAsync(recipeCreateDTO);
        var response = await _repo.UpdateAsync(recipeUpdateDTO);
        var entity = await _context.Recipes
            .Where(fi => fi.Id == recipeUpdateDTO.Id)
            .FirstOrDefaultAsync();

        //Assert
        Assert.Equal(Response.Updated, response);
        Assert.NotNull(entity);
        Assert.Equal(1, entity.Id);
        Assert.Equal(recipeCreateDTO.Title, entity.Title);
        Assert.Equal(recipeCreateDTO.IsPublic, entity.IsPublic);
        Assert.Equal(recipeUpdateDTO.Description, entity.Description);
        Assert.Equal(recipeCreateDTO.Method, entity.Method);
        Assert.Equal(recipeCreateDTO.AuthorId, entity.AuthorId);
        Assert.True(Enumerable.SequenceEqual(recipeCreateDTO.CategoryIDs ?? new List<int>{}, entity.Categories.Select(c => c.Id).ToList()));
    }

    [Fact]
    public async void Update_Updated_Method()
    {
        //Arrange
        var recipeCreateDTO = new RecipeCreateDTO{
            Title = "Apples and Oranges",
            IsPublic = false,
            Description = "A nice bowl of appels and oranges",
            Method = "Put the apples in a bowl. \nAdd the oranges into the bowl.",
            AuthorId = 1
        };

        var recipeUpdateDTO = new RecipeUpdateDTO
        {
            Id = 1,
            Method = "Pour the apples and oranges into a bowl and mix."
        };

        //Act
        await _repo.CreateAsync(recipeCreateDTO);
        var response = await _repo.UpdateAsync(recipeUpdateDTO);
        var entity = await _context.Recipes
            .Where(fi => fi.Id == recipeUpdateDTO.Id)
            .FirstOrDefaultAsync();

        //Assert
        Assert.Equal(Response.Updated, response);
        Assert.NotNull(entity);
        Assert.Equal(1, entity.Id);
        Assert.Equal(recipeCreateDTO.Title, entity.Title);
        Assert.Equal(recipeCreateDTO.IsPublic, entity.IsPublic);
        Assert.Equal(recipeCreateDTO.Description, entity.Description);
        Assert.Equal(recipeUpdateDTO.Method, entity.Method);
        Assert.Equal(recipeCreateDTO.AuthorId, entity.AuthorId);
        Assert.True(Enumerable.SequenceEqual(recipeCreateDTO.CategoryIDs ?? new List<int>{}, entity.Categories.Select(c => c.Id).ToList()));
    }

    [Fact]
    public async void Update_Recipe_with_same_Title_and_Author_Conflict()
    {
        //Arrange
        var recipeCreateDTO1 = new RecipeCreateDTO{
            Title = "Apples and Oranges",
            IsPublic = false,
            Description = "A nice bowl of appels and oranges",
            Method = "Put the apples in a bowl. \nAdd the oranges into the bowl.",
            AuthorId = 1
        };

        var recipeCreateDTO2 = new RecipeCreateDTO{
            Title = "Apples and Oranges fruit cup",
            IsPublic = false,
            Description = "A nice cup of appels and oranges",
            Method = "Slice the apples and oranges.\nPut the apples in a cup.\nAdd the oranges into the cup.",
            AuthorId = 1
        };

        var recipeUpdateDTO = new RecipeUpdateDTO
        {
            Id = 2,
            Title = "Apples and Oranges",
        };
        //Act
        await _repo.CreateAsync(recipeCreateDTO1);
        await _repo.CreateAsync(recipeCreateDTO2);
        var response = await _repo.UpdateAsync(recipeUpdateDTO);

        //Assert
        Assert.Equal(Response.Conflict, response);
    }


    [Fact]
    public async void Update_Updated_Categories()
    {
        //Arrange
        var recipeCreateDTO = new RecipeCreateDTO{
            Title = "Apples and Oranges",
            IsPublic = false,
            Description = "A nice bowl of appels and oranges",
            Method = "Put the apples in a bowl. \nAdd the oranges into the bowl.",
            AuthorId = 1,
            CategoryIDs = new List<int>{1}
        };

        var recipeUpdateDTO = new RecipeUpdateDTO
        {
            Id = 1,
            CategoryIDs = new List<int>{1,2}
        };

        //Act
        await _repo.CreateAsync(recipeCreateDTO);
        var response = await _repo.UpdateAsync(recipeUpdateDTO);
        var entity = await _context.Recipes
            .Where(r => r.Id == recipeUpdateDTO.Id)
            .FirstOrDefaultAsync();

        //Assert
        Assert.Equal(Response.Updated, response);
        Assert.NotNull(entity);
        Assert.Equal(1, entity.Id);
        Assert.Equal(recipeCreateDTO.Title, entity.Title);
        Assert.Equal(recipeCreateDTO.IsPublic, entity.IsPublic);
        Assert.Equal(recipeCreateDTO.Description, entity.Description);
        Assert.Equal(recipeCreateDTO.Method, entity.Method);
        Assert.Equal(recipeCreateDTO.AuthorId, entity.AuthorId);
        Assert.True(Enumerable.SequenceEqual(recipeUpdateDTO.CategoryIDs ?? new List<int>{}, entity.Categories.Select(c => c.Id).ToList()));
    }


    [Fact]
    public async void Update_Updated_FoodItems()
    {
        //Arrange
        var recipeCreateDTO = new RecipeCreateDTO{
            Title = "Apples and Oranges",
            IsPublic = false,
            Description = "A nice bowl of appels and oranges",
            Method = "Put the apples in a bowl. \nAdd the oranges into the bowl.",
            AuthorId = 1
        };

        var recipeUpdateDTO = new RecipeUpdateDTO
        {
            Id = 1,
            FoodItemRecipes = new List<FoodItemRecipeCreateDTO>{new FoodItemRecipeCreateDTO{Amount = 2, FoodItemID = 2, RecipeID = 1}}
        };

        //Act
        await _repo.CreateAsync(recipeCreateDTO);
        var response = await _repo.UpdateAsync(recipeUpdateDTO);
        var entity = await _context.Recipes
            .Where(r => r.Id == recipeUpdateDTO.Id)
            .FirstOrDefaultAsync();

        //Assert
        Assert.Equal(Response.Updated, response);
        Assert.NotNull(entity);
        Assert.Equal(1, entity.Id);
        Assert.Equal(recipeCreateDTO.Title, entity.Title);
        Assert.Equal(recipeCreateDTO.IsPublic, entity.IsPublic);
        Assert.Equal(recipeCreateDTO.Description, entity.Description);
        Assert.Equal(recipeCreateDTO.Method, entity.Method);
        Assert.Equal(recipeCreateDTO.AuthorId, entity.AuthorId);
        Assert.True(Enumerable.SequenceEqual(recipeCreateDTO.CategoryIDs ?? new List<int>{}, entity.Categories.Select(c => c.Id).ToList()));
        
        Assert.Collection<FoodItemRecipeDTO>
        (
            _context.FoodItemRecipes.Where(fir => fir.Recipe.Id == entity.Id).Select(fir => fir.ToDTO()).ToList(),
            item => {
                Assert.Equal(2, item.Amount);
                Assert.Equal(2, item.FoodItemID);
                Assert.Equal(1, item.RecipeID);
            }
        );
    }



    //Remove

    [Fact]
    public async void Remove_Deleted()
    {
        //Arrange
        var recipeCreateDTO = new RecipeCreateDTO{
            Title = "Apples and Oranges",
            IsPublic = false,
            Description = "A nice bowl of appels and oranges",
            Method = "Put the apples in a bowl. \nAdd the oranges into the bowl.",
            AuthorId = 1
        };

        //Act
        await _repo.CreateAsync(recipeCreateDTO);
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


    //ReadById
    [Fact]
    public async void ReadByID_returns_RecipeDTO()
    {
        //Arrange
        var recipeCreateDTO = new RecipeCreateDTO{
            Title = "Apples and Oranges",
            IsPublic = false,
            Description = "A nice bowl of appels and oranges",
            Method = "Put the apples in a bowl. \nAdd the oranges into the bowl.",
            AuthorId = 1
        };

        //Act
        await _repo.CreateAsync(recipeCreateDTO);
        var result = await _repo.ReadByIDAsync(1);
        

        //Assert
        Assert.True(result.IsSome);
        Assert.Equal(1, result.Value.Id);
        Assert.Equal(recipeCreateDTO.Title, result.Value.Title);
        Assert.Equal(recipeCreateDTO.IsPublic, result.Value.IsPublic);
        Assert.Equal(recipeCreateDTO.Description, result.Value.Description);
        Assert.Equal(recipeCreateDTO.Method, result.Value.Method);
        Assert.Equal(recipeCreateDTO.AuthorId, result.Value.AuthorId);
        Assert.True(Enumerable.SequenceEqual(recipeCreateDTO.CategoryIDs ?? new List<int>{}, result.Value.CategoryIDs));
    }

    [Fact]
    public async void ReadByID_returns_null()
    {
        //Act
        var result = await _repo.ReadByIDAsync(1);
        
        //Assert
        Assert.True(result.IsNone);
    }

    //ReadAll
    [Fact]
    public async void ReadAll_returns_all_fooditems()
    {
        //Arrange
        var recipeCreateDTO1 = new RecipeCreateDTO{
            Title = "Apples and Oranges",
            IsPublic = false,
            Description = "A nice bowl of appels and oranges",
            Method = "Put the apples in a bowl. \nAdd the oranges into the bowl.",
            AuthorId = 1
        };

        var recipeCreateDTO2 = new RecipeCreateDTO{
            Title = "Apples and Oranges fruit cup",
            IsPublic = false,
            Description = "A nice cup of appels and oranges",
            Method = "Slice the apples and oranges.\nPut the apples in a cup.\nAdd the oranges into the cup.",
            AuthorId = 1
        };

        //Act
        await _repo.CreateAsync(recipeCreateDTO1);
        await _repo.CreateAsync(recipeCreateDTO2);
        var list = await _repo.ReadAllAsync();

        //Assert
        Assert.NotNull(list);
        Assert.Collection<RecipeDTO>
        (
            list,
            item => {
                Assert.Equal(1, item.Id);
                Assert.Equal(recipeCreateDTO1.Title, item.Title);
                Assert.Equal(recipeCreateDTO1.IsPublic, item.IsPublic);
                Assert.Equal(recipeCreateDTO1.Description, item.Description);
                Assert.Equal(recipeCreateDTO1.Method, item.Method);
                Assert.Equal(recipeCreateDTO1.AuthorId, item.AuthorId);
                Assert.True(Enumerable.SequenceEqual(recipeCreateDTO1.CategoryIDs ?? new List<int>{}, item.CategoryIDs));
            },
            item => {
                Assert.Equal(2, item.Id);
                Assert.Equal(recipeCreateDTO2.Title, item.Title);
                Assert.Equal(recipeCreateDTO2.IsPublic, item.IsPublic);
                Assert.Equal(recipeCreateDTO2.Description, item.Description);
                Assert.Equal(recipeCreateDTO2.Method, item.Method);
                Assert.Equal(recipeCreateDTO2.AuthorId, item.AuthorId);
                Assert.True(Enumerable.SequenceEqual(recipeCreateDTO2.CategoryIDs ?? new List<int>{}, item.CategoryIDs));
            }
        );
    }

    [Fact]
    public async void ReadAll_returns_empty_list()
    {
        //Act
        var list = await _repo.ReadAllAsync();

        //Assert
        Assert.Empty(list.AsEnumerable());
    }


    [Fact]
    public async void ReadAllByAuthorID_returns_all_fooditems()
    {
        //Arrange
        var recipeCreateDTO1 = new RecipeCreateDTO{
            Title = "Apples and Oranges",
            IsPublic = false,
            Description = "A nice bowl of appels and oranges",
            Method = "Put the apples in a bowl. \nAdd the oranges into the bowl.",
            AuthorId = 1
        };

        var recipeCreateDTO2 = new RecipeCreateDTO{
            Title = "Apples and Oranges fruit cup",
            IsPublic = false,
            Description = "A nice cup of appels and oranges",
            Method = "Slice the apples and oranges.\nPut the apples in a cup.\nAdd the oranges into the cup.",
            AuthorId = 1
        };

        var recipeCreateDTO3 = new RecipeCreateDTO{
            Title = "Apples and Oranges fruit blend",
            IsPublic = false,
            Description = "A nice blend of appels and oranges",
            Method = "Put the apples and oranges into a blender and blend for 1 minute.",
            AuthorId = 2
        };

        //Act
        await _repo.CreateAsync(recipeCreateDTO1);
        await _repo.CreateAsync(recipeCreateDTO2);
        await _repo.CreateAsync(recipeCreateDTO3);
        var list = await _repo.ReadAllByAuthorIDAsync(1);

        //Assert
        Assert.NotNull(list);
        Assert.Collection<RecipeDTO>
        (
            list,
            item => {
                Assert.Equal(1, item.Id);
                Assert.Equal(recipeCreateDTO1.Title, item.Title);
                Assert.Equal(recipeCreateDTO1.IsPublic, item.IsPublic);
                Assert.Equal(recipeCreateDTO1.Description, item.Description);
                Assert.Equal(recipeCreateDTO1.Method, item.Method);
                Assert.Equal(recipeCreateDTO1.AuthorId, item.AuthorId);
                Assert.True(Enumerable.SequenceEqual(recipeCreateDTO1.CategoryIDs ?? new List<int>{}, item.CategoryIDs));
                
            },
            item => {
                Assert.Equal(2, item.Id);
                Assert.Equal(recipeCreateDTO2.Title, item.Title);
                Assert.Equal(recipeCreateDTO2.IsPublic, item.IsPublic);
                Assert.Equal(recipeCreateDTO2.Description, item.Description);
                Assert.Equal(recipeCreateDTO2.Method, item.Method);
                Assert.Equal(recipeCreateDTO2.AuthorId, item.AuthorId);
                Assert.True(Enumerable.SequenceEqual(recipeCreateDTO2.CategoryIDs ?? new List<int>{}, item.CategoryIDs));
            }
        );
    }


    [Fact]
    public async void ReadAllByMealId_returns_all()
    {

        //Arrange
        var recipe1 = new Recipe(
            "Apples and Oranges",
            false,
            "A nice bowl of appels and oranges",
            "Put the apples in a bowl. \nAdd the oranges into the bowl.",
            1,
            new List<Category>{_fruit, _vegan},
            false,
            false,
            false,
            true
        );

        var recipe2 = new Recipe(
            "Apples and Oranges fruit cup",
            false,
            "A nice apple and oranges fruit cup",
            "Slice the apples and oranges.\nPut the apples in a cup.\nAdd the oranges into the cup.",
            1,
            new List<Category>{_fruit, _vegan},
            false,
            false,
            false,
            true
        );

        var recipe3 = new Recipe(
            "Apples and Oranges fruit blend",
            false,
            "A nice blend of appels and oranges",
            "Put the apples and oranges into a blender and blend for 1 minute.",
            2,
            new List<Category>{_fruit, _vegan},
            false,
            false,
            false,
            true
        );

        _context.Recipes.Add(recipe1);
        _context.Recipes.Add(recipe2);
        _context.Recipes.Add(recipe3);
        _context.SaveChanges();

        RecipeMeal rm1 = new RecipeMeal(recipe1, _christmas, 2.0f);
        RecipeMeal rm2 = new RecipeMeal(recipe2, _christmas, 5.0f);
        _context.RecipeMeals.Add(rm1);
        _context.RecipeMeals.Add(rm2);
        _context.SaveChanges();

        //Act
        var recipes = await _repo.ReadAllByMealId(1);

        //Assert
        Assert.NotNull(recipes);
        Assert.Collection<RecipeAmountDTO>
        (
            recipes,
            item => {
                Assert.Equal(rm1.Amount, item.Amount);
                Assert.Equal(rm1.Recipe.Id, item.Recipe.Id);
            },
            item => {
                Assert.Equal(rm2.Amount, item.Amount);
                Assert.Equal(rm2.Recipe.Id, item.Recipe.Id);
            }
        );

    }

    [Fact]
    public async void ReadAllByAuthorID_returns_empty_list()
    {
        //Act
        var list = await _repo.ReadAllAsync();

        //Assert
        Assert.Empty(list.AsEnumerable());
    }


    [Fact]
    public async void ReadByAuthorIDAndTitle_returns_RecipeDTO()
    {
        //Arrange
        var recipeCreateDTO1 = new RecipeCreateDTO{
            Title = "Apples and Oranges",
            IsPublic = false,
            Description = "A nice bowl of appels and oranges",
            Method = "Put the apples in a bowl. \nAdd the oranges into the bowl.",
            AuthorId = 1
        };

        var recipeCreateDTO2 = new RecipeCreateDTO{
            Title = "Apples and Oranges fruit cup",
            IsPublic = false,
            Description = "A nice cup of appels and oranges",
            Method = "Slice the apples and oranges.\nPut the apples in a cup.\nAdd the oranges into the cup.",
            AuthorId = 1
        };

        var recipeCreateDTO3 = new RecipeCreateDTO{
            Title = "Apples and Oranges fruit blend",
            IsPublic = false,
            Description = "A nice blend of appels and oranges",
            Method = "Put the apples and oranges into a blender and blend for 1 minute.",
            AuthorId = 2
        };

        //Act
        await _repo.CreateAsync(recipeCreateDTO1);
        await _repo.CreateAsync(recipeCreateDTO2);
        await _repo.CreateAsync(recipeCreateDTO3);
        var result = await _repo.ReadByAuthorIDAndTitle(1, recipeCreateDTO1.Title);
        

        //Assert
        Assert.True(result.IsSome);
        Assert.Equal(1, result.Value.Id);
        Assert.Equal(recipeCreateDTO1.Title, result.Value.Title);
        Assert.Equal(recipeCreateDTO1.IsPublic, result.Value.IsPublic);
        Assert.Equal(recipeCreateDTO1.Description, result.Value.Description);
        Assert.Equal(recipeCreateDTO1.Method, result.Value.Method);
        Assert.Equal(recipeCreateDTO1.AuthorId, result.Value.AuthorId);
        Assert.True(Enumerable.SequenceEqual(recipeCreateDTO1.CategoryIDs ?? new List<int>{}, result.Value.CategoryIDs));
    }

    [Fact]
    public async void ReadByAuthorIDAndTitle_returns_null()
    {
        //Act
        var result = await _repo.ReadByAuthorIDAndTitle(1, "This recipe is not supposed to exist");
        
        //Assert
        Assert.True(result.IsNone);
    }




    [Fact]
    public async void ReadAllByCategoryID_returns_all_by_category()
    {
        //Arrange
        var recipeCreateDTO1 = new RecipeCreateDTO{
            Title = "Apples and Oranges",
            IsPublic = false,
            Description = "A nice bowl of appels and oranges",
            Method = "Put the apples in a bowl. \nAdd the oranges into the bowl.",
            AuthorId = 1,
            CategoryIDs = new List<int>{1}
        };

        var recipeCreateDTO2 = new RecipeCreateDTO{
            Title = "Apples and Oranges fruit cup",
            IsPublic = false,
            Description = "A nice cup of appels and oranges",
            Method = "Slice the apples and oranges.\nPut the apples in a cup.\nAdd the oranges into the cup.",
            AuthorId = 1,
            CategoryIDs = new List<int>{1, 2}
        };

        var recipeCreateDTO3 = new RecipeCreateDTO{
            Title = "Apples and Oranges fruit blend",
            IsPublic = false,
            Description = "A nice blend of appels and oranges",
            Method = "Put the apples and oranges into a blender and blend for 1 minute.",
            AuthorId = 2,
            CategoryIDs = new List<int>{2}

        };

        var categoryVegan = new Category("vegan");
        var categoryVegetarian = new Category("vegetarian");
        _context.Categories.Add(categoryVegan);
        _context.Categories.Add(categoryVegetarian);
        await _context.SaveChangesAsync();

        //Act
        await _repo.CreateAsync(recipeCreateDTO1);
        await _repo.CreateAsync(recipeCreateDTO2);
        var list = await _repo.ReadAllAsync();

        //Assert
        Assert.NotNull(list);
        Assert.Collection<RecipeDTO>
        (
            list,
            item => {
                Assert.Equal(1, item.Id);
                Assert.Equal(recipeCreateDTO1.Title, item.Title);
                Assert.Equal(recipeCreateDTO1.IsPublic, item.IsPublic);
                Assert.Equal(recipeCreateDTO1.Description, item.Description);
                Assert.Equal(recipeCreateDTO1.Method, item.Method);
                Assert.Equal(recipeCreateDTO1.AuthorId, item.AuthorId);
                Assert.True(Enumerable.SequenceEqual(recipeCreateDTO1.CategoryIDs ?? new List<int>{}, item.CategoryIDs));
            },
            item => {
                Assert.Equal(2, item.Id);
                Assert.Equal(recipeCreateDTO2.Title, item.Title);
                Assert.Equal(recipeCreateDTO2.IsPublic, item.IsPublic);
                Assert.Equal(recipeCreateDTO2.Description, item.Description);
                Assert.Equal(recipeCreateDTO2.Method, item.Method);
                Assert.Equal(recipeCreateDTO2.AuthorId, item.AuthorId);
                Assert.True(Enumerable.SequenceEqual(recipeCreateDTO2.CategoryIDs ?? new List<int>{}, item.CategoryIDs));
            }
        );
    }

    [Fact]
    public async void ReadSavedBySearchWord_returns_list()
    {
        //Arrange
        var recipe1 = new Recipe
        {
            Title = "Apples and Oranges box",
            IsPublic = false,
            Description = "A nice bowl of appels and oranges",
            Method = "Put the apples in a bowl. \nAdd the oranges into the bowl.",
            AuthorId = 1
        };

        var recipe2 = new Recipe
        {
            Title = "Apples and Oranges fruit cup",
            IsPublic = true,
            Description = "A nice cup of appels and oranges",
            Method = "Slice the apples and oranges.\nPut the apples in a cup.\nAdd the oranges into the cup.",
            AuthorId = 2
        };

        var recipe3 = new Recipe
        {
            Title = "Apples and Oranges fruit blend",
            IsPublic = false,
            Description = "A nice blend of appels and oranges",
            Method = "Put the apples and oranges into a blender and blend for 1 minute.",
            AuthorId = 2,
        };

        var recipe4 = new Recipe
        {
            Title = "Apples and Oranges",
            IsPublic = false,
            Description = "A nice blend of appels and oranges",
            Method = "Put the apples and oranges into a blender and blend for 1 minute.",
            AuthorId = 1,
        };

        _context.Add(recipe1);
        _context.Add(recipe2);
        _context.Add(recipe3);
        _context.Add(recipe4);
        await _context.SaveChangesAsync();
        User user = _context.Users.Where(u => u.Id == 1).FirstOrDefault()!;
        user.SavedRecipes = new List<Recipe>{recipe1, recipe2, recipe4};
        await _context.SaveChangesAsync();

        //Act
        var list = await _repo.ReadSavedBySearchWord("box", 1);
        var emptysearchwordlist = await _repo.ReadSavedBySearchWord("", 1);
        var _list = await _repo.ReadSavedBySearchWord("_", 1);

        //Assert
        Assert.NotNull(list);
        Assert.Collection<RecipeDTO>
        (
            list,
            item => {
                Assert.Equal(1, item.Id);
                Assert.Equal(recipe1.Title, item.Title);
                Assert.Equal(recipe1.IsPublic, item.IsPublic);
                Assert.Equal(recipe1.Description, item.Description);
                Assert.Equal(recipe1.Method, item.Method);
                Assert.Equal(recipe1.AuthorId, item.AuthorId);
            }
        );

        Assert.NotNull(_list);
        Assert.Collection<RecipeDTO>
        (
            _list,
            item => Assert.Equal(4, item.Id),
            item => Assert.Equal(1, item.Id),
            item => Assert.Equal(2, item.Id)
        );

        Assert.NotNull(emptysearchwordlist);
        Assert.Collection<RecipeDTO>
        (
            _list,
            item => Assert.Equal(4, item.Id),
            item => Assert.Equal(1, item.Id),
            item => Assert.Equal(2, item.Id)
        );
    }

    [Fact]
    public async void ReadPublicBySearchWord_returns_list()
    {
        //Arrange
        var recipe1 = new Recipe
        {
            Title = "Apples and Oranges fruit cup",
            IsPublic = true,
            Description = "A nice cup of appels and oranges",
            Method = "Slice the apples and oranges.\nPut the apples in a cup.\nAdd the oranges into the cup.",
            AuthorId = 2
        };

        var recipe2 = new Recipe
        {
            Title = "Apples and Oranges box",
            IsPublic = false,
            Description = "A nice bowl of appels and oranges",
            Method = "Put the apples in a bowl. \nAdd the oranges into the bowl.",
            AuthorId = 1
        };

        var recipe3 = new Recipe
        {
            Title = "Apples and Oranges fruit blend",
            IsPublic = false,
            Description = "A nice blend of appels and oranges",
            Method = "Put the apples and oranges into a blender and blend for 1 minute.",
            AuthorId = 2,
        };
        _context.Add(recipe1);
        _context.Add(recipe2);
        _context.Add(recipe3);
        await _context.SaveChangesAsync();
        User user = _context.Users.Where(u => u.Id == 1).FirstOrDefault()!;
        user.SavedRecipes = new List<Recipe>{recipe1, recipe2};
        await _context.SaveChangesAsync();

        //Act
        var list = await _repo.ReadPublicBySearchWord("fruit");
        var emptysearchwordlist = await _repo.ReadPublicBySearchWord("");
        var _list = await _repo.ReadPublicBySearchWord("_");

        //Assert
        Assert.NotNull(list);
        Assert.Collection<RecipeDTO>
        (
            list,
            item => {
                Assert.Equal(1, item.Id);
                Assert.Equal(recipe1.Title, item.Title);
                Assert.Equal(recipe1.IsPublic, item.IsPublic);
                Assert.Equal(recipe1.Description, item.Description);
                Assert.Equal(recipe1.Method, item.Method);
                Assert.Equal(recipe1.AuthorId, item.AuthorId);
            }
        );

        Assert.NotNull(_list);
        Assert.Collection<RecipeDTO>
        (
            _list,
            item => Assert.Equal(1, item.Id)
        );

        Assert.NotNull(emptysearchwordlist);
        Assert.Collection<RecipeDTO>
        (
            _list,
            item => Assert.Equal(1, item.Id)
        );
    }

    [Fact]
    public async void ReadAllPublic_returns_list()
    {
        //Arrange
        var recipe1 = new Recipe
        {
            Title = "Apples and Oranges fruit cup",
            IsPublic = true,
            Description = "A nice cup of appels and oranges",
            Method = "Slice the apples and oranges.\nPut the apples in a cup.\nAdd the oranges into the cup.",
            AuthorId = 2
        };

        var recipe2 = new Recipe
        {
            Title = "Apples and Oranges box",
            IsPublic = false,
            Description = "A nice bowl of appels and oranges",
            Method = "Put the apples in a bowl. \nAdd the oranges into the bowl.",
            AuthorId = 1
        };

        var recipe3 = new Recipe
        {
            Title = "Apples and Oranges fruit blend",
            IsPublic = false,
            Description = "A nice blend of appels and oranges",
            Method = "Put the apples and oranges into a blender and blend for 1 minute.",
            AuthorId = 2,
        };
        _context.Add(recipe1);
        _context.Add(recipe2);
        _context.Add(recipe3);
        await _context.SaveChangesAsync();
        User user = _context.Users.Where(u => u.Id == 1).FirstOrDefault()!;
        user.SavedRecipes = new List<Recipe>{recipe1, recipe2};
        await _context.SaveChangesAsync();

        //Act
        var list = await _repo.ReadAllPublicAsync();

        //Assert
        Assert.NotNull(list);
        Assert.Collection<RecipeDTO>
        (
            list,
            item => {
                Assert.Equal(1, item.Id);
                Assert.Equal(recipe1.Title, item.Title);
                Assert.Equal(recipe1.IsPublic, item.IsPublic);
                Assert.Equal(recipe1.Description, item.Description);
                Assert.Equal(recipe1.Method, item.Method);
                Assert.Equal(recipe1.AuthorId, item.AuthorId);
            }
        );
    }
}