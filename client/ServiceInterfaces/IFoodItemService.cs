namespace client.ServiceInterfaces;
using server.Core.EF.DTO;
public interface IFoodItemService
{
    Task<List<FoodItemAmountDTO>> GetByRecipe(int recipeID);
    Task<List<FoodItemDTO>> GetBySearchWord(string word);
    Task<FoodItemDTO?> GetFoodItemById(int fooditemID);
    Task<HttpResponseMessage> SetIngredients(List<FoodItemAmountDTO> foodItems, int recipeID);
    Task<List<FoodItemAmountDTO>> GetByMeal(int mealID);
}