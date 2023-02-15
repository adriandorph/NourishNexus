namespace server.Infrastructure;
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

    #nullable disable
    public RecipeCategory() {}
}