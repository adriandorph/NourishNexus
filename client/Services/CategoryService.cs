namespace client.Services;
using server.Core.EF.DTO;
using System.Net.Http.Json;
using System.Net.Http;
using ServiceInterfaces;

public class CategoryService : ICategoryService
{

    private readonly HttpClient _http;

    public CategoryService(HttpClient http)
    {
        _http = http;
    }
    
    public async Task<HttpResponseMessage> CreateCategory(CategoryCreateDTO category)
        => await _http.PostAsJsonAsync("api/Category/", category);
    
    public async Task<HttpResponseMessage> GetCategories()
        => await _http.GetAsync("api/Category/categories");
    
    public async Task<HttpResponseMessage> GetCategory (int categoryID)
    => await _http.GetAsync($"api/Category/{categoryID}");
}