using Microsoft.AspNetCore.Mvc;
using server.Services.DataSource;
using server.Services.Recipe;

namespace server.Controllers;

[ApiController]
[Route("[controller]")]
public class RecipeController(IRecipeService recipeService, ILogger<RecipeController> logger) : ControllerBase
{
    private readonly IRecipeService _recipeService = recipeService;
    private readonly ILogger<RecipeController> _logger = logger;

    [HttpPost(Name = "CreateRecipe")]
    public async Task<RecipeModel> CreateRecipe()
    {
        return await _recipeService.CreateRecipe();
    }
}