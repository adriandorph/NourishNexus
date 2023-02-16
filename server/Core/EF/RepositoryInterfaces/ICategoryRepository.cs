namespace server.Core.EF.RepositoryInterfaces;

    public interface ICategoryRepository{
    
    //Create
    public Task<(Response, CategoryDTO)> CreateAsync(CategoryCreateDTO category);

    //Update
    public Task<Response> UpdateAsync(CategoryUpdateDTO category);
    
    //Read
    public Task<Option<CategoryDTO>> ReadByIDAsync(int categoryID);

    //ReadAll
    


    }