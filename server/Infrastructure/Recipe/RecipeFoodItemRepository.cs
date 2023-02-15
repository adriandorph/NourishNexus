namespace server.Infrastructure;
public class RecipeFoodItemRepository : IRecipeFoodItemRepository
{
    private readonly NourishNexusContext _context;

        public RecipeFoodItemRepository(NourishNexusContext context)
        {
            _context = context;
        }

        public async Task<(Response, RecipeFoodItemDTO)> CreateAsync(RecipeFoodItemCreateDTO recipeItem)
        {
            var conflict = await _context.RecipeFoodItems
            .Where(rf => rf.recipe.Id == recipeItem.RecipeId && rf.foodItem.Id == recipeItem.FoodItemId)
            .Select(rf => rf.toDTO())
            .FirstOrDefaultAsync();

            if( conflict != null) return (Response.Conflict, conflict);

            var recipe = await _context.Recipes.Where(r => r.Id == recipeItem.RecipeId).FirstOrDefaultAsync();
            if (recipe == null) return (Response.NotFound, new RecipeFoodItemDTO(-1, recipeItem.RecipeId,recipeItem.FoodItemId));

            var foodItem = await _context.FoodItems.Where(i => i.Id == recipeItem.FoodItemId).FirstOrDefaultAsync();
            if (foodItem == null) return (Response.NotFound, new RecipeFoodItemDTO(-1, recipeItem.RecipeId, recipeItem.FoodItemId));
            
            var entity = new RecipeFoodItem
            (
                recipe,
                foodItem
            );

            _context.RecipeFoodItems.Add(entity);

            await _context.SaveChangesAsync();

            return (Response.Created, entity.toDTO());
        }

    public async Task<Response> RemoveAsync(int id)
    {
        var entity = await _context.RecipeFoodItems
            .Where(rf => rf.Id == id)
            .FirstOrDefaultAsync();

        if(entity == null){
            return Response.NotFound;
        }

        _context.RecipeFoodItems.Remove(entity);

        await _context.SaveChangesAsync();

        return Response.Deleted;
    }

    public async Task<Option<RecipeFoodItemDTO>> ReadByIDAsync(int Id) 
    {
        var recipeItems = from rf in _context.RecipeFoodItems
                            where rf.Id == Id
                            select rf.toDTO();
        return await recipeItems.FirstOrDefaultAsync();
    }

    public async Task<IReadOnlyCollection<RecipeFoodItemDTO>> ReadAllByRecipeIDAsync(int recipeID)
    {
        return await _context.RecipeFoodItems
                        .Where(rf => rf.recipe.Id == recipeID)
                        .Select(rf => rf.toDTO())
                        .ToListAsync();
    }
}