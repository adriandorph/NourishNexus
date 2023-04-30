namespace server.Controllers;

[ApiController]
[Route("api/[Controller]")]
public class RecipeController : ControllerBase
{
    private readonly ILogger<RecipeController> _logger;
    private readonly IRecipeRepository _recipeRepo;
    private readonly IUserRepository _userRepo;

    public RecipeController(ILogger<RecipeController> logger, IRecipeRepository recipeRepo, IUserRepository userRepo)
    {
        _logger = logger;
        _recipeRepo = recipeRepo;
        _userRepo = userRepo;
    }
    
    //POST

    [HttpPost]
    [AllowAnonymous]
    public async Task<IActionResult> Post(RecipeCreateDTO recipe)
    {
        try
        {
            (Response r, RecipeDTO dto) = await _recipeRepo.CreateAsync(recipe);
            if (r == Core.Response.Conflict) return Conflict("User already has a recipe with that title.");
            else if (r == Core.Response.NotFound) return NotFound("Could not find the user.");
            else if (r == Core.Response.BadRequest) return BadRequest();
            
            var userResult = await _userRepo.ReadByIDAsync(recipe.AuthorId);
            if (userResult.IsNone) throw new Exception("Could not find user");
            var user = userResult.Value;
            var updatedSavedRecipes = new List<int>();
            updatedSavedRecipes.AddRange(user.SavedRecipeIds);
            updatedSavedRecipes.Add(dto.Id);
            var userUpdate = new UserUpdateDTO
            {
                Id = user.Id,
                SavedRecipeIds = updatedSavedRecipes
            };
            r = await _userRepo.UpdateAsync(userUpdate);
            if (r == Core.Response.Updated) return Ok(dto.Id);
            else throw new Exception();
        }
        catch (Exception e)
        {
            _logger.LogError(e,e.Message);
            return StatusCode(500, "An unknown error occured");
        }
    }


    //PUT
    [HttpPut("update/{id}")]
    [AllowAnonymous]
    public async Task<IActionResult> Put(RecipeUpdateDTO recipe)
    {
        try
        {
            Response r = await _recipeRepo.UpdateAsync(recipe);
            if (r == Core.Response.Updated) return NoContent();
            else if (r == Core.Response.Conflict) return Conflict("User already has a recipe with that title.");
            else if (r == Core.Response.NotFound) return NotFound("Could not find the recipe.");
            else if (r == Core.Response.BadRequest) return BadRequest();
            else throw new Exception();
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
            if (r == Core.Response.Deleted) return NoContent();
            else if (r == Core.Response.NotFound) return NotFound();
            else throw new Exception();
        }
        catch (Exception e)
        {
            _logger.LogError(e, e.Message);
            return StatusCode(500, "Internal Server Error");
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
            _logger.LogError(e, e.Message);
            return StatusCode(500, e.Message);        
        }
    }

    [HttpGet("meal/{mealID}")]
    [AllowAnonymous]
    public async Task<IActionResult> GetRecipesByMealID(int mealID)
    {
        try
        {
            var r = await _recipeRepo.ReadAllByMealId(mealID);
            return Ok(r);
        }
        catch (Exception e)
        {
            _logger.LogError(e, e.Message);
            return StatusCode(500, "Internal Server Error");
        }
    }

    [HttpGet("/recipes")]
    [AllowAnonymous]
    public async Task<IActionResult> GetRecipesByIDs([FromBody] List<int> RecipesIds)
    {
        try
        {
            var res = new List<RecipeDTO>();
            foreach (var id in RecipesIds)
            {
                var r = await _recipeRepo.ReadByIDAsync(id);
                if (r.IsSome)
                {
                    res.Add(r.Value);
                }
            }
            return Ok(res);
        }
        catch (Exception e)
        {
            _logger.LogError(e, e.Message);
            return StatusCode(500, "Internal Server Error");
        }
    }


    [HttpGet("communityRecipes")]
    [AllowAnonymous]
    public async Task<IActionResult> GetAllPublicRecipes()
    {
        try
        {
            var r = await _recipeRepo.ReadAllPublicAsync();
            return Ok(r);
        }
        catch (Exception e)
        {
            _logger.LogError(e, e.Message);
            return StatusCode(500, "Internal Server Error");
        }
    }

    [HttpGet("search/saved")]
    public async Task<IActionResult> GetSavedBySearchWord(string word, int userID)
    {
        try
        {
            var r = await _recipeRepo.ReadSavedBySearchWord(word, userID);
            return Ok(r);
        }
        catch (Exception e)
        {
            _logger.LogError(e, e.Message);
            return StatusCode(500, "Internal Server Error");
        }
    }

    [HttpGet("search/community/{word}")]
    public async Task<IActionResult> GetPublicBySearchWord(string word)
    {
        try
        {
            var r = await _recipeRepo.ReadPublicBySearchWord(word);
            return Ok(r);
        }
        catch (Exception e)
        {
            _logger.LogError(e, e.Message);
            return StatusCode(500, "Internal Server Error");
        }
    }
}

   