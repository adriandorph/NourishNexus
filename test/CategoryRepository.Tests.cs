namespace test;

public class CategoryRepositoryTests
{
    NourishNexusContext _context;
    CategoryRepository _repo;
    //Setup
    public CategoryRepositoryTests()
    {
        var connection = new SqliteConnection("Data Source=:memory:");
        connection.Open();

        var builder = new DbContextOptionsBuilder<NourishNexusContext>();
        builder.UseSqlite(connection);
        builder.EnableSensitiveDataLogging();

        var context = new NourishNexusContext(builder.Options);
        context.Database.EnsureCreated();

        _context = context;
        _repo = new CategoryRepository(_context);
    }


    //Create
    
    [Fact]
    public async void CreateAsync_Created()
    {
        //Arrange
        var categoryCreateDTO = new CategoryCreateDTO{
            Name = "Fruit"
        };

        //Act
        (Response response, CategoryDTO categoryDTO) = await _repo.CreateAsync(categoryCreateDTO);

        var entity = await _context.Categories
            .Where(fi => fi.Id == 1)
            .FirstOrDefaultAsync();

        //Assert
        Assert.NotNull(entity);
        Assert.Equal(1, entity.Id);
        Assert.Equal(categoryCreateDTO.Name, entity.Name);
        Assert.True(Enumerable.SequenceEqual(new List<Recipe>{}, entity.Recipes));

        Assert.Equal(Response.Created, response);
        Assert.Equal(entity.Id, categoryDTO.Id);
        Assert.Equal(categoryCreateDTO.Name, categoryDTO.Name);
        Assert.True(Enumerable.SequenceEqual(new List<int>{}, categoryDTO.RecipeIDs));
    }

    [Fact]
    public async void CreateAsync_Category_with_same_Name_Conflict()
    {
        //Arrange
        var categoryCreateDTO = new CategoryCreateDTO{
            Name = "Vegan"
        };

        //Act
        await _repo.CreateAsync(categoryCreateDTO);
        (Response response, CategoryDTO categoryDTO) = await _repo.CreateAsync(categoryCreateDTO);

        //Assert
        Assert.Equal(Response.Conflict, response);
    }

    //Read
    [Fact]
    public async void ReadByID_CategoryDTO()
    {
        //Arrange
        var categoryCreateDTO1 = new CategoryCreateDTO{
            Name = "Vegan"
        };

        var categoryCreateDTO2 = new CategoryCreateDTO{
            Name = "Vegetarian"
        };

        //Act
        await _repo.CreateAsync(categoryCreateDTO1);
        await _repo.CreateAsync(categoryCreateDTO2);
        var result = await _repo.ReadByIDAsync(1);
        

        //Assert
        Assert.True(result.IsSome);
        Assert.Equal(1, result.Value.Id);
        Assert.Equal(categoryCreateDTO1.Name, result.Value.Name);
    }

    [Fact]
    public async void ReadAll()
    {
        //Arrange
        var categoryCreateDTO1 = new CategoryCreateDTO{
            Name = "Vegan"
        };

        var categoryCreateDTO2 = new CategoryCreateDTO{
            Name = "Vegetarian"
        };

        //Act
        await _repo.CreateAsync(categoryCreateDTO1);
        await _repo.CreateAsync(categoryCreateDTO2);
        var result = await _repo.ReadAllAsync();
        

        //Assert
        Assert.Collection<CategoryDTO>(
            result,
            item => {
                Assert.Equal(1, item.Id);
                Assert.Equal(categoryCreateDTO1.Name, item.Name);
            },
            item => {
                Assert.Equal(2, item.Id);
                Assert.Equal(categoryCreateDTO2.Name, item.Name);
            }
        );
    }

    [Fact]
    public async void ReadById_returns_null()
    {
        //Act
        var result = await _repo.ReadByIDAsync(1000);
        
        //Assert
        Assert.True(result.IsNone);
    }
}