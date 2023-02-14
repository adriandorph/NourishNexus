namespace server.Infrastructure;

public class Recipe {
    
    public string Title {get; set;} = "";
    public bool IsPublic {get; set;}
    public string Description {get; set;} = "";
    public string Method {get; set;} = "";
    public User Author {get; set;}

    public Recipe(string title, bool isPublic, string description, string method, User author)
    {
        this.Title = title;
        this.IsPublic = isPublic;
        this.Description = description;
        this.Method = method;
        this.Author = author;
    }
}

public class RecipeCategory 
{
    public int Id {get; set;}
    public Recipe Recipe {get; set;}
    public Category Category {get; set;}

    public RecipeCategory(Recipe recipe, Category category)
    {
        this.Recipe = recipe;
        this.Category = category;
    }
}

public class RecipeFoodItem
{
    public int Id {get; set;}
    public Recipe recipe {get; set;}
    public FoodItem foodItem {get; set;}
    
    public RecipeFoodItem(Recipe recipe, FoodItem foodItem){
        this.recipe = recipe;
        this.foodItem = foodItem;
    }
}