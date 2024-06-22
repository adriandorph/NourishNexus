using server.Core.Infrastructure.DataBase;

namespace server.Infrastructure.MongoDB;

public class FoodItemRepository(IMongoDatabase mongoDB) : IFoodItemRepository
{
    private readonly IMongoDatabase _mongoDB = mongoDB;

    public async Task<FoodItem> CreateFoodItem(FoodItem foodItem)
    {
        var collection = _mongoDB.GetCollection<FoodItem>("FoodItems");
        await collection.InsertOneAsync(foodItem);

        var filter = Builders<FoodItem>.Filter.Eq("Id", foodItem.Id);
        return await collection.Find(filter).FirstOrDefaultAsync();
    }

    public async Task UpdateFoodItem(FoodItem foodItem)
    {
        var collection = _mongoDB.GetCollection<FoodItem>("FoodItems");
        var filter = Builders<FoodItem>.Filter.Eq("Id", foodItem.Id);
        await collection.ReplaceOneAsync(filter, foodItem);
    }

    public async Task DeleteFoodItem(string foodItemId)
    {
        var collection = _mongoDB.GetCollection<FoodItem>("FoodItems");
        var filter = Builders<FoodItem>.Filter.Eq("Id", foodItemId);
        await collection.DeleteOneAsync(filter);
    }

    public async Task<FoodItem> GetFoodItemById(string foodItemId)
    {
        var collection = _mongoDB.GetCollection<FoodItem>("FoodItems");
        var filter = Builders<FoodItem>.Filter.Eq("Id", foodItemId);
        return await collection.Find(filter).FirstOrDefaultAsync();
    }

    public async Task<List<FoodItem>> GetAllFoodItems()
    {
        var collection = _mongoDB.GetCollection<FoodItem>("FoodItems");
        return await collection.Find(_ => true).ToListAsync();
    }

    public async Task<List<FoodItem>> GetFoodItemsByIds(List<string> foodItemIds)
    {
        var collection = _mongoDB.GetCollection<FoodItem>("FoodItems");
        var filter = Builders<FoodItem>.Filter.In("Id", foodItemIds);
        return await collection.Find(filter).ToListAsync();
    }
}