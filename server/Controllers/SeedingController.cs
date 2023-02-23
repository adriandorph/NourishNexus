using server.Core.Services;

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

    [HttpGet("/FoodItems")]
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
}