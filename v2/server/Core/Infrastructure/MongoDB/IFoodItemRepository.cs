namespace server.Core.Infrastructure.MongoDB;
public interface IFoodItemRepository
{
    Task<FoodItem> CreateFoodItem(FoodItem foodItem);
    Task UpdateFoodItem(FoodItem foodItem);
    Task DeleteFoodItem(string foodItemId);
    Task<FoodItem> GetFoodItemById(string foodItemId);
    Task<List<FoodItem>> GetAllFoodItems();
    Task<List<FoodItem>> GetFoodItemsByIds(List<string> foodItemIds);
}

