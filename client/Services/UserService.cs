namespace client.Services;
using server.Core.EF.DTO;
using System.Net.Http.Json;
using System.Net.Http;

public class UserService
{

    private readonly HttpClient _http;

    public UserService(HttpClient http)
    {
        _http = http;
    }
    
    public async Task<HttpResponseMessage> RegisterUser(UserCreateDTO user)
        => await _http.PostAsJsonAsync("api/User/", user);

    public async Task<HttpResponseMessage> Login(LoginRequest loginRequest)
        => await _http.PostAsJsonAsync($"api/auth/", loginRequest);
    
    public async Task<HttpResponseMessage> UpdateUser(UserUpdateDTO user)
        => await _http.PutAsJsonAsync($"api/User/update/{user.Id}", user);
    
    public async Task<UserDTO> GetUserByID(int id)
    {
        var result = await _http.GetAsync($"api/User/{id}");
        return await result.Content.ReadAsAsync<UserDTO>();
    }

    public async Task<UserNutritionDTO> GetUserNutritionByID(int id)
    {
        var result = await _http.GetAsync($"api/User/nutrition/{id}");
        return await result.Content.ReadAsAsync<UserNutritionDTO>();
    }
}