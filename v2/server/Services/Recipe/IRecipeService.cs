using server.Services.DataSource;

namespace server.Services.Recipe;

public interface IRecipeService
{
    Task<RecipeModel> CreateRecipe();
}