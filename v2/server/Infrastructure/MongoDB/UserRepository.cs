using server.Core.Infrastructure.DataBase;

namespace server.Infrastructure.MongoDB;

public class UserRepository(IMongoDatabase mongoDB) : IUserRepository 
{
    private readonly IMongoDatabase _mongoDB = mongoDB;

    public async Task<User?> CreateUser(User user)
    {
        // Insert user into database
        var collection = _mongoDB.GetCollection<User>("Users");
        await collection.InsertOneAsync(user);
        var filter = Builders<User>.Filter.Eq("Id", user.Id);

        return await collection.Find(filter).FirstOrDefaultAsync();
    }

    public async Task<User?> UpdateUser(User user)
    {
        var collection = _mongoDB.GetCollection<User>("Users");
        var filter = Builders<User>.Filter.Eq("Id", user.Id);
        var result = await collection.ReplaceOneAsync(filter, user);

        return result.IsAcknowledged && result.MatchedCount > 0 ? user : null; 
    }

    public async Task<bool> DeleteUser(string userId)
    {
        var collection = _mongoDB.GetCollection<User>("Users");
        var filter = Builders<User>.Filter.Eq("Id", userId);
        var result = await collection.DeleteOneAsync(filter);
        return result.IsAcknowledged && result.DeletedCount > 0;
    }

    public async Task<User?> GetUserByIdAsync(string userId)
    {
        var collection = _mongoDB.GetCollection<User>("Users");
        var filter = Builders<User>.Filter.Eq("Id", userId);
        return await collection.Find(filter).FirstOrDefaultAsync();
    }
    public async Task<User?> GetUserByEmail(string email)
    {
        var collection = _mongoDB.GetCollection<User>("Users");
        var filter = Builders<User>.Filter.Eq("Email", email);
        return await collection.Find(filter).FirstOrDefaultAsync();
    }

    public async Task<List<User>> GetAllUsers()
    {
        var collection = _mongoDB.GetCollection<User>("Users");
        return await collection.Find(_ => true).ToListAsync();
    }

    public async Task<List<User>> GetUsersByIds(List<string> userIds)
    {
        var collection = _mongoDB.GetCollection<User>("Users");
        var filter = Builders<User>.Filter.In("Id", userIds);
        return await collection.Find(filter).ToListAsync();
    }
}