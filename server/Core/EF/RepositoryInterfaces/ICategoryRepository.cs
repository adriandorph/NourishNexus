namespace server.Core.EF.RepositoryInterfaces;

    public interface ICategoryRepository{
    
    //Create
    public Task<(Response, CategoryDTO)> CreateAsync(CategoryCreateDTO category);
    
    //Read
    public Task<Option<CategoryDTO>> ReadByIDAsync(int categoryID);

    }