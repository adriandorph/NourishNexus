namespace client.Services;
using server.Core.EF.DTO;
using System.Net.Http.Json;
using System.Net.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.JSInterop;

public class UserService{

    private readonly HttpClient _http;

    public UserService(HttpClient http){
        _http = http;
        _http.BaseAddress = new Uri("http://localhost:5288");
    }
    
    public async Task<HttpResponseMessage> RegisterUser(UserCreateDTO user)
    {
        if (user == null)
        {
            throw new ArgumentNullException(nameof(user));
        }
            
        var response = await _http.PostAsJsonAsync("api/User/", user);
        
        return response;
    }

    public async Task<HttpResponseMessage> Login(string email)
    {
        var response = await _http.GetAsync($"api/User/login/{email}");
        Console.WriteLine(response);
        return response;
    }
}