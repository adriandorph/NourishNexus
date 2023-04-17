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
        if (meal.Date == null)  return (Response.BadRequest, new MealDTO(-1, meal.MealType ?? MealType.BREAKFAST, meal.UserID ?? 0, meal.Date ?? DateTime.MinValue, meal.CategoryIDs ?? new List<int>()));
        if (meal.MealType == null) return (Response.BadRequest, new MealDTO(-1, meal.MealType ?? MealType.BREAKFAST, meal.UserID ?? 0, meal.Date ?? DateTime.MinValue, meal.CategoryIDs ?? new List<int>()));
        if (meal.UserID == null) return (Response.BadRequest, new MealDTO(-1, meal.MealType ?? MealType.BREAKFAST, meal.UserID ?? 0, meal.Date ?? DateTime.MinValue, meal.CategoryIDs ?? new List<int>()));

        var conflict = await _context.Meals
        .Include(m => m.Categories)
        .Include(m => m.User)
        .Where(m => m.Date.Date == ((DateTime)meal.Date).Date && m.MealType == meal.MealType && m.User.Id == meal.UserID)
        .Select(m => m.ToDTO())
        .FirstOrDefaultAsync();

        if(conflict != null) return (Response.Conflict, conflict);

        var user = await _context.Users.Where(u => u.Id == meal.UserID).FirstOrDefaultAsync();
        if (user == null) return (Response.NotFound, new MealDTO(-1, (MealType)meal.MealType, meal.UserID ?? 0, meal.Date ?? DateTime.MinValue, meal.CategoryIDs ?? new List<int>()));

        var entity = new Meal
        (
            (MealType) meal.MealType,
            user,
            (DateTime) meal.Date,
            meal.CategoryIDs != null ? await CategoryIDsToCategories(meal.CategoryIDs) : new List<Category>()
        );

        _context.Meals.Add(entity);

        await _context.SaveChangesAsync();

        return (Response.Created, entity.ToDTO());
    }

    public async Task<Response> UpdateAsync(MealUpdateDTO meal)
    {
        var mealEntity = await _context.Meals
            .Include(m => m.Categories)
            .Include(m => m.User)
            .Where (m => m.Id == meal.Id)
            .FirstOrDefaultAsync();
        
        if(mealEntity == null) return Response.NotFound;

        var allNull = meal.Date == null && meal.UserID == null && meal.MealType == null;

        if(!allNull && _context.Meals.Any(m => m.Date.Date == (meal.Date ?? mealEntity.Date).Date 
            && m.User.Id == (meal.UserID ?? mealEntity.User.Id) 
            && m.MealType == (meal.MealType ?? mealEntity.MealType)))
            return Response.Conflict;
        
        if(mealEntity.MealType != meal.MealType && meal.MealType !=null)
        {
            mealEntity.MealType = (MealType) meal.MealType;
        }

        if(mealEntity.Date != meal.Date && meal.Date !=null)
        {
            mealEntity.Date = (DateTime) meal.Date;
        }

        if(meal.CategoryIDs != null)
        {
            mealEntity.Categories = await CategoryIDsToCategories(meal.CategoryIDs);
        }

        if(meal.FoodItemMeals != null)
        {
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

        if(meal.RecipeMeals != null)
        {
            //Delete all foodItemRecipes that are linked to this recipe
            foreach(var rm in await _context.RecipeMeals.Where(r => r.Meal.Id == meal.Id).ToListAsync())
            {
                _context.RecipeMeals.Remove(rm);
            }
            //Create the foodItemRecipes
            foreach(var rmCreateDTO in meal.RecipeMeals)
            {
                var recipe = await _context.Recipes.Where(rm => rm.Id == rmCreateDTO.RecipeID).FirstOrDefaultAsync();
                if (recipe == null) return Response.NotFound;
                RecipeMeal rmEntity = new RecipeMeal(recipe, mealEntity, rmCreateDTO.Amount);
                await _context.RecipeMeals.AddAsync(rmEntity);
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

    public async Task<IReadOnlyCollection<MealDTO>> ReadAllByDateAndUser(DateTime date, int userID)
        => await _context.Meals
            .Include(m => m.Categories)
            .Include(m => m.User)
            .Where(m => m.User.Id == userID && m.Date.Date == date.Date)
            .Select(m => m.ToDTO())
            .ToListAsync();

    public async Task<IReadOnlyCollection<MealDTO>> ReadAllByDateRangeAndUser(int userID, DateTime startDate, DateTime endDate)
        => await _context.Meals
            .Include(m => m.Categories)
            .Include(m => m.User)
            .Where(m => m.User.Id == userID && m.Date.Date >= startDate.Date && m.Date.Date <= endDate.Date)
            .Select(m => m.ToDTO())
            .ToListAsync();
     
     public async Task<Option<MealDTO>> ReadByUserIdDateAndMealTypeAsync(DateTime date, int userID, MealType mealType)
        => await _context.Meals
                .Include(m => m.Categories)
                .Include(m => m.User)
                .Where(m => m.User.Id == userID && m.Date.Date == date.Date && m.MealType == mealType)
                .Select(m => m.ToDTO())
                .FirstOrDefaultAsync();


    public async Task<Option<MealDTO>> ReadByIDAsync(int id)
        => await _context.Meals
            .Include(m => m.Categories)
            .Include(m => m.User)
            .Where(m => m.Id == id)
            .Select(m => m.ToDTO())
            .FirstOrDefaultAsync();
    
    public async Task<Option<MealWithFoodDTO>> ReadWithFoodByIDAsync(int id)
    {
        var meal = await ReadByIDAsync(id);
        if (meal.IsNone) return null;

        List<FoodItemAmountDTO> foodItems = await _context.FoodItemMeals
            .Where(fim => fim.Meal.Id == id)
            .Select(fim => new FoodItemAmountDTO
                {
                    Amount = fim.Amount,
                    FoodItem = fim.FoodItem.ToDTO()
                }
            )
            .ToListAsync();
        
        List<RecipeAmountDTO> recipes = await _context.RecipeMeals
            .Where(fir => fir.Meal.Id == id)
            .Include(fir => fir.Recipe.Categories)
            .Select(fir => new RecipeAmountDTO(
                    fir.Amount,
                    fir.Recipe.ToDTO()
                )
            )
            .ToListAsync();

        return new MealWithFoodDTO(meal, foodItems, recipes);
    }

    public async Task<IReadOnlyCollection<MealWithFoodDTO>> ReadAllWithFoodByUserAndDateAsync(int userID, DateTime date)
    {
        var meals = await _context.Meals
            .Include(m => m.Categories)
            .Include(m => m.User)
            .Where(m => m.User.Id == userID && m.Date.Date == date.Date)
            .Select(m => m.ToDTO())
            .ToListAsync();

        var mealsWithFood = new List<MealWithFoodDTO>();

        foreach(var meal in meals)
        {
            List<FoodItemAmountDTO> foodItems = await _context.FoodItemMeals
                .Where(fim => fim.Meal.Id == meal.Id)
                .Select(fim => new FoodItemAmountDTO
                    {
                        Amount = fim.Amount,
                        FoodItem = fim.FoodItem.ToDTO()
                    }
                )
                .ToListAsync();
        
            List<RecipeAmountDTO> recipes = await _context.RecipeMeals
                .Where(fir => fir.Meal.Id == meal.Id)
                .Include(fir => fir.Recipe.Categories)
                .Select(fir => new RecipeAmountDTO(
                        fir.Amount,
                        fir.Recipe.ToDTO()
                    )
                )
                .ToListAsync();
            
            var mealWithFood = new MealWithFoodDTO(meal, foodItems, recipes);
            mealsWithFood.Add(mealWithFood);
        }
        return mealsWithFood;
    }



    //Helper functions
     private async Task<List<Category>> CategoryIDsToCategories(List<int> categoryIDs)
        => await _context.Categories
            .Where(c => categoryIDs.Any(cID => cID == c.Id))
            .ToListAsync();
}