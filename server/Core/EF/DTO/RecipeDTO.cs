namespace server.Core.EF.DTO;

public record RecipeDTO(
    int Id,
    string Title,
    bool IsPublic,
    string Description,
    string Method,
    int AuthorId,
    List<int> CategoryIDs
);


public record RecipeCreateDTO{
    public string? Title {get; set;}
    public bool? IsPublic {get; set;}
    public string? Description {get; set;}
    public string? Method {get; set;}
    public int AuthorId {get; set;} 
    public List<int>? CategoryIDs {get; set;}
}

public record RecipeUpdateDTO : RecipeCreateDTO
{
    public int Id {get; set;}
    public List<FoodItemRecipeCreateDTO>? FoodItemRecipes {get; set;}
}


public record RecipeMealDTO(int Id, float Amount, int RecipeID, int MealID);

public record RecipeMealCreateDTO
{
    public float Amount {get; set;}
    public int RecipeID {get; set;}
    public int MealID {get; set;}
}

public record RecipeAmountDTO(float Amount, RecipeDTO Recipe);




