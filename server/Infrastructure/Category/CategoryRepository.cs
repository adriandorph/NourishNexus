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
        => await _context.Categories
            .Where(c => c.Id == categoryID)
            .Include(c => c.Recipes)
            .Select(c => c.ToDTO())
            .FirstOrDefaultAsync();
    
    public async Task<Option<CategoryDTO>> ReadByNameAsync(string name)
        => await _context.Categories
            .Where(c => c.Name == name)
            .Include(c => c.Recipes)
            .Select(c => c.ToDTO())
            .FirstOrDefaultAsync();
    
    }





