namespace client.Services;
using server.Core.EF.DTO;
using System.Net.Http.Json;
using System.IdentityModel.Tokens.Jwt;
using System.Text;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Components.WebAssembly.Authentication;
using Microsoft.IdentityModel.Tokens;
using Microsoft.JSInterop;
using System.Security.Claims;
using Blazored.LocalStorage;

public class UserService{

    
    private readonly HttpClient _http;
    private readonly IServiceProvider _serviceProvider;

    private readonly IJSRuntime _jsRuntime;
    private readonly ILocalStorageService _localStorage;
    private readonly IConfiguration _configuration;

    public UserService(HttpClient http, IServiceProvider serviceProvider, IJSRuntime jsRuntime, ILocalStorageService localStorage, IConfiguration configuration){
        _http = http;
        _jsRuntime = jsRuntime;
        _serviceProvider = serviceProvider;
        _localStorage = localStorage;
        _configuration = configuration;
        _http.BaseAddress = new Uri("http://localhost:5288/");
    }
    
    

    public async Task<Response> RegisterUser(UserCreateDTO user)
{
    if (user == null)
    {
        throw new ArgumentNullException(nameof(user));
    }
        
    try
    {
        var response = await _http.PostAsJsonAsync("api/User/", user);
        response.EnsureSuccessStatusCode();

        var createdUser = await response.Content.ReadFromJsonAsync<UserDTO>();
        
        return new Response(true, "User registered successfully");
    }
    catch (Exception e)
    {
        return new Response(false, e.Message);
    }
}

public async Task<(bool, string)> Login(string email)
{
    try
    {
        var response = await _http.GetAsync($"api/User/readbyemail/{email}");
        response.EnsureSuccessStatusCode();
         Console.WriteLine(response);
        var user = await response.Content.ReadFromJsonAsync<UserDTO>();
        Console.WriteLine(user);
        Console.WriteLine("Configuration is" + _configuration);
        
        // Generate JWT token
        string jwtSecret = _configuration["Jwt:Secret"];
        if (jwtSecret.IsNullOrEmpty())
        {
            throw new Exception("JWT secret not found in configuration");
        }
        var tokenHandler = new JwtSecurityTokenHandler();
        Console.WriteLine("Token handler is" + tokenHandler);
        var key = Encoding.ASCII.GetBytes(jwtSecret);
        Console.WriteLine("Key is" + key);
        var tokenDescriptor = new SecurityTokenDescriptor
        {
            Subject = new ClaimsIdentity(new Claim[]
            {
                new Claim(ClaimTypes.Email, user.Email)
            }),
            Expires = DateTime.UtcNow.AddDays(7),
            SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
        };
        Console.WriteLine("token descriptor is" + tokenDescriptor);
        var token = tokenHandler.CreateToken(tokenDescriptor);
        Console.WriteLine("token is" + token);
        var tokenString = tokenHandler.WriteToken(token);
        Console.WriteLine("tokenString is" + tokenString);
        
        // Store token in local storage
        await _localStorage.SetItemAsync("authToken", tokenString);
        
        return (true, "User logged in successfully");
    }
    catch (Exception e)
    {
        return (false, e.Message);
    }
}
    public async Task<Response> UpdateUser(UserUpdateDTO user){
        if (user == null)
    {
        throw new ArgumentNullException(nameof(user));
    }

    try
    {
        var response = await _http.PutAsJsonAsync("api/User", user);
        response.EnsureSuccessStatusCode();

        var createdUser = await response.Content.ReadFromJsonAsync<UserDTO>();
        
        return new Response(true, "User registered successfully");
    }
    catch (HttpRequestException ex)
    {
        return new Response(false, ex.Message);
    }
}
    }