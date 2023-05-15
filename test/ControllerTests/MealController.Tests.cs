namespace test;

using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using server.Controllers;
using server.Core.EF.DTO;
using server.Services;

public class MealControllerTests
{
    private Mock<IMealRepository> _repo;
    private Mock<IMealService> _service;
    private MealController _controller;
    private Mock<ILogger<MealController>> _logger;
    public MealControllerTests()
    {
        _repo = new Mock<IMealRepository>();
        var recipeRepo = new Mock<IRecipeRepository>();
        var foodItemRepo = new Mock<IFoodItemRepository>();
        _logger = new Mock<ILogger<MealController>>();
        _service = new Mock<IMealService>();
        _controller = new MealController(_logger.Object, _repo.Object, _service.Object);
    }

    [Fact]
    async void Post_returns_200()
    {
        //Arrange
        var arg = new MealCreateDTO
        {
            MealType = MealType.BREAKFAST,
            UserID = 1,
            Date = DateTime.MaxValue,
            CategoryIDs = new List<int>()
        };

        var expected = new MealDTO(1, MealType.BREAKFAST, 1, DateTime.MaxValue, new List<int>());
        var res = (Response.Created, expected);

        _repo.Setup(r => r.CreateAsync(arg)).ReturnsAsync(res);

        //Act
        var r = await _controller.Post(arg);

        //Assert
        var act = Assert.IsType<OkObjectResult>(r);
        Assert.Equal(expected, act.Value);
    }

    [Fact]
    async void Post_returns_400()
    {
        //Arrange
        var arg = new MealCreateDTO
        {
            MealType = MealType.BREAKFAST,
            UserID = 1,
            Date = DateTime.MaxValue,
            CategoryIDs = new List<int>()
        };

        var expected = new MealDTO(1, MealType.BREAKFAST, 1, DateTime.MaxValue, new List<int>());
        var res = (Response.BadRequest, expected);

        _repo.Setup(r => r.CreateAsync(arg)).ReturnsAsync(res);

        //Act
        var r = await _controller.Post(arg);

        //Assert
        var act = Assert.IsType<BadRequestResult>(r);
    }

    [Fact]
    async void Post_returns_409()
    {
        //Arrange
        var arg = new MealCreateDTO
        {
            MealType = MealType.BREAKFAST,
            UserID = 1,
            Date = DateTime.MaxValue,
            CategoryIDs = new List<int>()
        };

        var expected = new MealDTO(1, MealType.BREAKFAST, 1, DateTime.MaxValue, new List<int>());
        var res = (Response.Conflict, expected);

        _repo.Setup(r => r.CreateAsync(arg)).ReturnsAsync(res);

        //Act
        var r = await _controller.Post(arg);

        //Assert
        var act = Assert.IsType<ConflictResult>(r);
    }

    [Fact]
    async void Post_returns_404()
    {
        //Arrange
        var arg = new MealCreateDTO
        {
            MealType = MealType.BREAKFAST,
            UserID = 1,
            Date = DateTime.MaxValue,
            CategoryIDs = new List<int>()
        };

        var expected = new MealDTO(1, MealType.BREAKFAST, 1, DateTime.MaxValue, new List<int>());
        var res = (Response.NotFound, expected);

        _repo.Setup(r => r.CreateAsync(arg)).ReturnsAsync(res);

        //Act
        var r = await _controller.Post(arg);

        //Assert
        var act = Assert.IsType<NotFoundResult>(r);
    }

    [Fact]
    async void Post_returns_500()
    {
        //Arrange
        var arg = new MealCreateDTO
        {
            MealType = MealType.BREAKFAST,
            UserID = 1,
            Date = DateTime.MaxValue,
            CategoryIDs = new List<int>()
        };

        var expected = new MealDTO(1, MealType.BREAKFAST, 1, DateTime.MaxValue, new List<int>());
        var res = (Response.Deleted, expected);

        _repo.Setup(r => r.CreateAsync(arg)).ReturnsAsync(res);

        //Act
        var r = await _controller.Post(arg);

        //Assert
        var act = Assert.IsType<ObjectResult>(r);
        Assert.Equal(500, act.StatusCode);
        Assert.Equal("Internal Server Error", act.Value);
    }

    [Fact]
    async void Put_returns_204()
    {
        //Arrange
        var arg = new MealUpdateDTO
        {
            Id = 1,
            MealType = MealType.BREAKFAST,
            UserID = 1,
            Date = DateTime.MaxValue,
            CategoryIDs = new List<int>()
        };

        var res = Response.Updated;

        _repo.Setup(r => r.UpdateAsync(arg)).ReturnsAsync(res);

        //Act
        var r = await _controller.Put(arg);

        //Assert
        var act = Assert.IsType<NoContentResult>(r);
    }


    [Fact]
    async void Put_returns_500()
    {
        //Arrange
        var arg = new MealUpdateDTO
        {
            Id = 1,
            MealType = MealType.BREAKFAST,
            UserID = 1,
            Date = DateTime.MaxValue,
            CategoryIDs = new List<int>()
        };

        var res = Response.Deleted;

        _repo.Setup(r => r.UpdateAsync(arg)).ReturnsAsync(res);

        //Act
        var r = await _controller.Put(arg);

        //Assert
        var act = Assert.IsType<ObjectResult>(r);
        Assert.Equal(500, act.StatusCode);
        Assert.Equal("Internal Server Error", act.Value);
    }


    [Fact]
    async void GetMealById_returns_200_with_MealWithFoodDTO()
    {
        //Arrange
        var expected = new MealWithFoodDTO(
            new MealDTO(1, MealType.BREAKFAST, 1, DateTime.MaxValue, new List<int>()),
            new List<FoodItemAmountDTO>(),
            new List<RecipeAmountDTO>()
        );
        _repo.Setup(r => r.ReadWithFoodByIDAsync(1)).ReturnsAsync(expected);

        //Act
        var r = await _controller.GetById(1);

        //Assert
        var act = Assert.IsType<OkObjectResult>(r);
        Assert.Equal(expected, act.Value);
    }

    [Fact]
    async void GetMealById_returns_404()
    {
        //Arrange
        _repo.Setup(r => r.ReadWithFoodByIDAsync(1)).ReturnsAsync(new Option<MealWithFoodDTO>(null));

        //Act
        var r = await _controller.GetById(1);

        //Assert
        Assert.IsType<NotFoundResult>(r);
    }

    [Fact]
    async void GetWithFoodByUserAndDate_returns_200_with_MealWithFoodDTO()
    {
        //Arrange
        var expected = new List<MealWithFoodDTO>();
        _repo.Setup(r => r.ReadAllWithFoodByUserAndDateAsync(1, DateTime.MaxValue)).ReturnsAsync(expected);

        //Act
        var r = await _controller.GetWithFoodByUserAndDate(1, DateTime.MaxValue);

        //Assert
        var act = Assert.IsType<OkObjectResult>(r);
        Assert.Equal(expected, act.Value);
    }

    [Fact]
    async void GetByUserAndDate()
    {
        //Arrange
        var expected = new List<MealDTO>();
        _repo.Setup(r => r.ReadAllByDateAndUser(DateTime.MaxValue, 1)).ReturnsAsync(expected);

        //Act
        var r = await _controller.GetByUserAndDate(1, DateTime.MaxValue);

        //Assert
        var act = Assert.IsType<OkObjectResult>(r);
        Assert.Equal(expected, act.Value);
    }

    [Fact]
    async void GetWeekByUserAndDate()
    {
        //Arrange
        var meal = new Meal(
            0,
            625,
            new List<FoodItemAmountDTO>(),
            new List<RecipeCalories>()
        );

        var day = new Day(
            2500,
            meal,
            meal,
            meal,
            meal
        );

        var expected = new Week(
            day,
            day,
            day,
            day,
            day,
            day,
            day
        );

        _service.Setup(s => s.GetWeekByUserAndDate(1, DateTime.MinValue)).ReturnsAsync(new OkObjectResult(expected));

        //Act
        var r = await _controller.GetWeekByUserAndDate(1, DateTime.MinValue);

        //Assert
        var act = Assert.IsType<OkObjectResult>(r);
        Assert.Equal(expected, act.Value);
    }

    [Fact]
    async void GetDayByUserAndDate()
    {
        //Arrange
        var meal = new Meal(
            0,
            625,
            new List<FoodItemAmountDTO>(),
            new List<RecipeCalories>()
        );

        var expected = new Day(
            2500,
            meal,
            meal,
            meal,
            meal
        );

        _service.Setup(s => s.GetDayByUserAndDate(1, DateTime.MinValue)).ReturnsAsync(new OkObjectResult(expected));

        //Act
        var r = await _controller.GetDayByUserAndDate(1, DateTime.MinValue);

        //Assert
        var act = Assert.IsType<OkObjectResult>(r);
        Assert.Equal(expected, act.Value);
    }
}