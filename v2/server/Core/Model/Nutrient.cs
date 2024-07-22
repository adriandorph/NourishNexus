namespace server.Core.Model;
public class Nutrient
{
    public NutrientType NutrientType { get; set; }
    public float Amount { get; set; }
    public Unit Unit { get; set; }

    public NutrientDTO ToDTO()
        => new (
            NutrientType.GetDescription(), 
            Amount, 
            Unit.GetDescription()
        );
    
    public static Nutrient FromDTO(NutrientDTO dto)
        => new ()
        {
            NutrientType = dto.NutrientType.ToNutrientType(),
            Amount = dto.Amount,
            Unit = dto.Unit.ToUnit()
        };
}

public record NutrientDTO(
    [Required] string NutrientType,
    [Required] float Amount,
    [Required] string Unit
);