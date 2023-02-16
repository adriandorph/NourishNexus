namespace server.Infrastructure;

public class CategoryRepository : ICategoryRepository
{

    private readonly NourishNexusContext _context;

    public CategoryRepository(NourishNexusContext context){
        _context = context;
    }
    public async Task<(Response, CategoryDTO)> CreateAsync(CategoryCreateDTO category)
    {
        var conflict = await _context.Categories
            .Where(c => c.Name == category.Name)
            .FirstOrDefaultAsync();
        if (conflict != null) return (Response.Conflict, new CategoryDTO(-1, category.Name, new List<int>()));

        var entity = new Category
        (
            category.Name  
        );

        await _context.Categories.AddAsync(entity);

        await _context.SaveChangesAsync();

        return (Response.Created, entity.ToDTO());
    }

    public async  Task<Option<CategoryDTO>> ReadByIDAsync(int categoryID)
    {
        var categories = from c in _context.Categories
                        where c.Id == categoryID
                        select c.ToDTO();

        return await categories.FirstOrDefaultAsync();
    }



    
}





