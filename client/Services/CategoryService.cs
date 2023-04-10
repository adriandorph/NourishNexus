namespace client.Services;
using server.Core.EF.DTO;
using System.Net.Http.Json;
using System.Net.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.JSInterop;

public class CategoryService
{

    private readonly HttpClient _http;

    public CategoryService(HttpClient http){
        _http = http;
        if (_http.BaseAddress != new Uri("http://localhost:5288")){
            _http.BaseAddress = new Uri("http://localhost:5288");
        }
    }
    
    public async Task<HttpResponseMessage> CreateCategory(CategoryCreateDTO category)
        => await _http.PostAsJsonAsync("api/Category/", category);
    
    public async Task<HttpResponseMessage> GetCategories()
        => await _http.GetAsync("api/Category/categories");
    
    public async Task<HttpResponseMessage> GetCategory (int categoryID)
    => await _http.GetAsync($"api/Category/{categoryID}");
}