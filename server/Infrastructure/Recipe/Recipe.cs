namespace server.Infrastructure;

public class Recipe {
    public int Id {get; set;}
    public string Title {get; set;} = "";
    public bool IsPublic {get; set;}
    public string Description {get; set;} = "";
    public string Method {get; set;} = "";
    public User Author {get; set;}
    public List<Category> Categories {get; set;}
    public List<FoodItem> FoodItems {get; set;}

    public Recipe(string title, bool isPublic, string description, string method, User author, List<Category> categories, List<FoodItem> foodItems)
    {
        this.Title = title;
        this.IsPublic = isPublic;
        this.Description = description;
        this.Method = method;
        this.Author = author;
        this.Categories = categories;
        this.FoodItems = foodItems;
    }

    #nullable disable
    public Recipe() {}
    #nullable enable
    public static RecipeDTO ToDTO(Recipe r) 
        => new RecipeDTO(
            r.Id, 
            r.Title, 
            r.IsPublic, 
            r.Description, 
            r.Method, 
            r.Author.Id, 
            r.Categories == null ? new List<int>() : r.Categories.Select(c => c.Id).ToList(), 
            r.FoodItems == null ? new List<int>() : r.FoodItems.Select(fi => fi.Id).ToList()
        );
        
}



