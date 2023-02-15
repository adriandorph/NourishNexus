namespace server.Infrastructure;

public class Recipe {
    public int Id {get; set;}
    public string Title {get; set;} = "";
    public bool IsPublic {get; set;}
    public string Description {get; set;} = "";
    public string Method {get; set;} = "";
    public User Author {get; set;}
    //public ICollection<Category> Categories {get; set;}
    //public ICollection<FoodItem> FoodItems {get; set;}

    public Recipe(string title, bool isPublic, string description, string method, User author)
    {
        this.Title = title;
        this.IsPublic = isPublic;
        this.Description = description;
        this.Method = method;
        this.Author = author;
        //this.Categories = categories;
        //this.FoodItems = foodItems;
    }

    #nullable disable
    public Recipe() {}
    #nullable enable
    public RecipeDTO ToDTO() 
        => new RecipeDTO(Id, Title, IsPublic, Description, Method, Author.Id);
        
}



