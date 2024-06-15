using MongoDB.Driver;

namespace server.Services.DataSource;

public class UserRepository(IMongoDatabase mongoDB) : IUserRepository 
{
    private readonly IMongoDatabase _mongoDB = mongoDB;

    public async Task<UserModel?> CreateUser(UserModel user)
    {
        // Insert user into database
        var collection = _mongoDB.GetCollection<UserModel>("Users");
        await collection.InsertOneAsync(user);
        var filter = Builders<UserModel>.Filter.Eq("Id", user.Id);

        return await collection.Find(filter).FirstOrDefaultAsync();
    }

    public async Task UpdateUser(UserModel user)
    {
        var collection = _mongoDB.GetCollection<UserModel>("Users");
        var filter = Builders<UserModel>.Filter.Eq("Id", user.Id);
        await collection.ReplaceOneAsync(filter, user);
    }

    public async Task DeleteUser(string userId)
    {
        var collection = _mongoDB.GetCollection<UserModel>("Users");
        var filter = Builders<UserModel>.Filter.Eq("Id", userId);
        await collection.DeleteOneAsync(filter);
    }

    public async Task<UserModel?> GetUserById(string userId)
    {
        var collection = _mongoDB.GetCollection<UserModel>("Users");
        var filter = Builders<UserModel>.Filter.Eq("Id", userId);
        return await collection.Find(filter).FirstOrDefaultAsync();
    }
    public async Task<UserModel?> GetUserByEmail(string email)
    {
        var collection = _mongoDB.GetCollection<UserModel>("Users");
        var filter = Builders<UserModel>.Filter.Eq("Email", email);
        return await collection.Find(filter).FirstOrDefaultAsync();
    }

    public async Task<List<UserModel>> GetAllUsers()
    {
        var collection = _mongoDB.GetCollection<UserModel>("Users");
        return await collection.Find(_ => true).ToListAsync();
    }

    public async Task<List<UserModel>> GetUsersByIds(List<string> userIds)
    {
        var collection = _mongoDB.GetCollection<UserModel>("Users");
        var filter = Builders<UserModel>.Filter.In("Id", userIds);
        return await collection.Find(filter).ToListAsync();
    }
}