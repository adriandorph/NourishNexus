namespace test;

using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Any;
using server.Controllers;
using server.Services.MealPlan;

public class PlanningControllerTests
{
    private readonly Mock<ILogger<PlanningController>> _logger;
    private Mock<IMealPlanGenerator> _generator;
    private Mock<IUserRepository> _userRepo;

    private readonly PlanningController _controller;

    public PlanningControllerTests()
    {
        _logger = new Mock<ILogger<PlanningController>>();
        _generator = new Mock<IMealPlanGenerator>();
        _userRepo = new Mock<IUserRepository>();
        _controller = new PlanningController(_logger.Object, _generator.Object, new IntakeTargetCalculator(), _userRepo.Object);
    }

    [Fact]
    async void Generate7DayMealPlan_returns_200()
    {
        //Arrange
        var res = new DietReport(new NutrientTargets(), new NutrientTargets(), new NutrientTargets(), new NutrientTargets(), MealPlanResponse.Success, new MealPlan());
        _generator.Setup(g => g.Generate7DayMealPlan(1, DateTime.MinValue)).ReturnsAsync(res);

        //Act
        var r = await _controller.Generate7DayMealPlan(1, DateTime.MinValue);

        //Assert
        var actual = Assert.IsType<OkObjectResult>(r);
        Assert.Equal(MealPlanResponse.Success, actual.Value);
    }

    [Fact]
    async void SetIntakeTargets_returns_200()
    {
        //Arrange
        var form = new IntakeTargetForm(1, 22, Gender.Male, 75, 185, 1.4f, WeightGoal.Keep);
        _userRepo.Setup(r => r.UpdateAsync(It.IsAny<UserUpdateDTO>())).ReturnsAsync(Response.Updated);

        //Act
        var r = await _controller.SetIntakeTargets(form);

        //Assert
        var actual = Assert.IsType<OkObjectResult>(r);
        Assert.IsType<TargetsResult>(actual.Value);
    }
}