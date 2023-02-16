namespace server.Core.EF.DTO;

public record CategoryDTO(
    int Id,
    string CategoryName,
    List<int> RecipeID
);

#nullable disable
public record CategoryCreateDTO{
    public string CategoryName {get; set;}
}

public record CategoryUpdateDTO : CategoryCreateDTO
{
    public int Id {get; set;}
    public List<int> RecipeID {get; set;}
}


