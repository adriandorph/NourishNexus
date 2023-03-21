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


    [HttpGet("/{userID}/{date}")]
    public async Task<IActionResult> GetByUserAndDate(int userID, DateTime date)
    {
        try
        {
            var r = await _repo.ReadAllByDateAndUser(date, userID);


            var result = new 
            {
                Meals = r,
            };
            return Ok(r);
        }
        catch (Exception e)
        {
            _logger.LogError(e, e.Message);
            return StatusCode(500, "Internal Server Error");
        }
    }



    [HttpPut]
    [AllowAnonymous]
    public async Task<IActionResult> Put(MealReportDTO meal)
    {
        try
        {
            return await _service.ReportMeal(meal);
        }
        catch (Exception e)
        {
            _logger.LogError(e, e.Message);
            return StatusCode(500, "Internal Server Error");
        }
    }

}