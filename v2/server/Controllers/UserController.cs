using server.Core.Services.UserManagement;
using server.Core.Services.UserManagement.DTOs;

namespace server.Controllers;

[ApiController]
[Route("api/[controller]")]
public class UserController(IUserService userService, ILogger<UserController> logger) : ControllerBase
{
    private readonly IUserService _userService = userService;
    private readonly ILogger<UserController> _logger = logger;

    [HttpPost(Name = "Sign-up")]
    [AllowAnonymous]
    public async Task<IActionResult> SignUp([FromBody] SignUpDTO user)
    {
        try {
            return await _userService.SignUpAsync(user);
        } catch (Exception e) {
            _logger.LogError(e, "Error signing up user");
            return StatusCode(500, "Error signing up user");
        }
    }

    [HttpPut(Name = "Update User")]
    //TODO: [Authorize]
    [AllowAnonymous]
    public async Task<IActionResult> UpdateUserDetails([FromBody] UpdateUserDetailsDTO userDetails)
    {
        try {
            var result = await _userService.UpdateUserDetailsAsync(userDetails);
            return result != null ? Ok(result) : NotFound();
        } catch (Exception e) {
            _logger.LogError(e, "Error updating user details");
            return StatusCode(500, "Error updating user details");
        }
    }

    [HttpPut("update-password", Name = "Update Password")]
    //TODO: [Authorize]
    [AllowAnonymous]
    public async Task<IActionResult> UpdatePassword([FromBody] PasswordUpdateDTO passwordUpdate)
    {
        try {
            return await _userService.UpdatePasswordAsync(passwordUpdate);
        } catch (Exception e) {
            _logger.LogError(e, "Error updating password");
            return StatusCode(500, "Error updating password");
        }
    }
}