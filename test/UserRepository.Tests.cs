namespace test;

public class UserRepositoryTests
{
    NourishNexusContext _context;
    UserRepository _repo;
    //Setup
    public UserRepositoryTests()
    {
        var connection = new SqliteConnection("Data Source=:memory:");
        connection.Open();

        var builder = new DbContextOptionsBuilder<NourishNexusContext>();
        builder.UseSqlite(connection);
        builder.EnableSensitiveDataLogging();

        var context = new NourishNexusContext(builder.Options);
        context.Database.EnsureCreated();

        _context = context;
        _repo = new UserRepository(_context);
    }


    //Create

    [Fact]
    public async void CreateAsync_Created()
    {
        //Arrange
        var userCreateDTO = new UserCreateDTO{
            Email = "jhon@jhonson.com",
            Nickname = "Johnny"
        };

        //Act
        (Response response, UserDTO userDTO) = await _repo.CreateAsync(userCreateDTO);

        var entity = await _context.Users
            .Where(u => u.Email == userCreateDTO.Email)
            .FirstAsync();

        //Assert

        Assert.NotNull(entity);
        Assert.Equal(1, entity.Id);
        Assert.Equal(userCreateDTO.Email, entity.Email);
        Assert.Equal(userCreateDTO.Nickname, entity.Nickname);
        Assert.True(Enumerable.SequenceEqual(new List<int>(), entity.SavedRecipes.Select(r => r.Id).ToList()));

        Assert.Equal(Response.Created, response);
        Assert.Equal(entity.Id, userDTO.Id);
        Assert.Equal(userCreateDTO.Email, userDTO.Email);
        Assert.Equal(userCreateDTO.Nickname, userDTO.Nickname);
        Assert.True(Enumerable.SequenceEqual(new List<int>(), userDTO.SavedRecipeIds));
    }

    [Fact]
    public async void CreateAsync_User_with_same_email_Conflict()
    {
        //Arrange
        var userCreateDTO = new UserCreateDTO{
            Email = "jhon@jhonson.com",
            Nickname = "Johnny"
        };

        //Act
        await _repo.CreateAsync(userCreateDTO);
        (Response response, UserDTO userDTO) = await _repo.CreateAsync(userCreateDTO);

        //Assert
        Assert.Equal(Response.Conflict, response);
        Assert.Equal(-1, userDTO.Id);
        Assert.Equal(userCreateDTO.Email, userDTO.Email);
        Assert.Equal(userCreateDTO.Nickname, userDTO.Nickname);
        Assert.True(Enumerable.SequenceEqual(new List<int>(), userDTO.SavedRecipeIds));
    }


    //Update

    [Fact]
    public async void Update_Updated_Email()
    {
        //Arrange
        var userCreateDTO = new UserUpdateDTO
        {
            Email = "john@johnson.com",
            Nickname = "Johnny"
        };

        var userUpdateDTO = new UserUpdateDTO
        {
            Id = 1,
            Email = "john@bravo.com",
            Nickname = null
        };

        //Act
        await _repo.CreateAsync(userCreateDTO);
        var response = await _repo.UpdateAsync(userUpdateDTO);
        var entity = await _context.Users
            .Where(u => u.Email == userUpdateDTO.Email)
            .FirstOrDefaultAsync();

        //Assert
        Assert.Equal(Response.Updated, response);
        Assert.NotNull(entity);
        Assert.Equal(1, entity.Id);
        Assert.Equal(userUpdateDTO.Email, entity.Email);
        Assert.Equal(userCreateDTO.Nickname, entity.Nickname);
        Assert.True(Enumerable.SequenceEqual(new List<int>(), entity.SavedRecipes.Select(r => r.Id).ToList()));
    }

    [Fact]
    public async void Update_Updated_Nickname()
    {
        //Arrange
        var userCreateDTO = new UserUpdateDTO
        {
            Email = "john@johnson.com",
            Nickname = "Johnny"
        };

        var userUpdateDTO = new UserUpdateDTO
        {
            Id = 1,
            Email = null,
            Nickname = "Johnny Bravo"
        };

        //Act
        await _repo.CreateAsync(userCreateDTO);
        var response = await _repo.UpdateAsync(userUpdateDTO);
        var entity = await _context.Users
            .Where(u => u.Email == userCreateDTO.Email)
            .FirstOrDefaultAsync();

        //Assert
        Assert.Equal(Response.Updated, response);
        Assert.NotNull(entity);
        Assert.Equal(1, entity.Id);
        Assert.Equal(userCreateDTO.Email, entity.Email);
        Assert.Equal(userUpdateDTO.Nickname, entity.Nickname);
        Assert.True(Enumerable.SequenceEqual(new List<int>(), entity.SavedRecipes.Select(r => r.Id).ToList()));
    }

    [Fact]
    public async void Update_User_with_same_Email_and_different_Id_Conflict()
    {
        //Arrange
        var userCreateDTO1 = new UserUpdateDTO
        {
            Email = "john@johnson.com",
            Nickname = "Johnny"
        };

        var userCreateDTO2 = new UserUpdateDTO
        {
            Email = "pablo@pabloson.com",
            Nickname = "Pablo"
        };

        var userUpdateDTO = new UserUpdateDTO
        {
            Id = 1,
            Email = "pablo@pabloson.com",
            Nickname = "Johnny Bravo"
        };

        //Act
        await _repo.CreateAsync(userCreateDTO1);
        await _repo.CreateAsync(userCreateDTO2);
        var response = await _repo.UpdateAsync(userUpdateDTO);

        //Assert
        Assert.Equal(Response.Conflict, response);
    }


    [Fact]
    public async void Update_Updated_SavedRecipes()
    {
        //Arrange
        var userCreateDTO = new UserUpdateDTO
        {
            Email = "john@johnson.com",
            Nickname = "Johnny"
        };

        var recipe = new Recipe(
            "Apples and Oranges",
            false,
            "A nice bowl of appels and oranges",
            "Put the apples in a bowl. \nAdd the oranges into the bowl.",
            1,
            new List<Category>()
        );

        var userUpdateDTO = new UserUpdateDTO
        {
            Id = 1,
            SavedRecipeIds = new List<int>{1}
        };

        //Act
        await _repo.CreateAsync(userCreateDTO);
        await _context.Recipes.AddAsync(recipe);
        await _context.SaveChangesAsync();
        var response = await _repo.UpdateAsync(userUpdateDTO);
        var entity = await _context.Users
            .Where(u => u.Email == userCreateDTO.Email)
            .FirstOrDefaultAsync();

        //Assert
        Assert.Equal(Response.Updated, response);
        Assert.NotNull(entity);
        Assert.Equal(1, entity.Id);
        Assert.Equal(userCreateDTO.Email, entity.Email);
        Assert.Equal(userCreateDTO.Nickname, entity.Nickname);
        Assert.True(Enumerable.SequenceEqual(userUpdateDTO.SavedRecipeIds, entity.SavedRecipes.Select(r => r.Id).ToList()));
    }


    [Fact]
    public async void Update_NotFound()
    {
        //Arrange
        var userUpdateDTO = new UserUpdateDTO
        {
            Id = 5,
            Email = "john@johnson.com",
            Nickname = "Johnny Bravo"
        };

        //Act
        var response = await _repo.UpdateAsync(userUpdateDTO);

        //Assert
        Assert.Equal(Response.NotFound, response);
    }



    //Remove

    [Fact]
    public async void Remove_Deleted()
    {
        //Arrange
        var userCreateDTO = new UserCreateDTO{
            Email = "jhon@jhonson.com",
            Nickname = "Johnny"
        };

        //Act
        await _repo.CreateAsync(userCreateDTO);
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
    public async void ReadById_returns_UserDTO()
    {
        //Arrange
        var userCreateDTO = new UserCreateDTO{
            Email = "jhon@jhonson.com",
            Nickname = "Johnny"
        };

        var recipe = new Recipe(
            "Apples and Oranges",
            false,
            "A nice bowl of appels and oranges",
            "Put the apples in a bowl. \nAdd the oranges into the bowl.",
            1,
            new List<Category>()
        );

        var userUpdateDTO = new UserUpdateDTO
        {
            Id = 1,
            SavedRecipeIds = new List<int>{1}
        };

        //Act
        await _repo.CreateAsync(userCreateDTO);
        await _context.Recipes.AddAsync(recipe);
        await _context.SaveChangesAsync();
        await _repo.UpdateAsync(userUpdateDTO);
        
        var result = await _repo.ReadByIDAsync(1);
        

        //Assert
        Assert.True(result.IsSome);
        Assert.Equal(1, result.Value.Id);
        Assert.Equal(userCreateDTO.Email, result.Value.Email);
        Assert.Equal(userCreateDTO.Nickname, result.Value.Nickname);
        Assert.True(Enumerable.SequenceEqual(new List<int>{1}, result.Value.SavedRecipeIds));
    }

    [Fact]
    public async void ReadById_returns_null()
    {
        //Act
        var result = await _repo.ReadByIDAsync(1);
        
        //Assert
        Assert.True(result.IsNone);
    }
}