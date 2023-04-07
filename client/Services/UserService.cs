namespace client.Services;
using server.Core.EF.DTO;
using System.Net.Http.Json;
using System.Net.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.JSInterop;

public class UserService
{

    private readonly HttpClient _http;

    public UserService(HttpClient http){
        _http = http;
        _http.BaseAddress = new Uri("http://localhost:5288");
    }
    
    public async Task<HttpResponseMessage> RegisterUser(UserCreateDTO user)
        => await _http.PostAsJsonAsync("api/User/", user);

    public async Task<HttpResponseMessage> Login(LoginRequest loginRequest)
        => await _http.PostAsJsonAsync($"api/auth/", loginRequest);
    
    public async Task<HttpResponseMessage> UpdateUser(UserUpdateDTO user)
        => await _http.PutAsJsonAsync($"api/User/update/{user.Id}", user);
}