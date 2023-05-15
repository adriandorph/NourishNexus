namespace server.Controllers;


[ApiController]
[Route("api/[Controller]")]
public class FoodItemController : ControllerBase
{
    private readonly ILogger<FoodItemController> _logger;
    private readonly IFoodItemRepository _repo;    
    public FoodItemController(ILogger<FoodItemController> logger, IFoodItemRepository repo)
    {
        _logger = logger;
        _repo = repo;
    }

    [HttpPut("setingredients/{recipeID}")]
    public async Task<IActionResult> SetFoodItemsInRecipe(int recipeId, [FromBody] List<FoodItemAmountDTO> foodItems)
    {
        try
        {
            var r = await _repo.UpdateFoodItemsInRecipe(foodItems, recipeId);
            if (r == Core.Response.Updated) return NoContent();
            else throw new Exception("Not updated");
        }
        catch (Exception e)
        {
            _logger.LogError(e, e.Message);
            return StatusCode(500, "Internal Server Error.");
        }
    }
    

    [HttpGet("{id}")]
    public async Task<IActionResult> Get(int id)
    {
         try
        {
            var r = await _repo.ReadByIDAsync(id);

            if (r.IsNone) return NotFound();
            return Ok(r.Value);
        }
        catch (Exception e)
        {
             _logger.LogError(e, e.Message);
            return StatusCode(500, "Internal Server Error");      
        }
    }

    [HttpGet("recipe/{id}")]
    public async Task<IActionResult> GetByRecipe(int id)
    {
         try
        {
            var r = await _repo.ReadAllByRecipeId(id);
            return Ok(r);
        }
        catch (Exception e)
        {
             _logger.LogError(e, e.Message);
            return StatusCode(500, "Internal Server Error");       
        }
    }

    [HttpGet("meal/{id}")]
    public async Task<IActionResult> GetByMeal(int id)
    {
         try
        {
            var r = await _repo.ReadAllByMealId(id);
            return Ok(r);
        }
        catch (Exception e)
        {
             _logger.LogError(e, e.Message);
            return StatusCode(500, "Internal Server Error");      
        }
    }

    [HttpGet("search/{word}")]
    public async Task<IActionResult> GetBySearchWord(string word)
    {
        try
        {
            var r = await _repo.ReadAllBySearchWord(word);
            return Ok(r);
        }
        catch (Exception e)
        {
            _logger.LogError(e, e.Message);
            return StatusCode(500, "Internal Server Error");
        }
    }

}