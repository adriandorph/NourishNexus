namespace test;

using System.Security.Cryptography;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using server.Controllers;
public class AuthControllerTests
{
    private Mock<IUserRepository> _repo;
    private IConfiguration _config;
    private Mock<ILogger<AuthController>> _logger;
    private AuthController _controller;
    public AuthControllerTests()
    {
        _repo = new Mock<IUserRepository>();
        var configBuilder = new ConfigurationBuilder();
        configBuilder.AddInMemoryCollection(new List<KeyValuePair<string, string?>>
            {
                new KeyValuePair<string, string?>("Jwt:Secret", "12345678910abcdefghijkLMn")
            }
        );

        _config = configBuilder.Build();
        _logger = new Mock<ILogger<AuthController>>();
        _controller = new AuthController(_logger.Object, _repo.Object, _config);
    }

    [Fact]
    async void Login_returns_403()
    {
        //Arrange
        var user = new UserDTO(1, "Test", "test@email.email", new List<int>());
        var userAuth = new UserAuthDTO(1, "Test", "test@email.email", new byte[32], new byte[32]);
        _repo.Setup(r => r.ReadByEmailAsync("test@email.email")).ReturnsAsync(user);
        _repo.Setup(r => r.ReadAuthByEmailAsync("test@email.email")).ReturnsAsync(userAuth);
        
        var login = new LoginRequest
        {
            Email = "test@email.email",
            Password = "password"
        };


        //Act
        var r = await _controller.Login(login);

        //Assert
        var act = Assert.IsType<UnauthorizedResult>(r);
    }

    [Fact]
    async void Login_returns_200()
    {
        //Arrange
        var user = new UserDTO(1, "Test", "test@email.email", new List<int>());

        byte[] passwordSalt;
        byte[] passwordHash;

        using (var hmac = new HMACSHA512())
        {
            passwordSalt = hmac.Key;
            passwordHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes("password"));
        }


        var userAuth = new UserAuthDTO(1, "Test", "test@email.email", passwordHash, passwordSalt);
        _repo.Setup(r => r.ReadByEmailAsync("test@email.email")).ReturnsAsync(user);
        _repo.Setup(r => r.ReadAuthByEmailAsync("test@email.email")).ReturnsAsync(userAuth);
        
        var login = new LoginRequest
        {
            Email = "test@email.email",
            Password = "password"
        };


        //Act
        var r = await _controller.Login(login);

        //Assert
        var act = Assert.IsType<OkObjectResult>(r);
        Assert.NotNull(act.Value);
    }
}