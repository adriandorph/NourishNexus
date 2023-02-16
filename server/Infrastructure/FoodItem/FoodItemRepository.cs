namespace server.Infrastructure;

public class FoodItemRepository : IFoodItemRepository
{
    private readonly NourishNexusContext _context;

    public FoodItemRepository(NourishNexusContext context)
    {
        _context = context;
    }


    public async Task<(Response, FoodItemDTO)> CreateAsync(FoodItemCreateDTO item){
        var conflict = await _context.FoodItems
            .Where(i => i.Name == item.Name)
            .Select(i => i.ToDTO())
            .FirstOrDefaultAsync();

        if (conflict != null) return (Response.Conflict, conflict);

        if (item.Name == null || item.Unit == null) return (Response.BadRequest, new FoodItemDTO(-1, item.Name ?? "", item.Unit ?? 0, item.Calories ?? 0f, item.Protein ?? 0f));

        var entity = new FoodItem
        (
            item.Name,
            (Unit) item.Unit, 
            item.Calories ?? 0f,
            item.Protein ?? 0f,
            new List<Recipe>()
        );

        _context.FoodItems.Add(entity);

        await _context.SaveChangesAsync();

        return (Response.Created, entity.ToDTO());
    }

    public async Task<Response> UpdateAsync(FoodItemUpdateDTO item)
    {
        var itemEntity = await _context.FoodItems
            .Where(i => i.Id == item.Id)
            .FirstOrDefaultAsync();

        if(itemEntity == null) return Response.NotFound;

        if(_context.FoodItems.Any(fi => fi.Id != item.Id && fi.Name == item.Name)) return Response.Conflict;

        if (!itemEntity.Name.Equals(item.Name) && item.Name != null)
        {
            itemEntity.Name = item.Name;

        }

        if (!itemEntity.Unit.Equals(item.Unit) && item.Unit != null)
        {
            itemEntity.Unit = (Unit) item.Unit;

        }

        if (!itemEntity.Protein.Equals(item.Protein) && item.Protein != null)
        {
            itemEntity.Protein = (float) item.Protein;
        }

        if (!itemEntity.Calories.Equals(item.Calories) && item.Calories != null)
        {
            itemEntity.Calories = (float) item.Calories;
        }

        await _context.SaveChangesAsync();
        
        return Response.Updated;
    }
    

    public async Task<Response> RemoveAsync(int id)
    {
        var itemEntity = await _context.FoodItems
            .Where(i => i.Id == id)
            .FirstOrDefaultAsync();
        
        if(itemEntity == null) 
        {
            return Response.NotFound;
        }

        _context.FoodItems.Remove(itemEntity);
        await _context.SaveChangesAsync();
        
        return Response.Deleted;
    }


    public async Task<IReadOnlyCollection<FoodItemDTO>> ReadAllAsync()
    {
         return(await _context.FoodItems
                        .Select(i => i.ToDTO())
                        .ToListAsync())
                        .AsReadOnly();
    }
    

    public async Task<Option<FoodItemDTO>> ReadByIDAsync(int itemID)
    {
        var items = from i in _context.FoodItems
                        where i.Id == itemID
                        select i.ToDTO();

        return await items.FirstOrDefaultAsync();
    }
    
}