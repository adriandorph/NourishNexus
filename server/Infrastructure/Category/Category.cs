namespace server.Infrastructure;
using server.Core.EF;

public class Category 
{
    public int Id {get; set;}
    public string CategoryName {get; set;}
    public List<Recipe> Recipes {get; set;}

    public Category(string CategoryName)
    {
        this.CategoryName = CategoryName;
        this.Recipes = new List<Recipe>();
    }

#nullable disable
    public Category(){}

    public CategoryDTO ToDTO() 
        => new CategoryDTO(Id, CategoryName, Recipes.Select(r => r.Id).ToList());
}
