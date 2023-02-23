namespace server.Infrastructure;

public class MealRepository : IMealRepository
{

    private readonly NourishNexusContext _context;

    public MealRepository(NourishNexusContext context)
    {
        _context = context;
    }

    public async Task<(Response, MealDTO)> CreateAsync(MealCreateDTO meal) //Conflict, NotFound, Created
    {
        var conflict = await _context.Meals
        .Where(m => m.Date == meal.Date)
        .Include(m => m.Categories)
        .Select(m => m.ToDTO())
        .FirstOrDefaultAsync();

        if(conflict != null) return (Response.Conflict, conflict);

        var user = await _context.Users.Where(u => u.Id == meal.UserID).FirstOrDefaultAsync();
        if (meal.MealType == null) return (Response.BadRequest, new MealDTO(-1, meal.MealType ?? MealType.BREAKFAST, meal.UserID, meal.Date, meal.CategoryIDs ?? new List<int>()));
        if (user == null) return (Response.NotFound, new MealDTO(-1, (MealType)meal.MealType, meal.UserID, meal.Date, meal.CategoryIDs ?? new List<int>()));


        var entity = new Meal
        (
            (MealType) meal.MealType,
            user,
            meal.Date,
            meal.CategoryIDs != null ? await CategoryIDsToCategories(meal.CategoryIDs) : new List<Category>()
        );

        _context.Meals.Add(entity);

        await _context.SaveChangesAsync();

        return (Response.Created, entity.ToDTO());
    }

    public async Task<Response> UpdateAsync(MealUpdateDTO meal)
    {
        var mealEntity = await _context.Meals
            .Where (m => m.Id == meal.Id)
            .Include(m => m.Categories)
            .FirstOrDefaultAsync();
        
        if(mealEntity == null) return Response.NotFound;

        if(_context.Meals.Any(m => m.Date.Date == mealEntity.Date.Date && m.User.Id == mealEntity.User.Id && m.MealType == mealEntity.MealType))
            return Response.Conflict;
        
        if(mealEntity.MealType != meal.MealType && meal.MealType !=null){
            mealEntity.MealType = (MealType) meal.MealType;
        }

        if(mealEntity.Date != meal.Date && meal.Date !=null){
            mealEntity.Date = meal.Date;
        }

        if(meal.FoodItemMeals != null){
            //Delete all foodItemRecipes that are linked to this recipe
            foreach(var fim in await _context.FoodItemMeals.Where(f => f.Meal.Id == meal.Id).ToListAsync())
            {
                _context.FoodItemMeals.Remove(fim);
            }
            //Create the foodItemRecipes
            foreach(var fimCreateDTO in meal.FoodItemMeals)
            {
                var foodItem = await _context.FoodItems.Where(fim => fim.Id == fimCreateDTO.FoodItemID).FirstOrDefaultAsync();
                if (foodItem == null) return Response.NotFound;
                FoodItemMeal fimEntity = new FoodItemMeal(foodItem, mealEntity, fimCreateDTO.Amount);
                await _context.FoodItemMeals.AddAsync(fimEntity);
            }
        }

        await _context.SaveChangesAsync();

        return Response.Updated;
    }
    
    public async Task<Response> RemoveAsync(int id)
    {
        var mealEntity = await _context.Meals
            .Where(m => m.Id == id)
            .FirstOrDefaultAsync();
        
        if(mealEntity == null)
        {
            return Response.NotFound;
        }

        foreach(var fim in await _context.FoodItemMeals.Where(f => f.Meal.Id == mealEntity.Id).ToListAsync())
        {
            _context.FoodItemMeals.Remove(fim);
        }

        _context.Meals.Remove(mealEntity);
        await _context.SaveChangesAsync();

        return Response.Deleted;
    }

    public async Task<Option<MealDTO>> ReadAllByDateAndUser(DateTime date, int userID)
    => await _context.Meals
                .Where(m => m.User.Id == userID && m.Date.Date == date.Date)
                .Include(m => m.Categories)
                .Select(m => m.ToDTO())
                .FirstOrDefaultAsync();


    public async Task<Option<MealDTO>> ReadByIDAsync(int id)
        => await _context.Meals
            .Where(m => m.Id == id)
            .Select(m => m.ToDTO())
            .FirstOrDefaultAsync();



    //Helper functions
     private async Task<List<Category>> CategoryIDsToCategories(List<int> categoryIDs)
        => await _context.Categories
            .Where(c => categoryIDs.Any(cID => cID == c.Id))
            .ToListAsync();
}