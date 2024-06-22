namespace server.Services.DataSource.FoodItemSource;
public interface IFoodItemRepository
{
    Task<FoodItemModel> CreateFoodItem(FoodItemModel foodItem);
    Task UpdateFoodItem(FoodItemModel foodItem);
    Task DeleteFoodItem(string foodItemId);
    Task<FoodItemModel> GetFoodItemById(string foodItemId);
    Task<List<FoodItemModel>> GetAllFoodItems();
    Task<List<FoodItemModel>> GetFoodItemsByIds(List<string> foodItemIds);
}

