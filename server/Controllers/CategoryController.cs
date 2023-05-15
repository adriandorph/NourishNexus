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
            if (r == Core.Response.Created) return NoContent();
            else if (r == Core.Response.Conflict) return Conflict("There is already a category with that title.");
            else return StatusCode(500, "An unknown error occured");
        }
        catch (Exception e)
        {
            _logger.LogError(e, e.Message);
            return StatusCode(500, "Internal Server Error");
        }
    }

    [HttpGet("categories")]
    [AllowAnonymous]
    public async Task<IActionResult> GetAllCategories()
    {
        try
        {
            var r = await _repo.ReadAllAsync();
            return Ok(r);
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
    public async Task<IActionResult> GetCategoryById(int id)
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
}