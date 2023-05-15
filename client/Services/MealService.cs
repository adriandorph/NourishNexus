namespace client.Services;
using ServiceInterfaces;
using server.Core.EF.DTO;
using System.Net.Http.Json;
using System.Net.Http;
using server.Core.EF;

public class MealService : IMealService
{

    private readonly HttpClient _http;

    public MealService(HttpClient http)
    {
        _http = http;
        string environment = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT") ?? "";
        if (environment != "Production")
            if (_http.BaseAddress != new Uri("http://localhost:5288"))
                _http.BaseAddress = new Uri("http://localhost:5288");
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

    public async Task<List<MealWithFoodDTO>> GetMealsWithFoodByUserIDAndDate(int userId, DateTime date)
    {
        var result = await _http.GetAsync($"api/Meal/mealswithfood/{userId}/{date.ToString("yyyy-MM-ddTHH:mm:ss").Replace(".", "%3A")}");
        return await result.Content.ReadFromJsonAsync<List<MealWithFoodDTO>>() ?? new List<MealWithFoodDTO>();
    }

    public async Task<Week?> GetWeek(int userID, DateTime startDate)
    {
        var result = await _http.GetAsync($"api/Meal/week/{userID}/{startDate.ToString("yyyy-MM-ddTHH:mm:ss").Replace(".", "%3A")}");
        return await result.Content.ReadFromJsonAsync<Week>() ?? null;
    }

    public async Task<Day?> GetDay(int userID, DateTime startDate)
    {
        var result = await _http.GetAsync($"api/Meal/day/{userID}/{startDate.ToString("yyyy-MM-ddTHH:mm:ss").Replace(".", "%3A")}");
        return await result.Content.ReadFromJsonAsync<Day>() ?? null;
    }
}