namespace server.Services.RecipeManagement;

public interface IRecipeService
{
    Task<Recipe> CreateRecipe();
}