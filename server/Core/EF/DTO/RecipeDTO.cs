namespace server.Core.EF.DTO;
#nullable disable

public record RecipeDTO(
    int Id,
    string Title,
    bool isPublic,
    string Description,
    string Method,
    int authorId
);

public record RecipeCreateDTO{
    public string Title {get; set;}
    public bool? isPublic {get; set;}
    public string Description {get; set;}
    public string Method {get; set;}
    public int authorId {get; set;} 
}

public record RecipeUpdateDTO : RecipeCreateDTO
{
    public int Id {get; set;}
}




