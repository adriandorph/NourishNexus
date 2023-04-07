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
    
    public async Task<HttpResponseMessage> GetRecipes(int authorID)
    => await _http.GetAsync($"api/Recipe/author/{authorID}");

    public async Task<HttpResponseMessage> GetRecipe (int recipeID)
    => await _http.GetAsync($"api/Recipe/{recipeID}");

    public async Task<HttpResponseMessage> GetPublicRecipes()
    => await _http.GetAsync($"api/Recipe/communityRecipes");

    public async Task<HttpResponseMessage> UpdateRecipe(RecipeUpdateDTO recipe)
    => await _http.PutAsJsonAsync($"api/Recipe/update/{recipe.Id}", recipe);

    public async Task<HttpResponseMessage> DeleteRecipe(int id)
        => await _http.DeleteAsync($"api/Recipe/delete/{id}");
}