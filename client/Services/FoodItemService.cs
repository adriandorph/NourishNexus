namespace client.Services;
using server.Core.EF.DTO;
using System.Net.Http.Json;
using System.Net.Http;
using ServiceInterfaces;

public class FoodItemService : IFoodItemService 
{

    private readonly HttpClient _http;

    public FoodItemService(HttpClient http)
    {
        _http = http;
        string environment = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT") ?? "";
        if (environment != "Production")
            if (_http.BaseAddress != new Uri("http://localhost:5288"))
                _http.BaseAddress = new Uri("http://localhost:5288");
    }
    
    public async Task<List<FoodItemAmountDTO>> GetByRecipe(int recipeID)
    {
        var result = await _http.GetAsync($"api/FoodItem/recipe/{recipeID}");
        return await result.Content.ReadFromJsonAsync<List<FoodItemAmountDTO>>() ?? new List<FoodItemAmountDTO>();
    }

    public async Task<List<FoodItemDTO>> GetBySearchWord(string word)
    {
        if (word.Length == 0 || word.StartsWith(" ")) word = "_";
        var result = await _http.GetAsync($"api/FoodItem/search/{word}");
        return await result.Content.ReadFromJsonAsync<List<FoodItemDTO>>() ?? new List<FoodItemDTO>();
    }

    public async Task<FoodItemDTO?> GetFoodItemById(int fooditemID)
    {
        var result = await _http.GetAsync($"api/FoodItem/{fooditemID}");
        if (result.IsSuccessStatusCode) return await result.Content.ReadAsAsync<FoodItemDTO?>();
        else return null;
    }

    public async Task<HttpResponseMessage> SetIngredients(List<FoodItemAmountDTO> foodItems, int recipeID)
    {
        var result = await _http.PutAsJsonAsync($"api/FoodItem/setingredients/{recipeID}", foodItems);
        return result;
    }

    public async Task<List<FoodItemAmountDTO>> GetByMeal(int mealID)
    {
        var result = await _http.GetAsync($"api/FoodItem/meal/{mealID}");
        return await result.Content.ReadFromJsonAsync<List<FoodItemAmountDTO>>() ?? new List<FoodItemAmountDTO>();
    }


}