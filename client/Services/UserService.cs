namespace client.Services;
using server.Core.EF.DTO;
using System.Net.Http.Json;

public class UserService{
    private readonly HttpClient _http;

    public UserService(HttpClient http){
        _http = http;
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
        var response = await _http.PostAsJsonAsync("api/User", user);
        response.EnsureSuccessStatusCode();

        var createdUser = await response.Content.ReadFromJsonAsync<UserDTO>();
        
        return new Response(true, "User registered successfully");
    }
    catch (HttpRequestException ex)
    {
        return new Response(false, ex.Message);
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