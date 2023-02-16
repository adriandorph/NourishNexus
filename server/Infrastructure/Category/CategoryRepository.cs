namespace server.Infrastructure;

public class CategoryRepository : ICategoryRepository
{

    private readonly NourishNexusContext _context;

    public CategoryRepository(NourishNexusContext context){
        _context = context;
    }
    public Task<(Response, CategoryDTO)> CreateAsync(CategoryCreateDTO category)
    {
        throw new NotImplementedException();
    }

    public Task<Response> UpdateAsync(CategoryUpdateDTO category)
    {
        throw new NotImplementedException();
    }

    public Task<Option<CategoryDTO>> ReadByIDAsync(int categoryID)
    {
        throw new NotImplementedException();
    }

    
}





