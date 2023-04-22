namespace client.ServiceInterfaces;
using server.Core.EF.DTO;

public interface IRecipeService
{
        Task<HttpResponseMessage> CreateRecipe(RecipeCreateDTO recipe);
        Task<HttpResponseMessage> DeleteRecipe(int id);
        Task<List<RecipeDTO>> GetFromCommunityBySearchWord(string word);
        Task<List<RecipeDTO>> GetPublicRecipes();
        Task<RecipeDTO> GetRecipe(int recipeID);
        Task<List<RecipeAmountDTO>> GetByMeal(int mealID);
        Task<List<RecipeDTO>> GetSavedBySearchWord(string word, int userID);
        Task<HttpResponseMessage> UpdateRecipe(RecipeUpdateDTO recipe);
        Task<HttpResponseMessage> GetRecipesByAuthorID(int authorID);
}
