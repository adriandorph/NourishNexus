namespace server.Infrastructure;

public class Recipe {
    public int Id {get; set;}
    public string Title {get; set;} = "";
    public bool IsPublic {get; set;}
    public string Description {get; set;} = "";
    public string Method {get; set;} = "";
    public int AuthorId {get; set;}
    public List<Category> Categories {get; set;}
    public bool IsBreakfast {get; set;}
    public bool IsLunch {get; set;}
    public bool IsDinner {get; set;}
    public bool IsSnack {get; set;}

    public Recipe(string title, bool isPublic, string description, string method, int authorId, List<Category> categories, bool isBreakfast, bool isLunch, bool isDinner, bool isSnack)
    {
        this.Title = title;
        this.IsPublic = isPublic;
        this.Description = description;
        this.Method = method;
        this.AuthorId = authorId;
        this.Categories = categories;
        this.IsBreakfast = isBreakfast;
        this.IsLunch = isLunch;
        this.IsDinner = isDinner;
        this.IsSnack = isSnack;

    }

    #nullable disable
    public Recipe() {}
    #nullable enable

    public RecipeDTO ToDTO()
        => new RecipeDTO(
            this.Id, 
            this.Title, 
            this.IsPublic, 
            this.Description, 
            this.Method, 
            this.AuthorId, 
            this.Categories.Select(c => c.Id).ToList(),
            this.IsBreakfast,
            this.IsLunch,
            this.IsDinner,
            this.IsSnack
        );
        
}



