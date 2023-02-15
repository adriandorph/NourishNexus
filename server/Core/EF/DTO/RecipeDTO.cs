namespace server.Core.EF.DTO;
#nullable disable

public record RecipeDTO(
    int Id,
    string Title,
    bool IsPublic,
    string Description,
    string Method,
    int AuthorId
);

public record RecipeCreateDTO{
    public string Title {get; set;}
    public bool? IsPublic {get; set;}
    public string Description {get; set;}
    public string Method {get; set;}
    public int AuthorId {get; set;} 
}

public record RecipeUpdateDTO : RecipeCreateDTO
{
    public int Id {get; set;}
}




