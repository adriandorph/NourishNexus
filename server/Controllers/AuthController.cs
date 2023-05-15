using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using Microsoft.IdentityModel.Tokens;
using server.Infrastructure;

namespace server.Controllers;

[ApiController]
[Route("api/auth")]
public class AuthController : ControllerBase
{
    private readonly ILogger<AuthController> _logger;
    private readonly IUserRepository _userRepo;
    private readonly IConfiguration _configuration;
    public AuthController(ILogger<AuthController> logger, IUserRepository userRepo, IConfiguration configuration)
    {
        _logger = logger;
        _userRepo = userRepo;
        _configuration = configuration;
    }

    [HttpPost]
    [AllowAnonymous]
    public async Task<IActionResult> Login([FromBody] LoginRequest credentials)
    {
        try
        {
            if (!(await VerifyCredentials(credentials))) return Unauthorized();
            var res = await _userRepo.ReadByEmailAsync(credentials.Email);
            var user = res.Value;
            string token = CreateToken(user);
            return await Task.FromResult(Ok(token));
        }
        catch (Exception e)
        {
            _logger.LogError(e, e.Message);
            return StatusCode(500, "Internal Server Error");
        }
    }

    private async Task<bool> VerifyCredentials(LoginRequest credentials)
    {
        var r = await _userRepo.ReadAuthByEmailAsync(credentials.Email);
        if (r.IsNone) return false;
        var user = r.Value;
        
        return VerifyPassword(credentials.Password, user.PasswordHash, user.PasswordSalt);
    }

    private bool VerifyPassword(string password, byte[] passwordHash, byte[] passwordSalt)
    {
        using(var  hmac = new HMACSHA512(passwordSalt))
        {
            var computedHash = hmac.
                ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
            return computedHash.SequenceEqual(passwordHash);
        }
    }

    private string CreateToken(UserDTO user)
    {
        List<Claim> claims = new List<Claim>
        {
            new Claim(ClaimTypes.Email, user.Email),
            new Claim(ClaimTypes.Name, user.Nickname),
            new Claim(ClaimTypes.NameIdentifier, user.Id.ToString())
        };

        var secret = _configuration.GetSection("Jwt:Secret").Value ?? throw new Exception("Jwt secrets not configured");
        var key = new SymmetricSecurityKey(System.Text.Encoding.UTF8.GetBytes(secret));

        var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha512Signature);
        var token = new JwtSecurityToken
        (
            claims: claims,
            expires:DateTime.Now.AddDays(1),
            signingCredentials: creds
        );
        var jwt = new JwtSecurityTokenHandler().WriteToken(token);
        return jwt;
    }

}