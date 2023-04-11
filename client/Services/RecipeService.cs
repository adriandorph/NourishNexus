using System.Net;
namespace client.Services;
using server.Core.EF.DTO;
using System.Net.Http.Json;
using System.Net.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.JSInterop;

public class RecipeService
{

    private readonly HttpClient _http;

    public RecipeService(HttpClient http){
        _http = http;
        if (_http.BaseAddress != new Uri("http://localhost:5288")){
            _http.BaseAddress = new Uri("http://localhost:5288");
        }
    }
    
    public async Task<HttpResponseMessage> CreateRecipe(RecipeCreateDTO recipe)
        => await _http.PostAsJsonAsync("api/Recipe/", recipe);
    
    public async Task<HttpResponseMessage> GetRecipesByAuthorID(int authorID)
        => await _http.GetAsync($"api/Recipe/author/{authorID}");

    public async Task<RecipeDTO> GetRecipe(int recipeID)
    {
        var result = await _http.GetAsync($"api/Recipe/{recipeID}");
        return await result.Content.ReadAsAsync<RecipeDTO>();
    }

    public async Task<HttpResponseMessage> GetPublicRecipes()
        => await _http.GetAsync($"api/Recipe/communityRecipes");

    public async Task<HttpResponseMessage> UpdateRecipe(RecipeUpdateDTO recipe)
        => await _http.PutAsJsonAsync($"api/Recipe/update/{recipe.Id}", recipe);

    public async Task<HttpResponseMessage> DeleteRecipe(int id)
        => await _http.DeleteAsync($"api/Recipe/delete/{id}");
    
    public async Task<List<RecipeDTO>> GetSavedBySearchWord(string word, int userID)
    {
        var result = await _http.GetAsync($"api/Recipe/search/saved?word={word}&userID={userID}");
        return await result.Content.ReadFromJsonAsync<List<RecipeDTO>>() ?? new List<RecipeDTO>();
    }

    public async Task<List<RecipeDTO>> GetFromCommunityBySearchWord(string word)
    {
        var result = await _http.GetAsync($"api/Recipe/search/community/{word}");
        return await result.Content.ReadFromJsonAsync<List<RecipeDTO>>() ?? new List<RecipeDTO>();
    }
}