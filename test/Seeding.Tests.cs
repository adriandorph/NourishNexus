namespace test;
using server.Core.Services;

public class SeedingTests
{
    [Fact]
    public void FoodItemSeeding_Seed()
    {
        FoodItemSeeding.Seed();
    }
}