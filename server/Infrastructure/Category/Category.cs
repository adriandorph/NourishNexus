namespace server.Infrastructure;
using server.Core.EF;

public class Category 
{
    public int Id {get; set;}
    public CategoryEnum CategoryName {get; set;}
    public Recipe Recipe {get; set;}

    public Category(CategoryEnum category, Recipe recipe)
    {
        this.CategoryName = category;
        this.Recipe = recipe;
    }
}
