using server.Core.Services.Authentication;
using server.Core.Services.Authentication.DTOs;

namespace server.Controllers;

[ApiController]
[Route("api/auth")]
public class AuthController(
    ILogger<AuthController> logger,
    IAuthenticationService authService) : ControllerBase
{
    private readonly ILogger<AuthController> _logger = logger;
    private readonly IAuthenticationService _authService = authService;

    [HttpPost]
    [AllowAnonymous]
    public async Task<IActionResult> Login([FromBody] LoginDTO credentials)
    {
        try
        {
            var token = await _authService.AuthenticateAsync(credentials);
            return token == null ? Unauthorized() : Ok(token);
        }
        catch (Exception e)
        {
            _logger.LogError(e, e.Message);
            return StatusCode(500, "Internal Server Error");
        }
    }
}