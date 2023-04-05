using System.Security.Cryptography;

namespace server.Infrastructure;

public class UserRepository : IUserRepository
{
    private readonly NourishNexusContext _context;

    public UserRepository(NourishNexusContext context)
    {
        _context = context;
    }

    public async Task<(Response, UserDTO)> CreateAsync(UserCreateDTO user)
    {
        //Conflict?
        var conflict = await _context.Users
            .Where(u => u.Email == user.Email)
            .FirstOrDefaultAsync();
            
        if (conflict != null) return (Response.Conflict, new UserDTO(-1, user.Nickname ?? "", user.Email ?? "", new List<int>()));
        if (user.Email == null || user.Nickname == null || user.Password == null) return (Response.BadRequest, new UserDTO(-1, user.Email ?? "", user.Nickname ?? "", new List<int>()));

        CreatePasswordHash
        (
            user.Password,
            out byte[] passwordHash,
            out byte[] passwordSalt
        );

        //Create entity and insert it into the database context
        var entity = new User
        (
            user.Nickname,
            user.Email,
            passwordHash,
            passwordSalt,
            CreateRandomToken(),
            new List<Recipe>()
        );

        await _context.Users.AddAsync(entity);

        await _context.SaveChangesAsync();

        return (Response.Created, entity.ToDTO());

    }

    public async Task<Response> UpdateAsync(UserUpdateDTO user)//Returns NotFound, Conflict or Updated
    {
        var userEntity = await _context.Users
            .Where(u => u.Id == user.Id)
            .Include(u => u.SavedRecipes)
            .FirstOrDefaultAsync();
        
        
        if(userEntity == null) return Response.NotFound;

        if(_context.Users.Any(u => u.Id != user.Id && u.Email == user.Email)) return Response.Conflict;

        if (userEntity.Email != null && !userEntity.Email.Equals(user.Email) && user.Email != null)
        {
            userEntity.Email = user.Email;
        }

        if (userEntity.Nickname != null && !userEntity.Nickname.Equals(user.Nickname) && user.Nickname != null)
        {
            userEntity.Nickname = user.Nickname;
        }

        if (user.SavedRecipeIds != null)
        {
            userEntity.SavedRecipes = await RecipeIDsToRecipes(user.SavedRecipeIds);
        }


        //Nutrition
        if (user.BreakfastCalories != null) userEntity.BreakfastCalories = user.BreakfastCalories;
        if (user.LunchCalories != null) userEntity.LunchCalories = user.LunchCalories;
        if (user.DinnerCalories != null) userEntity.DinnerCalories = user.DinnerCalories;
        if (user.SnackCalories != null) userEntity.SnackCalories = user.SnackCalories;

        if(user.ProteinLB != null) userEntity.ProteinLB = user.ProteinLB;
        if(user.ProteinII != null) userEntity.ProteinII = user.ProteinII;
        if(user.ProteinUB != null) userEntity.ProteinUB = user.ProteinUB;

        if(user.CarbohydratesLB != null) userEntity.CarbohydratesLB = user.CarbohydratesLB;
        if(user.CarbohydratesII != null) userEntity.CarbohydratesII = user.CarbohydratesII;
        if(user.CarbohydratesUB != null) userEntity.CarbohydratesUB = user.CarbohydratesUB;

        if(user.SugarsLB != null) userEntity.SugarsLB = user.SugarsLB;
        if(user.SugarsII != null) userEntity.SugarsII = user.SugarsII;
        if(user.SugarsUB != null) userEntity.SugarsUB = user.SugarsUB;

        if(user.FibresLB != null) userEntity.FibresLB = user.FibresLB;
        if(user.FibresII != null) userEntity.FibresII = user.FibresII;
        if(user.FibresUB != null) userEntity.FibresUB = user.FibresUB;

        if(user.TotalFatLB != null) userEntity.TotalFatLB = user.TotalFatLB;
        if(user.TotalFatII != null) userEntity.TotalFatII = user.TotalFatII;
        if(user.TotalFatUB != null) userEntity.TotalFatUB = user.TotalFatUB;

        if(user.SaturatedFatLB != null) userEntity.SaturatedFatLB = user.SaturatedFatLB;
        if(user.SaturatedFatII != null) userEntity.SaturatedFatII = user.SaturatedFatII;
        if(user.SaturatedFatUB != null) userEntity.SaturatedFatUB = user.SaturatedFatUB;

        if(user.MonounsaturatedFatLB != null) userEntity.MonounsaturatedFatLB = user.MonounsaturatedFatLB;
        if(user.MonounsaturatedFatII != null) userEntity.MonounsaturatedFatII = user.MonounsaturatedFatII;
        if(user.MonounsaturatedFatUB != null) userEntity.MonounsaturatedFatUB = user.MonounsaturatedFatUB;

        if(user.PolyunsaturatedFatLB != null) userEntity.PolyunsaturatedFatLB = user.PolyunsaturatedFatLB;
        if(user.PolyunsaturatedFatII != null) userEntity.PolyunsaturatedFatII = user.PolyunsaturatedFatII;
        if(user.PolyunsaturatedFatUB != null) userEntity.PolyunsaturatedFatUB = user.PolyunsaturatedFatUB;

        if(user.TransFatLB != null) userEntity.TransFatLB = user.TransFatLB;
        if(user.TransFatII != null) userEntity.TransFatII = user.TransFatII;
        if(user.TransFatUB != null) userEntity.TransFatUB = user.TransFatUB;

        if(user.VitaminALB != null) userEntity.VitaminALB = user.VitaminALB;
        if(user.VitaminAII != null) userEntity.VitaminAII = user.VitaminAII;
        if(user.VitaminAUB != null) userEntity.VitaminAUB = user.VitaminAUB;

        if(user.VitaminB6LB != null) userEntity.VitaminB6LB = user.VitaminB6LB;
        if(user.VitaminB6II != null) userEntity.VitaminB6II = user.VitaminB6II;
        if(user.VitaminB6UB != null) userEntity.VitaminB6UB = user.VitaminB6UB;

        if(user.VitaminB12LB != null) userEntity.VitaminB12LB = user.VitaminB12LB;
        if(user.VitaminB12II != null) userEntity.VitaminB12II = user.VitaminB12II;
        if(user.VitaminB12UB != null) userEntity.VitaminB12UB = user.VitaminB12UB;

        if(user.VitaminCLB != null) userEntity.VitaminCLB = user.VitaminCLB;
        if(user.VitaminCII != null) userEntity.VitaminCII = user.VitaminCII;
        if(user.VitaminCUB != null) userEntity.VitaminCUB = user.VitaminCUB;

        if(user.VitaminDLB != null) userEntity.VitaminDLB = user.VitaminDLB;
        if(user.VitaminDII != null) userEntity.VitaminDII = user.VitaminDII;
        if(user.VitaminDUB != null) userEntity.VitaminDUB = user.VitaminDUB;

        if(user.VitaminELB != null) userEntity.VitaminELB = user.VitaminELB;
        if(user.VitaminEII != null) userEntity.VitaminEII = user.VitaminEII;
        if(user.VitaminEUB != null) userEntity.VitaminEUB = user.VitaminEUB;

        if(user.ThiaminLB != null) userEntity.ThiaminLB = user.ThiaminLB;
        if(user.ThiaminII != null) userEntity.ThiaminII = user.ThiaminII;
        if(user.ThiaminUB != null) userEntity.ThiaminUB = user.ThiaminUB;

        if(user.RiboflavinLB != null) userEntity.RiboflavinLB = user.RiboflavinLB;
        if(user.RiboflavinII != null) userEntity.RiboflavinII = user.RiboflavinII;
        if(user.RiboflavinUB != null) userEntity.RiboflavinUB = user.RiboflavinUB;

        if(user.NiacinLB != null) userEntity.NiacinLB = user.NiacinLB;
        if(user.NiacinII != null) userEntity.NiacinII = user.NiacinII;
        if(user.NiacinUB != null) userEntity.NiacinUB = user.NiacinUB;

        if(user.FolateLB != null) userEntity.FolateLB = user.FolateLB;
        if(user.FolateII != null) userEntity.FolateII = user.FolateII;
        if(user.FolateUB != null) userEntity.FolateUB = user.FolateUB;

        if(user.SaltLB != null) userEntity.SaltLB = user.SaltLB;
        if(user.SaltII != null) userEntity.SaltII = user.SaltII;
        if(user.SaltUB != null) userEntity.SaltUB = user.SaltUB;

        if(user.PotassiumLB != null) userEntity.PotassiumLB = user.PotassiumLB;
        if(user.PotassiumII != null) userEntity.PotassiumII = user.PotassiumII;
        if(user.PotassiumUB != null) userEntity.PotassiumUB = user.PotassiumUB;

        if(user.MagnesiumLB != null) userEntity.MagnesiumLB = user.MagnesiumLB;
        if(user.MagnesiumII != null) userEntity.MagnesiumII = user.MagnesiumII;
        if(user.MagnesiumUB != null) userEntity.MagnesiumUB = user.MagnesiumUB;

        if(user.IronLB != null) userEntity.IronLB = user.IronLB;
        if(user.IronII != null) userEntity.IronII = user.IronII;
        if(user.IronUB != null) userEntity.IronUB = user.IronUB;

        if(user.ZincLB != null) userEntity.ZincLB = user.ZincLB;
        if(user.ZincII != null) userEntity.ZincII = user.ZincII;
        if(user.ZincUB != null) userEntity.ZincUB = user.ZincUB;

        if(user.PhosphorusLB != null) userEntity.PhosphorusLB = user.PhosphorusLB;
        if(user.PhosphorusII != null) userEntity.PhosphorusII = user.PhosphorusII;
        if(user.PhosphorusUB != null) userEntity.PhosphorusUB = user.PhosphorusUB;

        if(user.CopperLB != null) userEntity.CopperLB = user.CopperLB;
        if(user.CopperII != null) userEntity.CopperII = user.CopperII;
        if(user.CopperUB != null) userEntity.CopperUB = user.CopperUB;

        if(user.IodineLB != null) userEntity.IodineLB = user.IodineLB;
        if(user.IodineII != null) userEntity.IodineII = user.IodineII;
        if(user.IodineUB != null) userEntity.IodineUB = user.IodineUB;

        if(user.SeleniumLB != null) userEntity.SeleniumLB = user.SeleniumLB;
        if(user.SeleniumII != null) userEntity.SeleniumII = user.SeleniumII;
        if(user.SeleniumUB != null) userEntity.SeleniumUB = user.SeleniumUB;

        if(user.CalciumLB != null) userEntity.CalciumLB = user.CalciumLB;
        if(user.CalciumII != null) userEntity.CalciumII = user.CalciumII;
        if(user.CalciumUB != null) userEntity.CalciumUB = user.CalciumUB;


        await _context.SaveChangesAsync();
        
        return Response.Updated;
    }

    public async Task<Response> RemoveAsync(int id)
    {
        var userEntity = await _context.Users
            .Where(u => u.Id == id)
            .FirstOrDefaultAsync();
        
        if(userEntity == null) 
        {
            return Response.NotFound;
        }

        _context.Users.Remove(userEntity);
        await _context.SaveChangesAsync();
        
        return Response.Deleted;
    }

    public async Task<Option<UserDTO>> ReadByIDAsync(int Id)
        => await _context.Users
            .Where(u => u.Id == Id)
            .Include(u => u.SavedRecipes)
            .Select(u => u.ToDTO())
            .FirstOrDefaultAsync();
    
    public async Task<Option<UserDTO>> ReadByEmailAsync(string Email)
        => await _context.Users
            .Where(u => u.Email == Email)
            .Include(u => u.SavedRecipes)
            .Select(u => u.ToDTO())
            .FirstOrDefaultAsync();
    

     public async Task<Option<UserNutritionDTO>> ReadWithNutritionByIDAsync(int Id)
        => await _context.Users
            .Where(u => u.Id == Id)
            .Include(u => u.SavedRecipes)
            .Select(u => u.ToNutritionDTO())
            .FirstOrDefaultAsync();
    
    public async Task<Option<UserAuthDTO>> ReadAuthByEmailAsync(string email)
        => await _context.Users
            .Where(u => u.Email == email)
            .Select(u => 
                new UserAuthDTO
                (
                    u.Id,
                    u.Nickname,
                    u.Email,
                    u.PasswordHash,
                    u.PasswordSalt
                )
            )
            .FirstOrDefaultAsync();

    //Helper functions
    private async Task<List<Recipe>> RecipeIDsToRecipes(List<int> recipeIDs)
        => await _context.Recipes
            .Where(r => recipeIDs.Any(rID => rID == r.Id))
            .ToListAsync();
    
    private void CreatePasswordHash(string password, out byte[] passwordHash, out byte[] passwordSalt)
    {
        using (var hmac = new HMACSHA512())
        {
            passwordSalt = hmac.Key;
            passwordHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
        }
    }

    private string CreateRandomToken()
    {
        return Convert.ToHexString(RandomNumberGenerator.GetBytes(64));
    }
}