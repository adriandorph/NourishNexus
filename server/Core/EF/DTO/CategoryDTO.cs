namespace server.Core.EF.DTO;

public record CategoryDTO(
    int Id,
    string Name,
    List<int> RecipeIDs
);

#nullable disable
public record CategoryCreateDTO
{
    public string Name {get; set;}
}

public record CategoryUpdateDTO : CategoryCreateDTO
{
    public int Id {get; set;}
}


