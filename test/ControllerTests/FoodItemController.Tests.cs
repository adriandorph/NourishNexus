namespace test;

using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using server.Controllers;
public class FoodItemControllerTests
{
    private Mock<IFoodItemRepository> _repo;
    private FoodItemController _controller;
    private Mock<ILogger<FoodItemController>> _logger;
    public FoodItemControllerTests()
    {
        _repo = new Mock<IFoodItemRepository>();
        _logger = new Mock<ILogger<FoodItemController>>();
        _controller = new FoodItemController(_logger.Object, _repo.Object);
    }
    
    [Fact]
    async void SetFoodItemsInRecipe_returns_204()
    {
        //Arrange
        var foodItems = new List<FoodItemAmountDTO>();
        _repo.Setup(r => r.UpdateFoodItemsInRecipe(foodItems, 1)).ReturnsAsync(Response.Updated);

        //Act
        var r = await _controller.SetFoodItemsInRecipe(1, foodItems);

        //Assert
        Assert.IsType<NoContentResult>(r);
    }

    [Fact]
    async void SetFoodItemsInRecipe_returns_500()
    {
        //Arrange
        var foodItems = new List<FoodItemAmountDTO>();
        _repo.Setup(r => r.UpdateFoodItemsInRecipe(foodItems, 1)).ReturnsAsync(Response.Deleted);

        //Act
        var r = await _controller.SetFoodItemsInRecipe(1, foodItems);

        //Assert
        var actual = Assert.IsType<ObjectResult>(r);
        Assert.Equal(500, actual.StatusCode);
        Assert.Equal("Internal Server Error.", actual.Value);
    }

    [Fact]
    async void Get_returns_200()
    {
        //Arrange
        var foodItem = new FoodItemDTO(1, "Test",
            1,
            1,
            1,
            1,
            1,
            1,
            1,
            1,
            1,
            1,
            1,
            1,
            1,
            1,
            1,
            1,
            1,
            1,
            1,
            1,
            1,
            1,
            1,
            1,
            1,
            1,
            1,
            1,
            1,
            1
        );
        _repo.Setup(r => r.ReadByIDAsync(1)).ReturnsAsync(foodItem);

        //Act
        var r = await _controller.Get(1);

        //Assert
        var actual = Assert.IsType<OkObjectResult>(r);
        Assert.Equal(foodItem, actual.Value);
    }

    [Fact]
    async void Get_returns_404()
    {
        //Arrange
        _repo.Setup(r => r.ReadByIDAsync(1)).ReturnsAsync(new Option<FoodItemDTO>(null));

        //Act
        var r = await _controller.Get(1);

        //Assert
        var actual = Assert.IsType<NotFoundResult>(r);
    }

    [Fact]
    async void GetByRecipe_returns_200()
    {
        //Arrange
        var foodItems = new List<FoodItemAmountDTO>();
        _repo.Setup(r => r.ReadAllByRecipeId(1)).ReturnsAsync(foodItems);

        //Act
        var r = await _controller.GetByRecipe(1);

        //Assert
        var actual = Assert.IsType<OkObjectResult>(r);
        Assert.Equal(foodItems, actual.Value);
    }

    [Fact]
    async void GetByMeal_returns_200()
    {
        //Arrange
        var foodItems = new List<FoodItemAmountDTO>();
        _repo.Setup(r => r.ReadAllByMealId(1)).ReturnsAsync(foodItems);

        //Act
        var r = await _controller.GetByMeal(1);

        //Assert
        var actual = Assert.IsType<OkObjectResult>(r);
        Assert.Equal(foodItems, actual.Value);
    }

    [Fact]
    async void GetBySearchWord_returns_200()
    {
        //Arrange
        var foodItems = new List<FoodItemDTO>();
        _repo.Setup(r => r.ReadAllBySearchWord("")).ReturnsAsync(foodItems);

        //Act
        var r = await _controller.GetBySearchWord("");

        //Assert
        var actual = Assert.IsType<OkObjectResult>(r);
        Assert.Equal(foodItems, actual.Value);
    }
    
}