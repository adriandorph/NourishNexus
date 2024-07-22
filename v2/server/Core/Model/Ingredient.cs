namespace server.Core.Model;
public class Ingredient
{
    public string Name { get; set; } = "Ingredient Name";
    public float Quantity { get; set; }
    public Unit Unit { get; set; }
    public bool HasNutrition { get {return Nutrients.Count > 0;} }

    /// <summary>
    /// Nutrients are in total per the specified Quantity and Unit.
    /// </summary>
    public List<Nutrient> Nutrients { get; set; } = [];

    public IngredientDTO ToDTO()
    => new (
        Name, 
        Quantity, 
        Unit.GetDescription(), 
        Nutrients.Select(n => n.ToDTO()).ToList()
    );

    public static Ingredient FromDTO(IngredientDTO dto)
    => new()
    {
        Name = dto.Name,
        Quantity = dto.Amount,
        Unit = dto.Unit.ToUnit(),
        Nutrients = dto.Nutrients.Select(Nutrient.FromDTO).ToList()
    };
}

public record IngredientDTO(
    [Required] string Name,
    [Required] float Amount,
    [Required] string Unit,
    [Required] List<NutrientDTO> Nutrients
);