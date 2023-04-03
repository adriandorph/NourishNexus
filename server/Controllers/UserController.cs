namespace server.Controllers;
using server.Core.EF.DTO;
using System.Net.Http.Json;
using System.IdentityModel.Tokens.Jwt;
using System.Text;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using Microsoft.AspNetCore.Authentication;
using Microsoft.JSInterop;
using System.Security.Claims;

[ApiController]
[Route("api/[Controller]")]
public class UserController : ControllerBase
{
    private readonly ILogger<UserController> _logger;
    
    private readonly IUserRepository _userRepo;
    private readonly IConfiguration _configuration;
    

    public UserController(ILogger<UserController> logger, IUserRepository userRepo, IConfiguration configuration)
    {
        _logger = logger;
        _userRepo = userRepo;
        _configuration = configuration;
    }
    
    //POST

    //Register

    [HttpPost]
    [AllowAnonymous]
    public async Task<IActionResult> Post(UserCreateDTO user)
    {
        try
        {
            (Response r, UserDTO dto) = await _userRepo.CreateAsync(user);
            if (r == Core.Response.Created) return Ok("Success");
            else if (r == Core.Response.Conflict) return Conflict("System already has a user with that email.");
            else if (r == Core.Response.BadRequest) return BadRequest("Bad Request");
            else return StatusCode(500, "An unknown error occured");
        }
        catch (Exception e)
        {
            return StatusCode(500, e.Message);
        }
    }

    //PUT
    [HttpPut]
    [AllowAnonymous]
    public async Task<IActionResult> Put(UserUpdateDTO user)
    {
        try
        {
            Response r = await _userRepo.UpdateAsync(user);
            if (r == Core.Response.Updated) return Ok("Success");
            else if (r == Core.Response.Conflict) return Conflict("System already has a user with that email.");
            else if (r == Core.Response.NotFound) return NotFound("Could not find the user.");
            else if (r == Core.Response.BadRequest) return BadRequest("Bad Request");
            else return StatusCode(500, "An unknown error occured");
        }
        catch (Exception)
        {
            return StatusCode(500, "Internal Server Error");
        }
    }


    //DELETE
     [HttpDelete("delete/{id}")]
    public async Task<IActionResult> DeleteUser(int id)
    {
        try{   
            var r = await _userRepo.RemoveAsync(id);
            if (r == Core.Response.Deleted) return Ok("Success");
            else if (r == Core.Response.NotFound) return NotFound("Could not find the user.");
            else if (r == Core.Response.BadRequest) return BadRequest("Bad Request");
            else return StatusCode(500, "An unknown error occured");
        }
        catch (Exception e)
        {
            _logger.LogError(e, e.Message);
            return StatusCode(500, "Internal Server Error. Try again later");
        }
    }


    //GET
    [HttpGet("{id}")]
    [AllowAnonymous]
    public async Task<IActionResult> GetUserById(int id)
    {
        try
        {
            var r = await _userRepo.ReadByIDAsync(id);

            if (r.IsNone) return NotFound();
            return Ok(r.Value);
        }
        catch (Exception)
        {
            return StatusCode(500, "Internal Server Error");        
        }
    }

    [HttpGet("readbyemail/{email}")]
    [AllowAnonymous]
    public async Task<IActionResult> GetUserByEmail(string Email)
    {
        try
        {
            var r = await _userRepo.ReadByEmailAsync(Email);

            if (r.IsNone) return NotFound();
            return Ok(r.Value);
        }
        catch (Exception)
        {
            return StatusCode(500, "Internal Server Error");        
        }
    }

    //Authentication
    [HttpGet("login/{email}")]
    [AllowAnonymous]
    public async Task<IActionResult> Login(string email)
    {
        var r = await _userRepo.ReadByEmailAsync(email);
        if (r.IsNone) return NotFound();
        string token = CreateToken(email);
        return Ok(token);

    }

    private string CreateToken(string Email){
        List<Claim> claims = new List<Claim>{
            new Claim(ClaimTypes.Email, Email)
        };

        var key = new SymmetricSecurityKey(System.Text.Encoding.UTF8.GetBytes(
            _configuration.GetSection("Jwt:Secret").Value));
        var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha512Signature);
        var token = new JwtSecurityToken(
            claims: claims,
            expires:DateTime.Now.AddDays(1),
            signingCredentials: creds
        );

        var jwt = new JwtSecurityTokenHandler().WriteToken(token);

        return jwt;
    }
}