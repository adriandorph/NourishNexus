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
              category.Name.Substring(0, 1).ToUpper() + category.Name.Substring(1).ToLower()
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
    

    public async Task<IList<CategoryDTO>> ReadAllAsync()
    {
       return await ( _context.Categories
                        .Include(c => c.Recipes)
                        .Select(c => c.ToDTO())
                        .ToListAsync());
    }
}