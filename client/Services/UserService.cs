namespace client.Services;
using server.Core.EF.DTO;
using System.Net.Http.Json;

public class UserService{
    private readonly HttpClient _http;

    public UserService(HttpClient http){
        _http = http;
    }

    public async Task<UserDTO> RegisterUser(UserCreateDTO user)
{
    if (user == null)
    {
        throw new ArgumentNullException(nameof(user));
    }
    
    try
    {
        var response = await _http.PostAsJsonAsync("api/user/", user);
        response.EnsureSuccessStatusCode();

        var createdUser = await response.Content.ReadFromJsonAsync<UserDTO>();
        return createdUser;
    }
    catch (HttpRequestException ex)
    {
        throw new Exception($"An error occurred while calling the API. Status code: {ex.StatusCode}. Reason: {ex.Message}");
    }
    catch (Exception ex)
    {
        throw new Exception($"An error occurred while calling the API: {ex.Message}");
    }
}   

}