using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using server.Core;
using server.Services.RecipeManagement;

namespace server.Controllers;

[ApiController]
[Route("api/[controller]")]
public class RecipeController(IRecipeService recipeService, ILogger<RecipeController> logger) : ControllerBase
{
    private readonly IRecipeService _recipeService = recipeService;
    private readonly ILogger<RecipeController> _logger = logger;

    [HttpPost(Name = "CreateRecipe")]
    [AllowAnonymous]
    public async Task<Recipe> CreateRecipe()
    {
        return await _recipeService.CreateRecipe();
    }
}