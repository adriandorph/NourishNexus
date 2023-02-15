namespace server.Core.EF.DTO;

public record CategoryDTO(
    int Id,
    CategoryEnum CategoryName,
    int RecipeId
);

public record CategoryCreateDTO
{
    public CategoryEnum CategoryName {get; set;}
    public int RecipeId {get; set;}
}

public record CategoryUpdateDTO : CategoryCreateDTO
{
    public int Id {get; set;}
}