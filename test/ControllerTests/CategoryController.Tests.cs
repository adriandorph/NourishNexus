namespace test;

using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using server.Controllers;
public class CategoryControllerTests
{
    private Mock<ICategoryRepository> _repo;
    private CategoryController _controller;
    public CategoryControllerTests()
    {
        _repo = new Mock<ICategoryRepository>();
        _controller = new CategoryController((new Mock<ILogger<CategoryController>>()).Object, _repo.Object);
    }

    [Fact]
    async void Post_returns_204()
    {
        //Arrange
        var arg = new CategoryCreateDTO
        {
            Name = "Test"
        };

        var expected = (Response.Created, new CategoryDTO(1, "Test", new List<int>()));

        _repo.Setup(r => r.CreateAsync(arg)).ReturnsAsync(expected);

        //Act
        var r = await _controller.Post(arg);

        //Assert
        var act = Assert.IsType<NoContentResult>(r);
    }

    [Fact]
    async void Post_returns_409()
    {
        //Arrange
        var arg = new CategoryCreateDTO
        {
            Name = "Test"
        };

        var expected = (Response.Conflict, new CategoryDTO(1, "Test", new List<int>()));

        _repo.Setup(r => r.CreateAsync(arg)).ReturnsAsync(expected);

        //Act
        var r = await _controller.Post(arg);

        //Assert
        var act = Assert.IsType<ConflictObjectResult>(r);
        Assert.Equal("There is already a category with that title.", act.Value);
    }

    [Fact]
    async void Post_returns_500()
    {
        //Arrange
        var arg = new CategoryCreateDTO
        {
            Name = "Test"
        };

        var expected = (Response.Deleted, new CategoryDTO(1, "Test", new List<int>()));

        _repo.Setup(r => r.CreateAsync(arg)).ReturnsAsync(expected);

        //Act
        var r = await _controller.Post(arg);

        //Assert
        var act = Assert.IsType<ObjectResult>(r);
        Assert.Equal(500, act.StatusCode);
        Assert.Equal("An unknown error occured", act.Value);
    }

    [Fact]
    async void GetAllCategories_returns_200_with_List_CategoryDTO()
    {
        //Arrange
        var expected = new List<CategoryDTO>
        {
            new CategoryDTO(1, "Test1", new List<int>{1,2,3}),
            new CategoryDTO(2, "Test2", new List<int>{2,3}),
            new CategoryDTO(3, "Test3", new List<int>{3})
        };

        _repo.Setup(r => r.ReadAllAsync()).ReturnsAsync(expected);

        //Act
        var r = await _controller.GetAllCategories();

        //Assert
        var act = Assert.IsType<OkObjectResult>(r);
        Assert.Equal(expected, act.Value);
    }

    [Fact]
    async void GetCategoryById_returns_200_with_CategoryDTO()
    {
        //Arrange
        var expected = new CategoryDTO(1, "Test", new List<int>{1,2,3});
        _repo.Setup(r => r.ReadByIDAsync(1)).ReturnsAsync(expected);

        //Act
        var r = await _controller.GetCategoryById(1);

        //Assert
        var act = Assert.IsType<OkObjectResult>(r);
        Assert.Equal(expected, act.Value);
    }

    [Fact]
    async void GetCategoryById_returns_404()
    {
        //Arrange
        _repo.Setup(r => r.ReadByIDAsync(1)).ReturnsAsync(new Option<CategoryDTO>(null));

        //Act
        var r = await _controller.GetCategoryById(1);

        //Assert
        Assert.IsType<NotFoundResult>(r);
    }
}