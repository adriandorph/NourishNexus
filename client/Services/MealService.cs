using System.Net;
namespace client.Services;
using server.Core.EF.DTO;
using System.Net.Http.Json;
using System.Net.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.JSInterop;
using server.Core.EF;

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

    public async Task<MealDTO?> PostMeal(int userID, DateTime date, MealType type)
    {
        var meal = new MealCreateDTO
        {
            MealType = type,
            UserID = userID,
            Date = date
        };

        var result = await _http.PostAsJsonAsync<MealCreateDTO>($"api/Meal", meal);
        return await result.Content.ReadFromJsonAsync<MealDTO>();
    }

    public async Task<HttpResponseMessage> UpdateMeal(MealUpdateDTO meal)
    {
        return await _http.PutAsJsonAsync<MealUpdateDTO>($"api/Meal", meal);
    }

    public async Task<List<MealDTO>> GetMealsByUserIDAndDate(int userId, DateTime date)
    {
        var result = await _http.GetAsync($"api/Meal/{userId}/{date.ToString("yyyy-MM-ddTHH:mm:ss").Replace(".", "%3A")}");
        return await result.Content.ReadFromJsonAsync<List<MealDTO>>() ?? new List<MealDTO>();
    }

    public async Task<MealWithFoodDTO?> GetMealByID(int mealID)
    {
        var result = await _http.GetAsync($"api/Meal/{mealID}");
        return await result.Content.ReadFromJsonAsync<MealWithFoodDTO>();
    }
}