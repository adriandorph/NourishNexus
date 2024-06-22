namespace server.Core.Services.RecipeManagement;

public interface IRecipeService
{
    Task<Recipe> CreateRecipe();
}