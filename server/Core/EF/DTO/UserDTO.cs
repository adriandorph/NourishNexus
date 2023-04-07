using System.ComponentModel;

namespace server.Core.EF.DTO;

public record UserDTO(
    int Id,
    string Nickname,
    [EmailAddress]
    string Email,
    List<int> SavedRecipeIds
);

public record UserAuthDTO(
    int Id,
    string Nickname,
    string Email,
    byte[] PasswordHash,
    byte[] PasswordSalt
);

public record LoginRequest
(
    [Required, EmailAddress]
    string Email = "",
    [Required, ] 
    string Password = ""
);


public record UserCreateDTO 
{
    [Required]
    public string? Nickname {get; set;}
    [Required, EmailAddress]
    public string? Email {get; set;}
    [Required, MinLength(6, ErrorMessage = "Password needs to be of minimum 6 characters")]
    public string? Password {get; set;}
    [Required, Compare("Password")]
    public string? ConfirmPassword {get; set;}
}

public record UserChangePasswordDTO{
    [Required, MinLength(6, ErrorMessage = "Password needs to be of minimum 6 characters")]
    public string? Password {get; set;}
    [Required, Compare("Password", ErrorMessage = "The passwords do not match")]
    public string? ConfirmPassword {get; set;}
}

public record UserUpdateDTO : UserCreateDTO
{
    public int Id {get; set;}
    public List<int>? SavedRecipeIds {get; set;}

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
}


public record UserNutritionDTO(
    int Id,
    string Nickname,
    [EmailAddress]
    string Email,
    List<int> SavedRecipeIds,
    
    float? BreakfastCalories,
    float? LunchCalories,
    float? DinnerCalories,
    float? SnackCalories,

    float? ProteinLB,
    float? ProteinII,
    float? ProteinUB,

    float? CarbohydratesLB,
    float? CarbohydratesII,
    float? CarbohydratesUB,

    float? SugarsLB,
    float? SugarsII,
    float? SugarsUB,

    float? FibresLB,
    float? FibresII,
    float? FibresUB,

    float? TotalFatLB,
    float? TotalFatII,
    float? TotalFatUB,

    float? SaturatedFatLB,
    float? SaturatedFatII,
    float? SaturatedFatUB,

    float? MonounsaturatedFatLB,
    float? MonounsaturatedFatII,
    float? MonounsaturatedFatUB,

    float? PolyunsaturatedFatLB,
    float? PolyunsaturatedFatII,
    float? PolyunsaturatedFatUB,

    float? TransFatLB,
    float? TransFatII,
    float? TransFatUB,

    float? VitaminALB,
    float? VitaminAII,
    float? VitaminAUB,

    float? VitaminB6LB,
    float? VitaminB6II,
    float? VitaminB6UB,

    float? VitaminB12LB,
    float? VitaminB12II,
    float? VitaminB12UB,

    float? VitaminCLB,
    float? VitaminCII,
    float? VitaminCUB,

    float? VitaminDLB,
    float? VitaminDII,
    float? VitaminDUB,

    float? VitaminELB,
    float? VitaminEII,
    float? VitaminEUB,

    float? ThiaminLB,
    float? ThiaminII,
    float? ThiaminUB,

    float? RiboflavinLB,
    float? RiboflavinII,
    float? RiboflavinUB,

    float? NiacinLB,
    float? NiacinII,
    float? NiacinUB,

    float? FolateLB,
    float? FolateII,
    float? FolateUB,

    float? SaltLB,
    float? SaltII,
    float? SaltUB,

    float? PotassiumLB,
    float? PotassiumII,
    float? PotassiumUB,

    float? MagnesiumLB,
    float? MagnesiumII,
    float? MagnesiumUB,

    float? IronLB,
    float? IronII,
    float? IronUB,

    float? ZincLB,
    float? ZincII,
    float? ZincUB,

    float? PhosphorusLB,
    float? PhosphorusII,
    float? PhosphorusUB,

    float? CopperLB,
    float? CopperII,
    float? CopperUB,

    float? IodineLB,
    float? IodineII,
    float? IodineUB,

    float? SeleniumLB,
    float? SeleniumII,
    float? SeleniumUB,

    float? CalciumLB,
    float? CalciumII,
    float? CalciumUB
);


