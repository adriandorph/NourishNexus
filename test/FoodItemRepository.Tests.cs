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
        Assert.Equal(foodItemCreateDTO.Calories, entity.Calories);
        Assert.Equal(foodItemCreateDTO.Protein, entity.Protein);

        Assert.Equal(Response.Created, response);
        Assert.Equal(entity.Id, foodItemDTO.Id);
        Assert.Equal(foodItemCreateDTO.Name, foodItemDTO.Name);
        Assert.Equal(foodItemCreateDTO.Calories, foodItemDTO.Calories);
        Assert.Equal(foodItemCreateDTO.Protein, foodItemDTO.Protein);
    }

    [Fact]
    public async void CreateAsync_FoodItem_with_same_Name_Conflict()
    {
        //Arrange
        var foodItemCreateDTO = new FoodItemCreateDTO{
            Name = "Apple",
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
        Assert.Equal(foodItemCreateDTO.Calories, entity.Calories);
        Assert.Equal(foodItemCreateDTO.Protein, entity.Protein);
    }

    [Fact]
    public async void Update_Updated_Calories()
    {
        //Arrange
        var foodItemCreateDTO = new FoodItemCreateDTO{
            Name = "Apple",
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
        Assert.Equal(foodItemUpdateDTO.Calories, entity.Calories);
    }

    [Fact]
    public async void Update_Updated_Protein()
    {
        //Arrange
        var foodItemCreateDTO = new FoodItemCreateDTO{
            Name = "Apple",
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
        Assert.Equal(foodItemCreateDTO.Calories, entity.Calories);
        Assert.Equal(foodItemUpdateDTO.Protein, entity.Protein);
    }

    [Fact]
    public async void Update_FoodItem_with_same_Name_and_different_Id_Conflict()
    {
        //Arrange
        var foodItemCreateDTO1 = new FoodItemCreateDTO{
            Name = "Apple",
            Calories = 100.0f,
            Protein = 0.3f
        };

        var foodItemCreateDTO2 = new FoodItemCreateDTO{
            Name = "Orange",
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
            Calories = 100.0f,
            Protein = 0.3f
        };

        var foodItemCreateDTO2 = new FoodItemCreateDTO{
            Name = "Orange",
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
                Assert.Equal(foodItemCreateDTO1.Calories,item.Calories);
                Assert.Equal(foodItemCreateDTO1.Protein,item.Protein);
            },
            item => {
                Assert.Equal(foodItemCreateDTO2.Name,item.Name);
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










    
    [Fact]
    public async void Update_Updated_Carbohydrates()
    {
        //Arrange
        var foodItemCreateDTO = new FoodItemCreateDTO{
            Name = "Apple",
            Carbohydrates = 100.0f,
        };

        var foodItemUpdateDTO = new FoodItemUpdateDTO
        {
            Id = 1,
            Carbohydrates = 200.0f
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
        Assert.Equal(foodItemUpdateDTO.Carbohydrates, entity.Carbohydrates);
    }

    [Fact]
    public async void Update_Updated_Sugars()
    {
        //Arrange
        var foodItemCreateDTO = new FoodItemCreateDTO{
            Name = "Apple",
            Sugars = 100.0f,
        };

        var foodItemUpdateDTO = new FoodItemUpdateDTO
        {
            Id = 1,
            Sugars = 200.0f
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
        Assert.Equal(foodItemUpdateDTO.Sugars, entity.Sugars);
    }

    [Fact]
    public async void Update_Updated_Fibres()
    {
        //Arrange
        var foodItemCreateDTO = new FoodItemCreateDTO{
            Name = "Apple",
            Fibres = 100.0f,
        };

        var foodItemUpdateDTO = new FoodItemUpdateDTO
        {
            Id = 1,
            Fibres = 200.0f
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
        Assert.Equal(foodItemUpdateDTO.Fibres, entity.Fibres);
    }

    [Fact]
    public async void Update_Updated_TotalFat()
    {
        //Arrange
        var foodItemCreateDTO = new FoodItemCreateDTO{
            Name = "Apple",
            TotalFat = 100.0f,
        };

        var foodItemUpdateDTO = new FoodItemUpdateDTO
        {
            Id = 1,
            TotalFat = 200.0f
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
        Assert.Equal(foodItemUpdateDTO.TotalFat, entity.TotalFat);
    }

    [Fact]
    public async void Update_Updated_SaturatedFat()
    {
        //Arrange
        var foodItemCreateDTO = new FoodItemCreateDTO{
            Name = "Apple",
            SaturatedFat = 100.0f,
        };

        var foodItemUpdateDTO = new FoodItemUpdateDTO
        {
            Id = 1,
            SaturatedFat = 200.0f
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
        Assert.Equal(foodItemUpdateDTO.SaturatedFat, entity.SaturatedFat);
    }

    [Fact]
    public async void Update_Updated_MonounsaturatedFat()
    {
        //Arrange
        var foodItemCreateDTO = new FoodItemCreateDTO{
            Name = "Apple",
            MonounsaturatedFat = 100.0f,
        };

        var foodItemUpdateDTO = new FoodItemUpdateDTO
        {
            Id = 1,
            MonounsaturatedFat = 200.0f
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
        Assert.Equal(foodItemUpdateDTO.MonounsaturatedFat, entity.MonounsaturatedFat);
    }

    [Fact]
    public async void Update_Updated_PolyunsaturatedFat()
    {
        //Arrange
        var foodItemCreateDTO = new FoodItemCreateDTO{
            Name = "Apple",
            PolyunsaturatedFat = 100.0f,
        };

        var foodItemUpdateDTO = new FoodItemUpdateDTO
        {
            Id = 1,
            PolyunsaturatedFat = 200.0f
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
        Assert.Equal(foodItemUpdateDTO.PolyunsaturatedFat, entity.PolyunsaturatedFat);
    }

    [Fact]
    public async void Update_Updated_TransFat()
    {
        //Arrange
        var foodItemCreateDTO = new FoodItemCreateDTO{
            Name = "Apple",
            TransFat = 100.0f,
        };

        var foodItemUpdateDTO = new FoodItemUpdateDTO
        {
            Id = 1,
            TransFat = 200.0f
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
        Assert.Equal(foodItemUpdateDTO.TransFat, entity.TransFat);
    }

    [Fact]
    public async void Update_Updated_VitaminA()
    {
        //Arrange
        var foodItemCreateDTO = new FoodItemCreateDTO{
            Name = "Apple",
            VitaminA = 100.0f,
        };

        var foodItemUpdateDTO = new FoodItemUpdateDTO
        {
            Id = 1,
            VitaminA = 200.0f
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
        Assert.Equal(foodItemUpdateDTO.VitaminA, entity.VitaminA);
    }

    [Fact]
    public async void Update_Updated_VitaminB6()
    {
        //Arrange
        var foodItemCreateDTO = new FoodItemCreateDTO{
            Name = "Apple",
            VitaminB6 = 100.0f,
        };

        var foodItemUpdateDTO = new FoodItemUpdateDTO
        {
            Id = 1,
            VitaminB6 = 200.0f
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
        Assert.Equal(foodItemUpdateDTO.VitaminB6, entity.VitaminB6);
    }

    [Fact]
    public async void Update_Updated_VitaminB12()
    {
        //Arrange
        var foodItemCreateDTO = new FoodItemCreateDTO{
            Name = "Apple",
            VitaminB12 = 100.0f,
        };

        var foodItemUpdateDTO = new FoodItemUpdateDTO
        {
            Id = 1,
            VitaminB12 = 200.0f
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
        Assert.Equal(foodItemUpdateDTO.VitaminB12, entity.VitaminB12);
    }

    [Fact]
    public async void Update_Updated_VitaminC()
    {
        //Arrange
        var foodItemCreateDTO = new FoodItemCreateDTO{
            Name = "Apple",
            VitaminC = 100.0f,
        };

        var foodItemUpdateDTO = new FoodItemUpdateDTO
        {
            Id = 1,
            VitaminC = 200.0f
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
        Assert.Equal(foodItemUpdateDTO.VitaminC, entity.VitaminC);
    }

    [Fact]
    public async void Update_Updated_VitaminD()
    {
        //Arrange
        var foodItemCreateDTO = new FoodItemCreateDTO{
            Name = "Apple",
            VitaminD = 100.0f,
        };

        var foodItemUpdateDTO = new FoodItemUpdateDTO
        {
            Id = 1,
            VitaminD = 200.0f
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
        Assert.Equal(foodItemUpdateDTO.VitaminD, entity.VitaminD);
    }

    [Fact]
    public async void Update_Updated_VitaminE()
    {
        //Arrange
        var foodItemCreateDTO = new FoodItemCreateDTO{
            Name = "Apple",
            VitaminE = 100.0f,
        };

        var foodItemUpdateDTO = new FoodItemUpdateDTO
        {
            Id = 1,
            VitaminE = 200.0f
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
        Assert.Equal(foodItemUpdateDTO.VitaminE, entity.VitaminE);
    }

    [Fact]
    public async void Update_Updated_VitaminK1()
    {
        //Arrange
        var foodItemCreateDTO = new FoodItemCreateDTO{
            Name = "Apple",
            VitaminK1 = 100.0f,
        };

        var foodItemUpdateDTO = new FoodItemUpdateDTO
        {
            Id = 1,
            VitaminK1 = 200.0f
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
        Assert.Equal(foodItemUpdateDTO.VitaminK1, entity.VitaminK1);
    }

    [Fact]
    public async void Update_Updated_Thiamin()
    {
        //Arrange
        var foodItemCreateDTO = new FoodItemCreateDTO{
            Name = "Apple",
            Thiamin = 100.0f,
        };

        var foodItemUpdateDTO = new FoodItemUpdateDTO
        {
            Id = 1,
            Thiamin = 200.0f
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
        Assert.Equal(foodItemUpdateDTO.Thiamin, entity.Thiamin);
    }

    [Fact]
    public async void Update_Updated_Riboflavin()
    {
        //Arrange
        var foodItemCreateDTO = new FoodItemCreateDTO{
            Name = "Apple",
            Riboflavin = 100.0f,
        };

        var foodItemUpdateDTO = new FoodItemUpdateDTO
        {
            Id = 1,
            Riboflavin = 200.0f
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
        Assert.Equal(foodItemUpdateDTO.Riboflavin, entity.Riboflavin);
    }

    [Fact]
    public async void Update_Updated_Niacin()
    {
        //Arrange
        var foodItemCreateDTO = new FoodItemCreateDTO{
            Name = "Apple",
            Niacin = 100.0f,
        };

        var foodItemUpdateDTO = new FoodItemUpdateDTO
        {
            Id = 1,
            Niacin = 200.0f
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
        Assert.Equal(foodItemUpdateDTO.Niacin, entity.Niacin);
    }

    [Fact]
    public async void Update_Updated_Folate()
    {
        //Arrange
        var foodItemCreateDTO = new FoodItemCreateDTO{
            Name = "Apple",
            Folate = 100.0f,
        };

        var foodItemUpdateDTO = new FoodItemUpdateDTO
        {
            Id = 1,
            Folate = 200.0f
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
        Assert.Equal(foodItemUpdateDTO.Folate, entity.Folate);
    }

    [Fact]
    public async void Update_Updated_Salt()
    {
        //Arrange
        var foodItemCreateDTO = new FoodItemCreateDTO{
            Name = "Apple",
            Salt = 100.0f,
        };

        var foodItemUpdateDTO = new FoodItemUpdateDTO
        {
            Id = 1,
            Salt = 200.0f
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
        Assert.Equal(foodItemUpdateDTO.Salt, entity.Salt);
    }

    [Fact]
    public async void Update_Updated_Potassium()
    {
        //Arrange
        var foodItemCreateDTO = new FoodItemCreateDTO{
            Name = "Apple",
            Potassium = 100.0f,
        };

        var foodItemUpdateDTO = new FoodItemUpdateDTO
        {
            Id = 1,
            Potassium = 200.0f
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
        Assert.Equal(foodItemUpdateDTO.Potassium, entity.Potassium);
    }

    [Fact]
    public async void Update_Updated_Magnesium()
    {
        //Arrange
        var foodItemCreateDTO = new FoodItemCreateDTO{
            Name = "Apple",
            Magnesium = 100.0f,
        };

        var foodItemUpdateDTO = new FoodItemUpdateDTO
        {
            Id = 1,
            Magnesium = 200.0f
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
        Assert.Equal(foodItemUpdateDTO.Magnesium, entity.Magnesium);
    }

    [Fact]
    public async void Update_Updated_Iron()
    {
        //Arrange
        var foodItemCreateDTO = new FoodItemCreateDTO{
            Name = "Apple",
            Iron = 100.0f,
        };

        var foodItemUpdateDTO = new FoodItemUpdateDTO
        {
            Id = 1,
            Iron = 200.0f
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
        Assert.Equal(foodItemUpdateDTO.Iron, entity.Iron);
    }

    [Fact]
    public async void Update_Updated_Zinc()
    {
        //Arrange
        var foodItemCreateDTO = new FoodItemCreateDTO{
            Name = "Apple",
            Zinc = 100.0f,
        };

        var foodItemUpdateDTO = new FoodItemUpdateDTO
        {
            Id = 1,
            Zinc = 200.0f
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
        Assert.Equal(foodItemUpdateDTO.Zinc, entity.Zinc);
    }

    [Fact]
    public async void Update_Updated_Phosphorus()
    {
        //Arrange
        var foodItemCreateDTO = new FoodItemCreateDTO{
            Name = "Apple",
            Phosphorus = 100.0f,
        };

        var foodItemUpdateDTO = new FoodItemUpdateDTO
        {
            Id = 1,
            Phosphorus = 200.0f
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
        Assert.Equal(foodItemUpdateDTO.Phosphorus, entity.Phosphorus);
    }

    [Fact]
    public async void Update_Updated_Copper()
    {
        //Arrange
        var foodItemCreateDTO = new FoodItemCreateDTO{
            Name = "Apple",
            Copper = 100.0f,
        };

        var foodItemUpdateDTO = new FoodItemUpdateDTO
        {
            Id = 1,
            Copper = 200.0f
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
        Assert.Equal(foodItemUpdateDTO.Copper, entity.Copper);
    }

    [Fact]
    public async void Update_Updated_Iodine()
    {
        //Arrange
        var foodItemCreateDTO = new FoodItemCreateDTO{
            Name = "Apple",
            Iodine = 100.0f,
        };

        var foodItemUpdateDTO = new FoodItemUpdateDTO
        {
            Id = 1,
            Iodine = 200.0f
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
        Assert.Equal(foodItemUpdateDTO.Iodine, entity.Iodine);
    }

    [Fact]
    public async void Update_Updated_Nickel()
    {
        //Arrange
        var foodItemCreateDTO = new FoodItemCreateDTO{
            Name = "Apple",
            Nickel = 100.0f,
        };

        var foodItemUpdateDTO = new FoodItemUpdateDTO
        {
            Id = 1,
            Nickel = 200.0f
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
        Assert.Equal(foodItemUpdateDTO.Nickel, entity.Nickel);
    }

    [Fact]
    public async void Update_Updated_Selen()
    {
        //Arrange
        var foodItemCreateDTO = new FoodItemCreateDTO{
            Name = "Apple",
            Selen = 100.0f,
        };

        var foodItemUpdateDTO = new FoodItemUpdateDTO
        {
            Id = 1,
            Selen = 200.0f
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
        Assert.Equal(foodItemUpdateDTO.Selen, entity.Selen);
    }

    [Fact]
    public async void Update_Updated_Calcium()
    {
        //Arrange
        var foodItemCreateDTO = new FoodItemCreateDTO{
            Name = "Apple",
            Calcium = 100.0f,
        };

        var foodItemUpdateDTO = new FoodItemUpdateDTO
        {
            Id = 1,
            Calcium = 200.0f
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
        Assert.Equal(foodItemUpdateDTO.Calcium, entity.Calcium);
    }
}