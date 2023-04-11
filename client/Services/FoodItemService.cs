using System.Net;
namespace client.Services;
using server.Core.EF.DTO;
using System.Net.Http.Json;
using System.Net.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.JSInterop;

public class FoodItemService
{

    private readonly HttpClient _http;

    public FoodItemService(HttpClient http){
        _http = http;
        if (_http.BaseAddress != new Uri("http://localhost:5288")){
            _http.BaseAddress = new Uri("http://localhost:5288");
        }
    }
    
    public async Task<List<FoodItemAmountDTO>> GetByRecipe(int recipeID)
    {
        var result = await _http.GetAsync($"api/FoodItem/recipe/{recipeID}");
        return await result.Content.ReadFromJsonAsync<List<FoodItemAmountDTO>>() ?? new List<FoodItemAmountDTO>();
    }
}