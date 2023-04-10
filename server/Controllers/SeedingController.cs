using server.Services;

namespace server.Controllers;

[ApiController]
[Route("api/[Controller]")]
public class SeedingController : ControllerBase
{
    private readonly ILogger<SeedingController> _logger;
    private readonly FoodItemSeeding _foodItemSeeding;

    public SeedingController(ILogger<SeedingController> logger, FoodItemSeeding foodItemSeeding)
    {
        _logger = logger;
        _foodItemSeeding = foodItemSeeding;
    }

    [HttpPost("/FoodItems")]
    public async Task<IActionResult> SeedFoodItems()
    {
        try
        {
            Response r = await _foodItemSeeding.Seed();
            if (r == Core.Response.Created) return Ok("Success");
            else return StatusCode(500, "An unknown error occured");
        }
        catch (Exception e)
        {
            _logger.LogError(e, e.Message);
            return StatusCode(500, "Internal Server Error");
        }

    }

    [HttpPost("/Recipes")]
    public IActionResult SeedRecipes()
    {
        try
        {
            _foodItemSeeding.SeedRecipes();
            return NoContent();
        }
        catch (Exception e)
        {
            _logger.LogError(e, e.Message);
            return StatusCode(500, "Internal Server Error");
        }
    }

    [HttpDelete]
    public IActionResult ClearFoodItemsAndRecipes()
    {
        try
        {
            _foodItemSeeding.Clear();
            return NoContent();
        }
        catch (Exception e)
        {
            _logger.LogError(e,e.Message);
            return StatusCode(500, "Internal Server Error");
        }
    }
}