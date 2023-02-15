namespace test;

public class FoodItemRepositoryTests
{
    NourishNexusContext _context;
    FoodItemRepository _repo;
    //Setup
    public FoodItemRepositoryTests()
    {
        var connection = new SqliteConnection("Data Source=:memory:");
        connection.Open();

        var builder = new DbContextOptionsBuilder<NourishNexusContext>();
        builder.UseSqlite(connection);
        builder.EnableSensitiveDataLogging();

        var context = new NourishNexusContext(builder.Options);
        context.Database.EnsureCreated();

        _context = context;
        _repo = new FoodItemRepository(_context);
    }


    //Create

    [Fact]
    public async void CreateAsync_Created()
    {
        //Arrange
        var foodItemCreateDTO = new FoodItemCreateDTO{
            Name = "Apple",
            Unit = Unit.g,
            Calories = 100.0f,
            Protein = 0.3f
        };

        //Act
        (Response response, FoodItemDTO foodItemDTO) = await _repo.CreateAsync(foodItemCreateDTO);

        var entity = await _context.FoodItems
            .Where(fi => fi.Id == 1)
            .FirstOrDefaultAsync();

        //Assert
        Assert.NotNull(entity);
        Assert.Equal(1, entity.Id);
        Assert.Equal(foodItemCreateDTO.Name, entity.Name);
        Assert.Equal(foodItemCreateDTO.Unit, entity.Unit);
        Assert.Equal(foodItemCreateDTO.Calories, entity.Calories);
        Assert.Equal(foodItemCreateDTO.Protein, entity.Protein);

        Assert.Equal(Response.Created, response);
        Assert.Equal(entity.Id, foodItemDTO.Id);
        Assert.Equal(foodItemCreateDTO.Name, foodItemDTO.Name);
        Assert.Equal(foodItemCreateDTO.Unit, foodItemDTO.Unit);
        Assert.Equal(foodItemCreateDTO.Calories, foodItemDTO.Calories);
        Assert.Equal(foodItemCreateDTO.Protein, foodItemDTO.Protein);
    }

    [Fact]
    public async void CreateAsync_FoodItem_with_same_Name_Conflict()
    {
        //Arrange
        var foodItemCreateDTO = new FoodItemCreateDTO{
            Name = "Apple",
            Unit = Unit.g,
            Calories = 100.0f,
            Protein = 0.3f
        };

        //Act
        await _repo.CreateAsync(foodItemCreateDTO);
        (Response response, FoodItemDTO foodItemDTO) = await _repo.CreateAsync(foodItemCreateDTO);

        //Assert
        Assert.Equal(Response.Conflict, response);
    }


    //Update

    [Fact]
    public async void Update_Updated_Name()
    {
        //Arrange
        var foodItemCreateDTO = new FoodItemCreateDTO{
            Name = "Apple",
            Unit = Unit.g,
            Calories = 100.0f,
            Protein = 0.3f
        };

        var foodItemUpdateDTO = new FoodItemUpdateDTO
        {
            Id = 1,
            Name = "Orange"
        };

        //Act
        await _repo.CreateAsync(foodItemCreateDTO);
        var response = await _repo.UpdateAsync(foodItemUpdateDTO);
        var entity = await _context.FoodItems
            .Where(u => u.Name == foodItemUpdateDTO.Name)
            .FirstOrDefaultAsync();

        //Assert
        Assert.Equal(Response.Updated, response);
        Assert.NotNull(entity);
        Assert.Equal(1, entity.Id);
        Assert.Equal(foodItemUpdateDTO.Name, entity.Name);
        Assert.Equal(foodItemCreateDTO.Unit, entity.Unit);
        Assert.Equal(foodItemCreateDTO.Calories, entity.Calories);
        Assert.Equal(foodItemCreateDTO.Protein, entity.Protein);
    }

    [Fact]
    public async void Update_Updated_Unit()
    {
        //Arrange
        var foodItemCreateDTO = new FoodItemCreateDTO{
            Name = "Apple",
            Unit = Unit.g,
            Calories = 100.0f,
            Protein = 0.3f
        };

        var foodItemUpdateDTO = new FoodItemUpdateDTO
        {
            Id = 1,
            Unit = Unit.ml
        };

        //Act
        await _repo.CreateAsync(foodItemCreateDTO);
        var response = await _repo.UpdateAsync(foodItemUpdateDTO);
        var entity = await _context.FoodItems
            .Where(fi => fi.Id == foodItemUpdateDTO.Id)
            .FirstOrDefaultAsync();

        //Assert
        Assert.Equal(Response.Updated, response);
        Assert.NotNull(entity);
        Assert.Equal(1, entity.Id);
        Assert.Equal(foodItemCreateDTO.Name, entity.Name);
        Assert.Equal(foodItemUpdateDTO.Unit, entity.Unit);
        Assert.Equal(foodItemCreateDTO.Calories, entity.Calories);
        Assert.Equal(foodItemCreateDTO.Protein, entity.Protein);
    }

    [Fact]
    public async void Update_Updated_Calories()
    {
        //Arrange
        var foodItemCreateDTO = new FoodItemCreateDTO{
            Name = "Apple",
            Unit = Unit.g,
            Calories = 100.0f,
            Protein = 0.3f
        };

        var foodItemUpdateDTO = new FoodItemUpdateDTO
        {
            Id = 1,
            Calories = 200.0f
        };

        //Act
        await _repo.CreateAsync(foodItemCreateDTO);
        var response = await _repo.UpdateAsync(foodItemUpdateDTO);
        var entity = await _context.FoodItems
            .Where(fi => fi.Id == foodItemUpdateDTO.Id)
            .FirstOrDefaultAsync();

        //Assert
        Assert.Equal(Response.Updated, response);
        Assert.NotNull(entity);
        Assert.Equal(1, entity.Id);
        Assert.Equal(foodItemCreateDTO.Name, entity.Name);
        Assert.Equal(foodItemCreateDTO.Unit, entity.Unit);
        Assert.Equal(foodItemUpdateDTO.Calories, entity.Calories);
        Assert.Equal(foodItemCreateDTO.Protein, entity.Protein);
    }

    [Fact]
    public async void Update_Updated_Protein()
    {
        //Arrange
        var foodItemCreateDTO = new FoodItemCreateDTO{
            Name = "Apple",
            Unit = Unit.g,
            Calories = 100.0f,
            Protein = 0.3f
        };

        var foodItemUpdateDTO = new FoodItemUpdateDTO
        {
            Id = 1,
            Protein = 1.0f
        };

        //Act
        await _repo.CreateAsync(foodItemCreateDTO);
        var response = await _repo.UpdateAsync(foodItemUpdateDTO);
        var entity = await _context.FoodItems
            .Where(fi => fi.Id == foodItemUpdateDTO.Id)
            .FirstOrDefaultAsync();

        //Assert
        Assert.Equal(Response.Updated, response);
        Assert.NotNull(entity);
        Assert.Equal(1, entity.Id);
        Assert.Equal(foodItemCreateDTO.Name, entity.Name);
        Assert.Equal(foodItemCreateDTO.Unit, entity.Unit);
        Assert.Equal(foodItemCreateDTO.Calories, entity.Calories);
        Assert.Equal(foodItemUpdateDTO.Protein, entity.Protein);
    }

    [Fact]
    public async void Update_FoodItem_with_same_Name_and_different_Id_Conflict()
    {
        //Arrange
        var foodItemCreateDTO1 = new FoodItemCreateDTO{
            Name = "Apple",
            Unit = Unit.g,
            Calories = 100.0f,
            Protein = 0.3f
        };

        var foodItemCreateDTO2 = new FoodItemCreateDTO{
            Name = "Orange",
            Unit = Unit.g,
            Calories = 47.0f,
            Protein = 0.9f
        };

        var foodItemUpdateDTO = new FoodItemUpdateDTO
        {
            Id = 1,
            Name = "Orange",
        };

        //Act
        await _repo.CreateAsync(foodItemCreateDTO1);
        await _repo.CreateAsync(foodItemCreateDTO2);
        var response = await _repo.UpdateAsync(foodItemUpdateDTO);

        //Assert
        Assert.Equal(Response.Conflict, response);
    }

    [Fact]
    public async void Update_NotFound()
    {
        //Arrange
        var FoodItemUpdateDTO = new FoodItemUpdateDTO
        {
            Id = 5,
            Name = "Pear"
        };

        //Act
        var response = await _repo.UpdateAsync(FoodItemUpdateDTO);

        //Assert
        Assert.Equal(Response.NotFound, response);
    }



    //Remove

    [Fact]
    public async void Remove_Deleted()
    {
        //Arrange
        var foodItemCreateDTO = new FoodItemCreateDTO{
            Name = "Apple",
            Unit = Unit.g,
            Calories = 100.0f,
            Protein = 0.3f
        };

        //Act
        await _repo.CreateAsync(foodItemCreateDTO);
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
    public async void ReadByID_returns_FoodItemDTO()
    {
        //Arrange
        var foodItemCreateDTO = new FoodItemCreateDTO{
            Name = "Apple",
            Unit = Unit.g,
            Calories = 100.0f,
            Protein = 0.3f
        };

        //Act
        await _repo.CreateAsync(foodItemCreateDTO);
        var result = await _repo.ReadByIDAsync(1);
        

        //Assert
        Assert.True(result.IsSome);
        Assert.Equal(1, result.Value.Id);
        Assert.Equal(foodItemCreateDTO.Name, result.Value.Name);
        Assert.Equal(foodItemCreateDTO.Unit, result.Value.Unit);
        Assert.Equal(foodItemCreateDTO.Calories, result.Value.Calories);
        Assert.Equal(foodItemCreateDTO.Protein, result.Value.Protein);
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
        var foodItemCreateDTO1 = new FoodItemCreateDTO{
            Name = "Apple",
            Unit = Unit.g,
            Calories = 100.0f,
            Protein = 0.3f
        };

        var foodItemCreateDTO2 = new FoodItemCreateDTO{
            Name = "Orange",
            Unit = Unit.g,
            Calories = 47.0f,
            Protein = 0.9f
        };

        //Act
        await _repo.CreateAsync(foodItemCreateDTO1);
        await _repo.CreateAsync(foodItemCreateDTO2);
        var list = await _repo.ReadAllAsync();

        //Assert
        Assert.NotNull(list);
        Assert.Collection<FoodItemDTO>
        (
            list,
            item => {
                Assert.Equal(foodItemCreateDTO1.Name,item.Name);
                Assert.Equal(foodItemCreateDTO1.Unit,item.Unit);
                Assert.Equal(foodItemCreateDTO1.Calories,item.Calories);
                Assert.Equal(foodItemCreateDTO1.Protein,item.Protein);
            },
            item => {
                Assert.Equal(foodItemCreateDTO2.Name,item.Name);
                Assert.Equal(foodItemCreateDTO2.Unit,item.Unit);
                Assert.Equal(foodItemCreateDTO2.Calories,item.Calories);
                Assert.Equal(foodItemCreateDTO2.Protein,item.Protein);
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
}