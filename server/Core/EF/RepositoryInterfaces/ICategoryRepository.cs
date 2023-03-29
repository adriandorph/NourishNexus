namespace server.Core.EF.RepositoryInterfaces;
using server.Core.EF.DTO;

    public interface ICategoryRepository{
    
    //Create
    public Task<(Response, CategoryDTO)> CreateAsync(CategoryCreateDTO category);
    
    //Read
    public Task<Option<CategoryDTO>> ReadByIDAsync(int categoryID);
    public Task<Option<CategoryDTO>> ReadByNameAsync(string name);

    }