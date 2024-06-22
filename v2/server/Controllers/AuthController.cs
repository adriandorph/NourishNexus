using System.Security.Claims;
using System.Security.Cryptography;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using server.Services.DataSource.UserSource;
using server.Services.DataSource.Authentication;

namespace server.Controllers;

[ApiController]
[Route("api/auth")]
public class AuthController(
    ILogger<AuthController> logger,
    IAuthenticationRepository authRepo,
    IUserRepository userRepo,
    IConfiguration configuration
) : ControllerBase
{
    private readonly ILogger<AuthController> _logger = logger;
    private readonly IAuthenticationRepository _authRepo = authRepo;
    private readonly IUserRepository _userRepo = userRepo;
    private readonly IConfiguration _configuration = configuration;

    [HttpPost]
    [AllowAnonymous]
    public async Task<IActionResult> Login([FromBody] LoginRequest credentials)
    {
        try
        {
            var user = await _userRepo.GetUserByEmail(credentials.Email);
            if (user == null) return Unauthorized();

            if (!await VerifyCredentials(credentials, user)) return Unauthorized();

            string token = CreateToken(user);
            return Ok(token);
        }
        catch (Exception e)
        {
            _logger.LogError(e, e.Message);
            return StatusCode(500, "Internal Server Error");
        }
    }

    private async Task<bool> VerifyCredentials(LoginRequest credentials, User user)
    {
        if (user.Id == null) return false;

        var auth = await _authRepo.GetAuthenticationByUserId(user.Id);

        if (auth == null || auth.PasswordHash == null || auth.PasswordSalt == null) 
            return false;

        return VerifyPassword(credentials.Password, auth.PasswordHash, auth.PasswordSalt);
    }

    private static bool VerifyPassword(string password, byte[] passwordHash, byte[] passwordSalt)
    {
        using var hmac = new HMACSHA512(passwordSalt);
        var computedHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
        return computedHash.SequenceEqual(passwordHash);
    }

    private string CreateToken(User user)
    {
        List<Claim> claims =
        [
            new Claim(ClaimTypes.Email, user.Email!),
            new Claim(ClaimTypes.Name, user.Username!),
            new Claim(ClaimTypes.NameIdentifier, user.Id!)
        ];

        var secret = _configuration["Jwt:Secret"] ?? throw new Exception("Jwt secret not configured");
        var key = new SymmetricSecurityKey(System.Text.Encoding.UTF8.GetBytes(secret));

        var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha512Signature);
        var token = new JwtSecurityToken(
            issuer: _configuration["Jwt:Issuer"],
            audience: _configuration["Jwt:Audience"],
            claims: claims,
            expires: DateTime.Now.AddDays(1),
            signingCredentials: creds
        );
        var jwt = new JwtSecurityTokenHandler().WriteToken(token);
        return jwt;
    }
}

public record LoginRequest(string Email, string Password);