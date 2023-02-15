//Recipe Category relation. Many to Many

public record RecipeCategoryDTO(
    int Id,
    int RecipeId,
    int CategoryId
);

public record RecipeCategoryCreateDTO
{
    public int RecipeId {get; set;}
    public int CategoryId {get; set;}
}

public record RecipeCategoryUpdateDTO : RecipeCategoryCreateDTO
{
    public int Id {get; set;}
}