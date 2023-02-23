namespace test;
using server.Core.Services;

public class SeedingTests
{
    [Fact]
    public void names()
    {
        FoodItemSeeding.Names();
    }
}