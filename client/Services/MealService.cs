using System.Net;
namespace client.Services;
using server.Core.EF.DTO;
using System.Net.Http.Json;
using System.Net.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.JSInterop;

public class MealService
{

    private readonly HttpClient _http;

    public MealService(HttpClient http)
    {
        _http = http;
        if (_http.BaseAddress != new Uri("http://localhost:5288"))
        {
            _http.BaseAddress = new Uri("http://localhost:5288");
        }
    }

    public async Task<List<MealDTO>> GetMealsByUserIDAndDate(int userId, DateTime date)
    {
        var result = await _http.GetAsync($"api/Meal/{userId}/{date.ToString("yyyy-MM-ddTHH:mm:ss").Replace(".", "%3A")}");
        return await result.Content.ReadFromJsonAsync<List<MealDTO>>() ?? new List<MealDTO>();
    }

}