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

    [HttpPost]
    public async Task<IActionResult> Post(FoodItemCreateDTO foodItem)
    {
        try
        {
            (Response r, FoodItemDTO dto) = await _repo.CreateAsync(foodItem);
            if (r == Core.Response.Created) return Ok("Success");
            else if (r == Core.Response.Conflict) return Conflict("There is already a foodItem with that title.");
            else if (r == Core.Response.BadRequest) return BadRequest("Bad request.");
            else return StatusCode(500, "An unknown error occured");
        }
        catch (Exception e)
        {
            return StatusCode(500, e.Message);
        }
    }


    [HttpPut]
    public async Task<IActionResult> Put(FoodItemUpdateDTO foodItem)
    {
        try
        {
            Response r = await _repo.UpdateAsync(foodItem);
            if (r == Core.Response.Updated) return Ok("Success");
            else if (r == Core.Response.Conflict) return Conflict("There is already a foodItem with that title.");
            else if (r == Core.Response.BadRequest) return BadRequest("Bad request.");
            else return StatusCode(500, "An unknown error occured");
        }
        catch (Exception e)
        {
            return StatusCode(500, e.Message);
        }
    }

    
    [HttpDelete]
    public async Task<IActionResult> DeleteFoodItem(int id)
    {
        try
        {
            var r = await _repo.RemoveAsync(id);
            if (r == Core.Response.Deleted) return Ok("Success");
            else if (r == Core.Response.NotFound) return Ok($"Could not find a fooditem with id {id}");
            else return StatusCode(500, "An unknown error occured");
        }
        catch (Exception e)
        {
            return StatusCode(500, e.Message);
        }
    }

    [HttpGet]
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
            return StatusCode(500, e.Message);        
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
            return StatusCode(500, e.Message);        
        }
    }

}