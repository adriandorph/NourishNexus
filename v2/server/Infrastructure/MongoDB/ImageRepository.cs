using server.Core.Infrastructure.DataBase;

namespace server.Infrastructure.MongoDB;

public class ImageRepository(IMongoDatabase mongoDB) : IImageRepository
{
    private readonly IMongoDatabase _mongoDB = mongoDB;

    public async Task<Image?> CreateImageAsync(Image image)
    {
        var collection = _mongoDB.GetCollection<Image>("Images");
        await collection.InsertOneAsync(image);
        var filter = Builders<Image>.Filter.Eq("Id", image.Id);
        return await collection.Find(filter).FirstOrDefaultAsync();
    }

    public async Task<Image?> UpdateImageAsync(Image image)
    {
        var collection = _mongoDB.GetCollection<Image>("Images");
        var filter = Builders<Image>.Filter.Eq("Id", image.Id);
        var result = await collection.ReplaceOneAsync(filter, image);
        return result.IsAcknowledged && result.MatchedCount > 1 ? image : null;
    }

    public async Task<bool> DeleteImageAsync(string id)
    {
        var collection = _mongoDB.GetCollection<Image>("Images");
        var filter = Builders<Image>.Filter.Eq("Id", id);
        var result = await collection.DeleteOneAsync(filter);
        return result.IsAcknowledged && result.DeletedCount > 0;
    }

    public async Task<Image?> GetImageByIdAsync(string id)
    {
        var collection = _mongoDB.GetCollection<Image>("Images");
        var filter = Builders<Image>.Filter.Eq("Id", id);
        return await collection.Find(filter).FirstOrDefaultAsync();
    }
    

    public async Task<List<Image>> GetImagesByIdsAsync(List<string> ids)
    {
        var collection = _mongoDB.GetCollection<Image>("Images");
        var filter = Builders<Image>.Filter.In("Id", ids);
        return await collection.Find(filter).ToListAsync();
    }
}