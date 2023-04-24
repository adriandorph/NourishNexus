namespace client.ServiceInterfaces;
using server.Core.EF.DTO;

public interface ICategoryService
{
    Task<HttpResponseMessage> CreateCategory(CategoryCreateDTO category);
    Task<HttpResponseMessage> GetCategories();
    Task<HttpResponseMessage> GetCategory(int categoryID);
}
