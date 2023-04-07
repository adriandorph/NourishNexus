namespace server.Controllers;

[ApiController]
[Route("api/[Controller]")]
public class RecipeController : ControllerBase
{
    private readonly ILogger<RecipeController> _logger;
    private readonly IRecipeRepository _recipeRepo;

    public RecipeController(ILogger<RecipeController> logger, IRecipeRepository recipeRepo)
    {
        _logger = logger;
        _recipeRepo = recipeRepo;
    }
    
    //POST

    [HttpPost]
    [AllowAnonymous]
    public async Task<IActionResult> Post(RecipeCreateDTO recipe)
    {
        try
        {
            (Response r, RecipeDTO dto) = await _recipeRepo.CreateAsync(recipe);
            if (r == Core.Response.Created) return Ok("Success");
            else if (r == Core.Response.Conflict) return Conflict("User already has a recipe with that title.");
            else if (r == Core.Response.NotFound) return NotFound("Could not find the user.");
            else if (r == Core.Response.BadRequest) return BadRequest("Bad Request");
            else return StatusCode(500, "An unknown error occured");
        }
        catch (Exception e)
        {
            return StatusCode(500, e.Message);
        }
    }


    //PUT
    [HttpPut]
    [AllowAnonymous]
    public async Task<IActionResult> Put(RecipeUpdateDTO recipe)
    {
        try
        {
            Response r = await _recipeRepo.UpdateAsync(recipe);
            if (r == Core.Response.Updated) return Ok("Success");
            else if (r == Core.Response.Conflict) return Conflict("User already has a recipe with that title.");
            else if (r == Core.Response.NotFound) return NotFound("Could not find the recipe.");
            else if (r == Core.Response.BadRequest) return BadRequest("Bad Request");
            else return StatusCode(500, "An unknown error occured");
        }
        catch (Exception e)
        {
            _logger.LogError(e, e.Message);
            return StatusCode(500, "Internal Server Error");
        }
    }


    //DELETE
     [HttpDelete("delete/{id}")]
    public async Task<IActionResult> DeleteRecipe(int id)
    {
        try{   
            var r = await _recipeRepo.RemoveAsync(id);
            if (r == Core.Response.Deleted) return Ok("Success");
            else if (r == Core.Response.Conflict) return Conflict("User already has a recipe with that title.");
            else if (r == Core.Response.NotFound) return NotFound("Could not find the user.");
            else if (r == Core.Response.BadRequest) return BadRequest("Bad Request");
            else return StatusCode(500, "An unknown error occured");
        }
        catch (Exception e)
        {
            _logger.LogError(e, e.Message);
            return StatusCode(500, "Internal Server Error. Try again later");
        }
    }


    //GET
    [HttpGet("{id}")]
    [AllowAnonymous]
    public async Task<IActionResult> GetRecipeById(int id)
    {
        try
        {
            var r = await _recipeRepo.ReadByIDAsync(id);

            if (r.IsNone) return NotFound();
            return Ok(r.Value);
        }
        catch (Exception e)
        {
            return StatusCode(500, e.Message);        
        }
    }

    [HttpGet("category/{categoryID}")]
    [AllowAnonymous]
    public async Task<IActionResult> GetRecipesByCategoryID(int categoryID)
    {
        try
        {
            var r = await _recipeRepo.ReadAllByCategoryIDAsync(categoryID);
            return Ok(r ?? new List<RecipeDTO>{});
        }
        catch (Exception)
        {
            return StatusCode(500, "Internal Server Error");
        }
    }

    [HttpGet("author/{authorID}")]
    [AllowAnonymous]
    public async Task<IActionResult> GetRecipesByAuthorID(int authorID)
    {
        try
        {
            var r = await _recipeRepo.ReadAllByAuthorIDAsync(authorID);
            Console.WriteLine("Recipes are" + r);
            return Ok(r ?? new List<RecipeDTO>{});
        }
        catch (Exception)
        {
            return StatusCode(500, "Internal Server Error");
        }
    }

    [HttpGet("communityRecipes")]
    [AllowAnonymous]
    public async Task<IActionResult> GetAllPublicRecipes(int authorID)
    {
        try
        {
            var r = await _recipeRepo.ReadAllPublicAsync();
            Console.WriteLine("Recipes are" + r);
            return Ok(r ?? new List<RecipeDTO>{});
        }
        catch (Exception)
        {
            return StatusCode(500, "Internal Server Error");
        }
    }
}

   