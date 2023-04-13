using Microsoft.Identity.Web;
using server.Services;

namespace server.Controllers;

[ApiController]
[Route("api/[Controller]")]
public class MealController : ControllerBase
{
    ILogger<MealController> _logger;

    IMealRepository _repo;
    IMealService _service;

    public MealController(ILogger<MealController> logger, IMealRepository repo, IMealService service)
    {
        _logger = logger;
        _repo = repo;
        _service = service;
    }

    [HttpPost]
    public async Task<IActionResult> Post(MealCreateDTO mealCreate)
    {
        try
        {
            var (r, dto) = await _repo.CreateAsync(mealCreate);
            if (r == Core.Response.BadRequest) return BadRequest();
            if (r == Core.Response.Conflict) return Conflict();
            if (r == Core.Response.NotFound) return NotFound();
            if (r == Core.Response.Created) return Ok(dto);
            throw new Exception(r.ToString());
        }
        catch (Exception e)
        {
            _logger.LogError(e, e.Message);
            return StatusCode(500, "Internal Server Error");
        }
    }

    [HttpPut]
    [AllowAnonymous]
    public async Task<IActionResult> Put(MealUpdateDTO meal)
    {
        try
        {
            var r = await _repo.UpdateAsync(meal);
            if (r == Core.Response.Updated) return NoContent();
            throw new Exception(r.ToString());
        }
        catch (Exception e)
        {
            _logger.LogError(e, e.Message);
            return StatusCode(500, "Internal Server Error");
        }
    }

    [HttpGet("{mealID}")]
    public async Task<IActionResult> GetById(int mealID)
    {
        try
        {
            var r = await _repo.ReadWithFoodByIDAsync(mealID);
            if(r.IsNone) return NotFound();
            return Ok(r.Value);
        }   
        catch (Exception e)
        {
            _logger.LogError(e,e.Message);
            return StatusCode(500, "Internal Server Error");
        }
    }

    [HttpGet("{userID}/{date}")]
    public async Task<IActionResult> GetByUserAndDate(int userID, DateTime date)
    {
        try
        {
            var r = await _repo.ReadAllByDateAndUser(date, userID);
            return Ok(r);
        }
        catch (Exception e)
        {
            _logger.LogError(e, e.Message);
            return StatusCode(500, "Internal Server Error");
        }
    }
}