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
            .Include(i => i.Recipes)
            .Select(i => i.ToDTO())
            .FirstOrDefaultAsync();

        if (conflict != null) return (Response.Conflict, conflict);

        if (item.Name == null) return (Response.BadRequest, new FoodItemDTO(
            -1, 
            item.Name ?? "", 
            item.Calories ?? 0f, 
            item.Protein ?? 0f, 
            item.Carbohydrates ?? 0f,
            item.Sugars ?? 0f,
            item.Fibres ?? 0f,
            item.TotalFat ?? 0f,
            item.SaturatedFat ?? 0f,
            item.MonounsaturatedFat ?? 0f,
            item.PolyunsaturatedFat ?? 0f,
            item.TransFat ?? 0f,
            item.VitaminA ?? 0f,
            item.VitaminB6 ?? 0f,
            item.VitaminB12 ?? 0f,
            item.VitaminC ?? 0f,
            item.VitaminD ?? 0f,
            item.VitaminE ?? 0f,
            item.VitaminK1 ?? 0f,
            item.Thiamin ?? 0f,
            item.Riboflavin ?? 0f,
            item.Niacin ?? 0f,
            item.Folate ?? 0f,
            item.Salt ?? 0f,
            item.Potassium ?? 0f,
            item.Magnesium ?? 0f,
            item.Iron ?? 0f,
            item.Zinc ?? 0f,
            item.Phosphorus ?? 0f,
            item.Copper ?? 0f,
            item.Iodine ?? 0f,
            item.Nickel ?? 0f,
            item.Selen ?? 0f,
            item.Calcium ?? 0f));

        var entity = new FoodItem
        (
            item.Name,
            new List<Recipe>(),
            item.Calories ?? 0f,
            item.Protein ?? 0f,
            item.Carbohydrates ?? 0f,
            item.Sugars ?? 0f,
            item.Fibres ?? 0f,
            item.TotalFat ?? 0f,
            item.SaturatedFat ?? 0f,
            item.MonounsaturatedFat ?? 0f,
            item.PolyunsaturatedFat ?? 0f,
            item.TransFat ?? 0f,
            item.VitaminA ?? 0f,
            item.VitaminB6 ?? 0f,
            item.VitaminB12 ?? 0f,
            item.VitaminC ?? 0f,
            item.VitaminD ?? 0f,
            item.VitaminE ?? 0f,
            item.VitaminK1 ?? 0f,
            item.Thiamin ?? 0f,
            item.Riboflavin ?? 0f,
            item.Niacin ?? 0f,
            item.Folate ?? 0f,
            item.Salt ?? 0f,
            item.Potassium ?? 0f,
            item.Magnesium ?? 0f,
            item.Iron ?? 0f,
            item.Zinc ?? 0f,
            item.Phosphorus ?? 0f,
            item.Copper ?? 0f,
            item.Iodine ?? 0f,
            item.Nickel ?? 0f,
            item.Selen ?? 0f,
            item.Calcium ?? 0f
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

        if (!itemEntity.Calories.Equals(item.Calories) && item.Calories != null)
        {
            itemEntity.Calories = (float) item.Calories;
        }

        if (!itemEntity.Protein.Equals(item.Protein) && item.Protein != null)
        {
            itemEntity.Protein = (float) item.Protein;
        }

        if (!itemEntity.Carbohydrates.Equals(item.Carbohydrates) && item.Carbohydrates != null)
        {
            itemEntity.Carbohydrates = (float) item.Carbohydrates;
        }

        if (!itemEntity.Sugars.Equals(item.Sugars) && item.Sugars != null)
        {
            itemEntity.Sugars = (float) item.Sugars;
        }

        if (!itemEntity.Fibres.Equals(item.Fibres) && item.Fibres != null)
        {
            itemEntity.Fibres = (float) item.Fibres;
        }

        if (!itemEntity.TotalFat.Equals(item.TotalFat) && item.TotalFat != null)
        {
            itemEntity.TotalFat = (float) item.TotalFat;
        }

        if (!itemEntity.SaturatedFat.Equals(item.SaturatedFat) && item.SaturatedFat != null)
        {
            itemEntity.SaturatedFat = (float) item.SaturatedFat;
        }

        if (!itemEntity.MonounsaturatedFat.Equals(item.MonounsaturatedFat) && item.MonounsaturatedFat != null)
        {
            itemEntity.MonounsaturatedFat = (float) item.MonounsaturatedFat;
        }

        if (!itemEntity.PolyunsaturatedFat.Equals(item.PolyunsaturatedFat) && item.PolyunsaturatedFat != null)
        {
            itemEntity.PolyunsaturatedFat = (float) item.PolyunsaturatedFat;
        }

        if (!itemEntity.TransFat.Equals(item.TransFat) && item.TransFat != null)
        {
            itemEntity.TransFat = (float) item.TransFat;
        }

        if (!itemEntity.VitaminA.Equals(item.VitaminA) && item.VitaminA != null)
        {
            itemEntity.VitaminA = (float) item.VitaminA;
        }

        if (!itemEntity.VitaminB6.Equals(item.VitaminB6) && item.VitaminB6 != null)
        {
            itemEntity.VitaminB6 = (float) item.VitaminB6;
        }

        if (!itemEntity.VitaminB12.Equals(item.VitaminB12) && item.VitaminB12 != null)
        {
            itemEntity.VitaminB12 = (float) item.VitaminB12;
        }

        if (!itemEntity.VitaminC.Equals(item.VitaminC) && item.VitaminC != null)
        {
            itemEntity.VitaminC = (float) item.VitaminC;
        }

        if (!itemEntity.VitaminD.Equals(item.VitaminD) && item.VitaminD != null)
        {
            itemEntity.VitaminD = (float) item.VitaminD;
        }

        if (!itemEntity.VitaminE.Equals(item.VitaminE) && item.VitaminE != null)
        {
            itemEntity.VitaminE = (float) item.VitaminE;
        }

        if (!itemEntity.VitaminK1.Equals(item.VitaminK1) && item.VitaminK1 != null)
        {
            itemEntity.VitaminK1 = (float) item.VitaminK1;
        }

        if (!itemEntity.Thiamin.Equals(item.Thiamin) && item.Thiamin != null)
        {
            itemEntity.Thiamin = (float) item.Thiamin;
        }

        if (!itemEntity.Riboflavin.Equals(item.Riboflavin) && item.Riboflavin != null)
        {
            itemEntity.Riboflavin = (float) item.Riboflavin;
        }

        if (!itemEntity.Niacin.Equals(item.Niacin) && item.Niacin != null)
        {
            itemEntity.Niacin = (float) item.Niacin;
        }

        if (!itemEntity.Folate.Equals(item.Folate) && item.Folate != null)
        {
            itemEntity.Folate = (float) item.Folate;
        }

        if (!itemEntity.Salt.Equals(item.Salt) && item.Salt != null)
        {
            itemEntity.Salt = (float) item.Salt;
        }

        if (!itemEntity.Potassium.Equals(item.Potassium) && item.Potassium != null)
        {
            itemEntity.Potassium = (float) item.Potassium;
        }

        if (!itemEntity.Magnesium.Equals(item.Magnesium) && item.Magnesium != null)
        {
            itemEntity.Magnesium = (float) item.Magnesium;
        }

        if (!itemEntity.Iron.Equals(item.Iron) && item.Iron != null)
        {
            itemEntity.Iron = (float) item.Iron;
        }

        if (!itemEntity.Zinc.Equals(item.Zinc) && item.Zinc != null)
        {
            itemEntity.Zinc = (float) item.Zinc;
        }

        if (!itemEntity.Phosphorus.Equals(item.Phosphorus) && item.Phosphorus != null)
        {
            itemEntity.Phosphorus = (float) item.Phosphorus;
        }

        if (!itemEntity.Copper.Equals(item.Copper) && item.Copper != null)
        {
            itemEntity.Copper = (float) item.Copper;
        }

        if (!itemEntity.Iodine.Equals(item.Iodine) && item.Iodine != null)
        {
            itemEntity.Iodine = (float) item.Iodine;
        }

        if (!itemEntity.Nickel.Equals(item.Nickel) && item.Nickel != null)
        {
            itemEntity.Nickel = (float) item.Nickel;
        }

        if (!itemEntity.Selen.Equals(item.Selen) && item.Selen != null)
        {
            itemEntity.Selen = (float) item.Selen;
        }

        if (!itemEntity.Calcium.Equals(item.Calcium) && item.Calcium != null)
        {
            itemEntity.Calcium = (float) item.Calcium;
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