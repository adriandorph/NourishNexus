namespace server.Core.EF.DTO;

public record FoodItemDTO(
    int Id,

    string Name,

    Unit Unit,

    float Calories,

    float Protein
);

#nullable disable
public record FoodItemCreateDTO{

    public string Name {get; set;}
    public Unit? Unit {get; set;}
    public float? Calories {get; set;}
    public float? Protein {get; set;}
}

public record FoodItemUpdateDTO : FoodItemCreateDTO
{
    public int Id {get; set;}
}



