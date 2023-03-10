namespace server.Controllers;


[ApiController]
[Route("api/[Controller]")]
public class CategoryController : ControllerBase
{
    private readonly ILogger<CategoryController> _logger;
    private readonly ICategoryRepository _repo;    
    public CategoryController(ILogger<CategoryController> logger, ICategoryRepository repo)
    {
        _logger = logger;
        _repo = repo;
    }

    [HttpPost]
    public async Task<IActionResult> Post(CategoryCreateDTO category)
    {
        try
        {
            (Response r, CategoryDTO dto) = await _repo.CreateAsync(category);
            if (r == Core.Response.Created) return Ok("Success");
            else if (r == Core.Response.Conflict) return Conflict("There is already a category with that title.");
            else return StatusCode(500, "An unknown error occured");
        }
        catch (Exception e)
        {
            return StatusCode(500, e.Message);
        }
    }
}