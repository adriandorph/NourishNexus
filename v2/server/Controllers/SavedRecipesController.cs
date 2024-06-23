using server.Core.Services.SavedRecipes;

namespace server.Controllers;

[ApiController]
[Route("api/[controller]")]
public class SavedRecipesController(
    ISavedRecipeService savedRecipeService,
    ILogger<SavedRecipesController> logger) : ControllerBase
{
    private readonly ISavedRecipeService _savedRecipeService = savedRecipeService;
    private readonly ILogger<SavedRecipesController> _logger = logger;


    [HttpGet("{userId}", Name = "Get Saved Recipes")]
    public async Task<IActionResult> GetSavedRecipes(string userId)
    {
        try {
            var savedRecipes = await _savedRecipeService.GetSavedRecipesAsync(userId);
            if (savedRecipes == null) return NotFound();
            return Ok(savedRecipes);
        } catch (Exception e) {
            _logger.LogError(e, "Error getting saved recipes");
            return StatusCode(500, "Error getting saved recipes");
        }
    }

    [HttpPost(Name = "Save Recipe")]
    public async Task<IActionResult> SaveRecipe([FromBody] SaveRecipeDTO saveRecipeDTO)
    {
        try {
            var savedRecipes = await _savedRecipeService.SaveRecipeAsync(saveRecipeDTO.RecipeId, saveRecipeDTO.UserId);
            if (savedRecipes == null) return NotFound();
            return Ok(savedRecipes);
        } catch (Exception e) {
            _logger.LogError(e, "Error saving recipe");
            return StatusCode(500, "Error saving recipe");
        }
    }

    [HttpDelete(Name = "Unsave Recipe")]
    public async Task<IActionResult> UnsaveRecipe([FromBody] SaveRecipeDTO saveRecipeDTO)
    {
        try {
            var savedRecipes = await _savedRecipeService.UnsaveRecipeAsync(saveRecipeDTO.UserId, saveRecipeDTO.RecipeId);
            if (savedRecipes == null) return NotFound();
            return Ok(savedRecipes);
        } catch (Exception e) {
            _logger.LogError(e, "Error unsaving recipe");
            return StatusCode(500, "Error unsaving recipe");
        }
    }

    public record SaveRecipeDTO(string RecipeId, string UserId);
}