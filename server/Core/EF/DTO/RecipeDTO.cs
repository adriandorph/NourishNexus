namespace server.Core.EF;
#nullable disable

public record RecipeDTO(
    int Id,
    string Title,
    bool isPublic,
    string Method,
    int authorId
);

public record RecipeUpdateDTO{
    public int Id {get; set;}
    public string Title {get; set;}
    public bool isPublic {get; set;}
    public string Method {get; set;}
    public int authorId {get; set;} 
}


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


//Recipe FoodItem relation. Many to Many

public record RecipeFoodItemDTO(
    int Id,
    int RecipeId,
    int FoodItemId
);

public record RecipeFoodItemCreateDTO
{
    public int RecipeId {get; set;}
    public int FoodItemId {get; set;}
}

public record RecipeFoodItemUpdateDTO : RecipeFoodItemCreateDTO
{
    public int Id {get; set;}
}