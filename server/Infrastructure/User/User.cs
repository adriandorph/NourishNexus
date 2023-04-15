namespace server.Infrastructure;

public class User
{
    public int Id {get;set;}

    [StringLength(25)]
    public string Nickname {get;set;}

    [Required, EmailAddress, StringLength(50)]
    public string Email {get;set;}
    public byte[] PasswordHash {get; set;} = new byte[32];
    public byte[] PasswordSalt {get; set;} = new byte[32];

    public List<Recipe> SavedRecipes {get; set;}

    public float? BreakfastCalories {get; set;}
    public float? LunchCalories {get; set;}
    public float? DinnerCalories {get; set;}
    public float? SnackCalories {get; set;}

    public float? ProteinLB {get; set;}
    public float? ProteinII {get; set;}
    public float? ProteinUB {get; set;}

    public float? CarbohydratesLB {get; set;}
    public float? CarbohydratesII {get; set;}
    public float? CarbohydratesUB {get; set;}

    public float? SugarsLB {get; set;}
    public float? SugarsII {get; set;}
    public float? SugarsUB {get; set;}

    public float? FibresLB {get; set;}
    public float? FibresII {get; set;}
    public float? FibresUB {get; set;}

    public float? TotalFatLB {get; set;}
    public float? TotalFatII {get; set;}
    public float? TotalFatUB {get; set;}

    public float? SaturatedFatLB {get; set;}
    public float? SaturatedFatII {get; set;}
    public float? SaturatedFatUB {get; set;}

    public float? MonounsaturatedFatLB {get; set;}
    public float? MonounsaturatedFatII {get; set;}
    public float? MonounsaturatedFatUB {get; set;}

    public float? PolyunsaturatedFatLB {get; set;}
    public float? PolyunsaturatedFatII {get; set;}
    public float? PolyunsaturatedFatUB {get; set;}

    public float? TransFatLB {get; set;}
    public float? TransFatII {get; set;}
    public float? TransFatUB {get; set;}

    public float? VitaminALB {get; set;}
    public float? VitaminAII {get; set;}
    public float? VitaminAUB {get; set;}

    public float? VitaminB6LB {get; set;}
    public float? VitaminB6II {get; set;}
    public float? VitaminB6UB {get; set;}

    public float? VitaminB12LB {get; set;}
    public float? VitaminB12II {get; set;}
    public float? VitaminB12UB {get; set;}

    public float? VitaminCLB {get; set;}
    public float? VitaminCII {get; set;}
    public float? VitaminCUB {get; set;}

    public float? VitaminDLB {get; set;}
    public float? VitaminDII {get; set;}
    public float? VitaminDUB {get; set;}

    public float? VitaminELB {get; set;}
    public float? VitaminEII {get; set;}
    public float? VitaminEUB {get; set;}

    public float? ThiaminLB {get; set;}
    public float? ThiaminII {get; set;}
    public float? ThiaminUB {get; set;}

    public float? RiboflavinLB {get; set;}
    public float? RiboflavinII {get; set;}
    public float? RiboflavinUB {get; set;}

    public float? NiacinLB {get; set;}
    public float? NiacinII {get; set;}
    public float? NiacinUB {get; set;}

    public float? FolateLB {get; set;}
    public float? FolateII {get; set;}
    public float? FolateUB {get; set;}

    public float? SaltLB {get; set;}
    public float? SaltII {get; set;}
    public float? SaltUB {get; set;}

    public float? PotassiumLB {get; set;}
    public float? PotassiumII {get; set;}
    public float? PotassiumUB {get; set;}

    public float? MagnesiumLB {get; set;}
    public float? MagnesiumII {get; set;}
    public float? MagnesiumUB {get; set;}

    public float? IronLB {get; set;}
    public float? IronII {get; set;}
    public float? IronUB {get; set;}

    public float? ZincLB {get; set;}
    public float? ZincII {get; set;}
    public float? ZincUB {get; set;}

    public float? PhosphorusLB {get; set;}
    public float? PhosphorusII {get; set;}
    public float? PhosphorusUB {get; set;}

    public float? CopperLB {get; set;}
    public float? CopperII {get; set;}
    public float? CopperUB {get; set;}

    public float? IodineLB {get; set;}
    public float? IodineII {get; set;}
    public float? IodineUB {get; set;}

    public float? SeleniumLB {get; set;}
    public float? SeleniumII {get; set;}
    public float? SeleniumUB {get; set;}

    public float? CalciumLB {get; set;}
    public float? CalciumII {get; set;}
    public float? CalciumUB {get; set;}


    public User(string Nickname, string Email, byte[] passwordHash, byte[] passwordSalt,  List<Recipe> savedRecipes) 
    {
        this.Nickname = Nickname;
        this.Email = Email;
        this.PasswordHash = passwordHash;
        this.PasswordSalt = passwordSalt;
        this.SavedRecipes = savedRecipes;
    }

    #nullable disable
    public User() {}

    public UserDTO ToDTO()
            => new UserDTO(
                this.Id,   
                this.Nickname,
                this.Email,
                this.SavedRecipes.Select(r => r.Id).ToList()
            );

    public UserNutritionDTO ToNutritionDTO()
            => new UserNutritionDTO
            {
                Id = this.Id,
                Nickname = this.Nickname,
                Email = this.Email,
                SavedRecipeIds = this.SavedRecipes.Select(r => r.Id).ToList(),
                BreakfastCalories = this.BreakfastCalories,
                LunchCalories = this.LunchCalories,
                DinnerCalories = this.DinnerCalories,
                SnackCalories = this.SnackCalories,
                
                ProteinLB = this.ProteinLB,
                ProteinII = this.ProteinII,
                ProteinUB = this.ProteinUB,

                CarbohydratesLB = this.CarbohydratesLB,
                CarbohydratesII = this.CarbohydratesII,
                CarbohydratesUB = this.CarbohydratesUB,

                SugarsLB = this.SugarsLB,
                SugarsII = this.SugarsII,
                SugarsUB = this.SugarsUB,

                FibresLB = this.FibresLB,
                FibresII = this.FibresII,
                FibresUB = this.FibresUB,

                TotalFatLB = this.TotalFatLB,
                TotalFatII = this.TotalFatII,
                TotalFatUB = this.TotalFatUB,

                SaturatedFatLB = this.SaturatedFatLB,
                SaturatedFatII = this.SaturatedFatII,
                SaturatedFatUB = this.SaturatedFatUB,

                MonounsaturatedFatLB = this.MonounsaturatedFatLB,
                MonounsaturatedFatII = this.MonounsaturatedFatII,
                MonounsaturatedFatUB = this.MonounsaturatedFatUB,

                PolyunsaturatedFatLB = this.PolyunsaturatedFatLB,
                PolyunsaturatedFatII = this.PolyunsaturatedFatII,
                PolyunsaturatedFatUB = this.PolyunsaturatedFatUB,

                TransFatLB = this.TransFatLB,
                TransFatII = this.TransFatII,
                TransFatUB = this.TransFatUB,

                VitaminALB = this.VitaminALB,
                VitaminAII = this.VitaminAII,
                VitaminAUB = this.VitaminAUB,

                VitaminB6LB = this.VitaminB6LB,
                VitaminB6II = this.VitaminB6II,
                VitaminB6UB = this.VitaminB6UB,

                VitaminB12LB = this.VitaminB12LB,
                VitaminB12II = this.VitaminB12II,
                VitaminB12UB = this.VitaminB12UB,

                VitaminCLB = this.VitaminCLB,
                VitaminCII = this.VitaminCII,
                VitaminCUB = this.VitaminCUB,

                VitaminDLB = this.VitaminDLB,
                VitaminDII = this.VitaminDII,
                VitaminDUB = this.VitaminDUB,

                VitaminELB = this.VitaminELB,
                VitaminEII = this.VitaminEII,
                VitaminEUB = this.VitaminEUB,

                ThiaminLB = this.ThiaminLB,
                ThiaminII = this.ThiaminII,
                ThiaminUB = this.ThiaminUB,

                RiboflavinLB = this.RiboflavinLB,
                RiboflavinII = this.RiboflavinII,
                RiboflavinUB = this.RiboflavinUB,

                NiacinLB = this.NiacinLB,
                NiacinII = this.NiacinII,
                NiacinUB = this.NiacinUB,

                FolateLB = this.FolateLB,
                FolateII = this.FolateII,
                FolateUB = this.FolateUB,

                SaltLB = this.SaltLB,
                SaltII = this.SaltII,
                SaltUB = this.SaltUB,

                PotassiumLB = this.PotassiumLB,
                PotassiumII = this.PotassiumII,
                PotassiumUB = this.PotassiumUB,

                MagnesiumLB = this.MagnesiumLB,
                MagnesiumII = this.MagnesiumII,
                MagnesiumUB = this.MagnesiumUB,

                IronLB = this.IronLB,
                IronII = this.IronII,
                IronUB = this.IronUB,

                ZincLB = this.ZincLB,
                ZincII = this.ZincII,
                ZincUB = this.ZincUB,

                PhosphorusLB = this.PhosphorusLB,
                PhosphorusII = this.PhosphorusII,
                PhosphorusUB = this.PhosphorusUB,

                CopperLB = this.CopperLB,
                CopperII = this.CopperII,
                CopperUB = this.CopperUB,

                IodineLB = this.IodineLB,
                IodineII = this.IodineII,
                IodineUB = this.IodineUB,

                SeleniumLB = this.SeleniumLB,
                SeleniumII = this.SeleniumII,
                SeleniumUB = this.SeleniumUB,

                CalciumLB = this.CalciumLB,
                CalciumII = this.CalciumII,
                CalciumUB = this.CalciumUB,
            };
}