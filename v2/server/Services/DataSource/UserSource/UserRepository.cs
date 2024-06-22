using MongoDB.Driver;
using server.Core;

namespace server.Services.DataSource.UserSource;

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

    public async Task UpdateUser(User user)
    {
        var collection = _mongoDB.GetCollection<User>("Users");
        var filter = Builders<User>.Filter.Eq("Id", user.Id);
        await collection.ReplaceOneAsync(filter, user);
    }

    public async Task DeleteUser(string userId)
    {
        var collection = _mongoDB.GetCollection<User>("Users");
        var filter = Builders<User>.Filter.Eq("Id", userId);
        await collection.DeleteOneAsync(filter);
    }

    public async Task<User?> GetUserById(string userId)
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