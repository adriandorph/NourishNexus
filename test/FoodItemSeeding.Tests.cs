using Microsoft.IdentityModel.Tokens;
using server.Services;

namespace test;

public class FoodItemSeedingTests
{
    private NourishNexusContext _context;
    private readonly FoodItemSeeding _fis;
    private IFoodItemRepository _repo;
    public FoodItemSeedingTests()
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
        _fis = new FoodItemSeeding(_context, _repo);
    }

    [Fact]
    void Clear()
    {
        //Arrange
        _fis.SeedRecipes();

        //Act
        _fis.Clear();

        //Assert
        Assert.Empty(_context.Categories);
        Assert.True(_context.Users.Where(u => u.Nickname == "NourishNexus").IsNullOrEmpty());
        Assert.Empty(_context.Recipes);
        Assert.Empty(_context.FoodItems);
        Assert.Empty(_context.FoodItemRecipes);
    }

    [Fact]
    void SeedRecipes()
    {
        //Act
        _fis.SeedRecipes();

        //Assert
        Assert.NotEmpty(_context.Categories);
        Assert.True(_context.Users.Any(u => u.Nickname == "NourishNexus"));
        Assert.NotEmpty(_context.Recipes);
        Assert.NotEmpty(_context.FoodItems);
        Assert.NotEmpty(_context.FoodItemRecipes);
    }

    [Fact]
    async void Seed()
    {
        //Act
        await _fis.Seed(@"../../../../data.csv");

        //Assert
        Assert.NotEmpty(_context.FoodItems);
        Assert.True(_context.FoodItems.Count() > 1000);
    }
}