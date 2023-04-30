using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using server.Controllers;

namespace test;


public class RecipeControllerTests
{
    Mock<ILogger<RecipeController>> _logger;
    Mock<IRecipeRepository> _repo;
    Mock<IUserRepository> _userRepo;
    RecipeController _controller;
    public RecipeControllerTests()
    {
        _logger = new Mock<ILogger<RecipeController>>();
        _repo = new Mock<IRecipeRepository>();
        _userRepo = new Mock<IUserRepository>();
        _controller = new RecipeController(_logger.Object, _repo.Object, _userRepo.Object);
    }

    [Fact]
    async void Post_returns_200()
    {
        //Arrange
        var createRecipe = new RecipeCreateDTO{AuthorId = 1};
        var res = new RecipeDTO(1, "title", true, "", "", 1, new List<int>(), true, true, true, true);
        _repo.Setup(r => r.CreateAsync(createRecipe)).ReturnsAsync((Response.Created, res));
        _userRepo.Setup(r => r.UpdateAsync(It.IsAny<UserUpdateDTO>())).ReturnsAsync(Response.Updated);
        _userRepo.Setup(r => r.ReadByIDAsync(1)).ReturnsAsync(new UserDTO(1, "", "", new List<int>()));

        //Act
        var r = await _controller.Post(createRecipe);

        //Assert
        var actual = Assert.IsType<OkObjectResult>(r);
        Assert.Equal(1, actual.Value);
    }

    [Fact]
    async void Post_returns_409()
    {
        //Arrange
        var createRecipe = new RecipeCreateDTO{AuthorId = 1};
        var res = new RecipeDTO(1, "title", true, "", "", 1, new List<int>(), true, true, true, true);
        _repo.Setup(r => r.CreateAsync(createRecipe)).ReturnsAsync((Response.Conflict, res));

        //Act
        var r = await _controller.Post(createRecipe);

        //Assert
        var actual = Assert.IsType<ConflictObjectResult>(r);
        Assert.Equal("User already has a recipe with that title.", actual.Value);
    }

    [Fact]
    async void Post_returns_404()
    {
        //Arrange
        var createRecipe = new RecipeCreateDTO{AuthorId = 1};
        var res = new RecipeDTO(1, "title", true, "", "", 1, new List<int>(), true, true, true, true);
        _repo.Setup(r => r.CreateAsync(createRecipe)).ReturnsAsync((Response.NotFound, res));

        //Act
        var r = await _controller.Post(createRecipe);

        //Assert
        var actual = Assert.IsType<NotFoundObjectResult>(r);
        Assert.Equal("Could not find the user.", actual.Value);
    }

    [Fact]
    async void Post_returns_400()
    {
        //Arrange
        var createRecipe = new RecipeCreateDTO{AuthorId = 1};
        var res = new RecipeDTO(1, "title", true, "", "", 1, new List<int>(), true, true, true, true);
        _repo.Setup(r => r.CreateAsync(createRecipe)).ReturnsAsync((Response.BadRequest, res));

        //Act
        var r = await _controller.Post(createRecipe);

        //Assert
        var actual = Assert.IsType<BadRequestResult>(r);
    }

    [Fact]
    async void Post_returns_500_due_to_not_finding_user()
    {
        //Arrange
        var createRecipe = new RecipeCreateDTO{AuthorId = 1};
        var res = new RecipeDTO(1, "title", true, "", "", 1, new List<int>(), true, true, true, true);
        _repo.Setup(r => r.CreateAsync(createRecipe)).ReturnsAsync((Response.Created, res));
        _userRepo.Setup(r => r.ReadByIDAsync(It.IsAny<int>())).ReturnsAsync(new Option<UserDTO>(null));

        //Act
        var r = await _controller.Post(createRecipe);

        //Assert
        var actual = Assert.IsType<ObjectResult>(r);
        Assert.Equal(500, actual.StatusCode);
        Assert.Equal("An unknown error occured", actual.Value);
    }

    [Fact]
    async void Post_returns_500_did_not_update()
    {
        //Arrange
        var createRecipe = new RecipeCreateDTO{AuthorId = 1};
        var res = new RecipeDTO(1, "title", true, "", "", 1, new List<int>(), true, true, true, true);
        _repo.Setup(r => r.CreateAsync(createRecipe)).ReturnsAsync((Response.Created, res));
        _userRepo.Setup(r => r.ReadByIDAsync(It.IsAny<int>())).ReturnsAsync(new UserDTO(1, "", "", new List<int>()));
        _userRepo.Setup(r => r.UpdateAsync(It.IsAny<UserUpdateDTO>())).ReturnsAsync(Response.NotFound);

        //Act
        var r = await _controller.Post(createRecipe);

        //Assert
        var actual = Assert.IsType<ObjectResult>(r);
        Assert.Equal(500, actual.StatusCode);
        Assert.Equal("An unknown error occured", actual.Value);
    }

    [Fact]
    async void Put_returns_204()
    {
        //Arrange
        _repo.Setup(r => r.UpdateAsync(It.IsAny<RecipeUpdateDTO>())).ReturnsAsync(Response.Updated);

        //Act
        var r = await _controller.Put(new RecipeUpdateDTO{});

        //Assert
        Assert.IsType<NoContentResult>(r);
    }

    [Fact]
    async void Put_returns_409()
    {
        //Arrange
        _repo.Setup(r => r.UpdateAsync(It.IsAny<RecipeUpdateDTO>())).ReturnsAsync(Response.Conflict);

        //Act
        var r = await _controller.Put(new RecipeUpdateDTO{});

        //Assert
        var actual = Assert.IsType<ConflictObjectResult>(r);
        Assert.Equal("User already has a recipe with that title.", actual.Value);
    }

    [Fact]
    async void Put_returns_404()
    {
        //Arrange
        _repo.Setup(r => r.UpdateAsync(It.IsAny<RecipeUpdateDTO>())).ReturnsAsync(Response.NotFound);

        //Act
        var r = await _controller.Put(new RecipeUpdateDTO{});

        //Assert
        var actual = Assert.IsType<NotFoundObjectResult>(r);
        Assert.Equal("Could not find the recipe.", actual.Value);
    }

    [Fact]
    async void Put_returns_400()
    {
        //Arrange
        _repo.Setup(r => r.UpdateAsync(It.IsAny<RecipeUpdateDTO>())).ReturnsAsync(Response.BadRequest);

        //Act
        var r = await _controller.Put(new RecipeUpdateDTO{});

        //Assert
        var actual = Assert.IsType<BadRequestResult>(r);
    }

    [Fact]
    async void Put_returns_500()
    {
        //Arrange
        _repo.Setup(r => r.UpdateAsync(It.IsAny<RecipeUpdateDTO>())).ReturnsAsync(Response.Deleted);

        //Act
        var r = await _controller.Put(new RecipeUpdateDTO{});

        //Assert
        var actual = Assert.IsType<ObjectResult>(r);
        Assert.Equal(500, actual.StatusCode);
        Assert.Equal("Internal Server Error", actual.Value);
    }

    [Fact]
    async void Delete_returns_204()
    {
        //Arrange
        _repo.Setup(r => r.RemoveAsync(It.IsAny<int>())).ReturnsAsync(Response.Deleted);

        //Act
        var r = await _controller.DeleteRecipe(1);

        //Assert
        Assert.IsType<NoContentResult>(r);
    }

    [Fact]
    async void Delete_returns_404()
    {
        //Arrange
        _repo.Setup(r => r.RemoveAsync(It.IsAny<int>())).ReturnsAsync(Response.NotFound);

        //Act
        var r = await _controller.DeleteRecipe(1);

        //Assert
        Assert.IsType<NotFoundResult>(r);
    }

    [Fact]
    async void Delete_returns_500()
    {
        //Arrange
        _repo.Setup(r => r.RemoveAsync(It.IsAny<int>())).ReturnsAsync(Response.Updated);

        //Act
        var r = await _controller.DeleteRecipe(1);

        //Assert
        var actual = Assert.IsType<ObjectResult>(r);
        Assert.Equal(500, actual.StatusCode);
        Assert.Equal("Internal Server Error", actual.Value);
    }

    [Fact]
    async void GetRecipeById_returns_200()
    {
        //Arrange
        var recipe = new RecipeDTO(1, "", true, "", "", 1, new List<int>(), true, true, true, true);
        _repo.Setup(r => r.ReadByIDAsync(1)).ReturnsAsync(recipe);

        //Act
        var r = await _controller.GetRecipeById(1);

        //Assert
        var actual = Assert.IsType<OkObjectResult>(r);
        Assert.Equal(recipe, actual.Value);
    }

    [Fact]
    async void GetRecipeById_returns_404()
    {
        //Arrange
        _repo.Setup(r => r.ReadByIDAsync(1)).ReturnsAsync(new Option<RecipeDTO>(null));

        //Act
        var r = await _controller.GetRecipeById(1);

        //Assert
        var actual = Assert.IsType<NotFoundResult>(r);
    }

    [Fact]
    async void GetRecipesByMealID_returns_200()
    {
        //Arrange
        var list = new List<RecipeAmountDTO>();
        _repo.Setup(r => r.ReadAllByMealId(1)).ReturnsAsync(list);

        //Act
        var r = await _controller.GetRecipesByMealID(1);

        //Assert
        var actual = Assert.IsType<OkObjectResult>(r);
        Assert.Equal(list, actual.Value);
    }

    [Fact]
    async void GetRecipesByIDs_returns_200()
    {
        //Arrange
        var list = new List<int>{1,2};
        var recipe1 = new RecipeDTO(1, "", true, "", "", 1, new List<int>(), true, true, true, true);
        var recipe2 = new RecipeDTO(2, "", true, "", "", 1, new List<int>(), true, true, true, true);
        _repo.Setup(r => r.ReadByIDAsync(1)).ReturnsAsync(recipe1);
        _repo.Setup(r => r.ReadByIDAsync(2)).ReturnsAsync(recipe2);


        //Act
        var r = await _controller.GetRecipesByIDs(list);

        //Assert
        var actual = Assert.IsType<OkObjectResult>(r);
        var reslist = Assert.IsType<List<RecipeDTO>>(actual.Value);
        Assert.Collection<RecipeDTO>(
            reslist,
            item => Assert.Equal(recipe1, item),
            item => Assert.Equal(recipe2, item)
        );
    }

    [Fact]
    async void GetAllPublicRecipes_returns_200()
    {
        //Arrange
        var list = new List<RecipeDTO>();
        _repo.Setup(r => r.ReadAllPublicAsync()).ReturnsAsync(list);

        //Act
        var r = await _controller.GetAllPublicRecipes();

        //Assert
        var actual = Assert.IsType<OkObjectResult>(r);
        Assert.Equal(list, actual.Value);
    }

    [Fact]
    async void GetSavedBySearchWord_returns_200()
    {
        //Arrange
        var list = new List<RecipeDTO>();
        _repo.Setup(r => r.ReadSavedBySearchWord("", 1)).ReturnsAsync(list);

        //Act
        var r = await _controller.GetSavedBySearchWord("", 1);

        //Assert
        var actual = Assert.IsType<OkObjectResult>(r);
        Assert.Equal(list, actual.Value);
    }

    [Fact]
    async void GetPublicBySearchWord_returns_200()
    {
        //Arrange
        var list = new List<RecipeDTO>();
        _repo.Setup(r => r.ReadPublicBySearchWord("")).ReturnsAsync(list);

        //Act
        var r = await _controller.GetPublicBySearchWord("");

        //Assert
        var actual = Assert.IsType<OkObjectResult>(r);
        Assert.Equal(list, actual.Value);
    }
}