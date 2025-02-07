using server.Core.Services.RecipeManagement;
using server.Core.Services.RecipeManagement.DTOs;

namespace server.Controllers;

[ApiController]
[Route("api/[controller]")]
public class RecipeController(IRecipeControllerService recipeControllerService, ILogger<RecipeController> logger) : ControllerBase
{
    private readonly IRecipeControllerService _recipeControllerService = recipeControllerService;
    private readonly ILogger<RecipeController> _logger = logger;

    [HttpPost(Name = "Create Recipe")]
    //TODO: [Authorize]
    [AllowAnonymous]
    public async Task<IActionResult> CreateRecipe([FromBody] RecipeCreateDTO recipeCreateDTO)
    {
        try {
            var result = await _recipeControllerService.CreateRecipeAsync(recipeCreateDTO);
            if (result == null) return BadRequest();
            return Ok(result);//CreatedAtAction(nameof(GetRecipe), new {id = result.Id}, result);
        } catch (Exception e) {
            _logger.LogError(e, "Error creating recipe");
            return StatusCode(500, "Error creating recipe");
        }
    }

    [HttpPut(Name = "UpdateRecipe")]
    //TODO: [Authorize]
    [AllowAnonymous]
    public async Task<IActionResult> UpdateRecipe([FromBody] RecipeUpdateDTO recipeUpdateDTO)
    {
        try {
            var result = await _recipeControllerService.UpdateRecipeAsync(recipeUpdateDTO);
            if (result == null) return BadRequest();
            return Ok(result);
        } catch (Exception e) {
            _logger.LogError(e, "Error updating recipe");
            return StatusCode(500, "Error updating recipe");
        }
    }

    [HttpGet("{recipeId}", Name = "Get Recipe")]
    public async Task<IActionResult> GetRecipe([FromRoute] string recipeId)
    {
        try {
            var result = await _recipeControllerService.GetRecipeAsync(recipeId);
            return result != null ? Ok(result) : NotFound();
        } catch (Exception e) {
            _logger.LogError(e, "Error getting recipe");
            return StatusCode(500, "Error getting recipe");
        }
    }

    [HttpDelete("{recipeId}")]
    public async Task<IActionResult> DeleteRecipe([FromRoute] string recipeId)
    {
        try {
            var result = await _recipeControllerService.DeleteRecipeAsync(recipeId);
            return result ? Ok() : NotFound();
        } catch (Exception e) {
            _logger.LogError(e, "Error deleting recipe");
            return StatusCode(500, "Error deleting recipe");
        }
    }
}