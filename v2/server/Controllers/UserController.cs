using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using server.Services;

namespace server.Controllers;

[ApiController]
[Route("api/[controller]")]
public class UserController(IUserService userService, ILogger<UserController> logger) : ControllerBase
{
    private readonly IUserService _userService = userService;
    private readonly ILogger<UserController> _logger = logger;

    [HttpPost(Name = "SignUp")]
    [AllowAnonymous]
    public async Task<IActionResult> SignUp([FromBody] SignUpDto user)
    {
        try {
            return await _userService.SignUp(user);
        } catch (Exception e) {
            _logger.LogError(e, "Error signing up user");
            return StatusCode(500, "Error signing up user");
        }
    }
}