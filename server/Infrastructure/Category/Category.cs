namespace server.Infrastructure;
using server.Core.EF;

public class Category 
{
    public int Id {get; set;}
    public string Name {get; set;}
    public List<Recipe> Recipes {get; set;}

    public Category(string Name)
    {
        this.Name = Name;
        this.Recipes = new List<Recipe>();
    }

#nullable disable
    public Category(){}

    public CategoryDTO ToDTO() 
        => new CategoryDTO(Id, Name, Recipes.Select(r => r.Id).ToList());
}
