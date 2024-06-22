namespace server.Services.DataSource.FoodItemSource;

public class FoodItemRepository(IMongoDatabase mongoDB) : IFoodItemRepository
{
    private readonly IMongoDatabase _mongoDB = mongoDB;

    public async Task<FoodItemModel> CreateFoodItem(FoodItemModel foodItem)
    {
        var collection = _mongoDB.GetCollection<FoodItemModel>("FoodItems");
        await collection.InsertOneAsync(foodItem);

        var filter = Builders<FoodItemModel>.Filter.Eq("Id", foodItem.Id);
        return await collection.Find(filter).FirstOrDefaultAsync();
    }

    public async Task UpdateFoodItem(FoodItemModel foodItem)
    {
        var collection = _mongoDB.GetCollection<FoodItemModel>("FoodItems");
        var filter = Builders<FoodItemModel>.Filter.Eq("Id", foodItem.Id);
        await collection.ReplaceOneAsync(filter, foodItem);
    }

    public async Task DeleteFoodItem(string foodItemId)
    {
        var collection = _mongoDB.GetCollection<FoodItemModel>("FoodItems");
        var filter = Builders<FoodItemModel>.Filter.Eq("Id", foodItemId);
        await collection.DeleteOneAsync(filter);
    }

    public async Task<FoodItemModel> GetFoodItemById(string foodItemId)
    {
        var collection = _mongoDB.GetCollection<FoodItemModel>("FoodItems");
        var filter = Builders<FoodItemModel>.Filter.Eq("Id", foodItemId);
        return await collection.Find(filter).FirstOrDefaultAsync();
    }

    public async Task<List<FoodItemModel>> GetAllFoodItems()
    {
        var collection = _mongoDB.GetCollection<FoodItemModel>("FoodItems");
        return await collection.Find(_ => true).ToListAsync();
    }

    public async Task<List<FoodItemModel>> GetFoodItemsByIds(List<string> foodItemIds)
    {
        var collection = _mongoDB.GetCollection<FoodItemModel>("FoodItems");
        var filter = Builders<FoodItemModel>.Filter.In("Id", foodItemIds);
        return await collection.Find(filter).ToListAsync();
    }
}