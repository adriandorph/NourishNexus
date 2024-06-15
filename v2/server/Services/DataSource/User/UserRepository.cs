using MongoDB.Driver;

namespace server.Services.DataSource.User;

public class UserRepository(IMongoClient mongoClient){
    private readonly IMongoDatabase _mongoDB = mongoClient.GetDatabase("Users");

    public async Task<UserModel> CreateUser(UserModel user){
        var collection = _mongoDB.GetCollection<UserModel>("Users");
        await collection.InsertOneAsync(user);
        return user;
    }

    public async Task UpdateUser(UserModel user){
        var collection = _mongoDB.GetCollection<UserModel>("Users");
        var filter = Builders<UserModel>.Filter.Eq("Id", user.Id);
        await collection.ReplaceOneAsync(filter, user);
    }

    public async Task DeleteUser(string userId){
        var collection = _mongoDB.GetCollection<UserModel>("Users");
        var filter = Builders<UserModel>.Filter.Eq("Id", userId);
        await collection.DeleteOneAsync(filter);
    }

    public async Task<UserModel> GetUserById(string userId){
        var collection = _mongoDB.GetCollection<UserModel>("Users");
        var filter = Builders<UserModel>.Filter.Eq("Id", userId);
        return await collection.Find(filter).FirstOrDefaultAsync();
    }

    public async Task<List<UserModel>> GetAllUsers(){
        var collection = _mongoDB.GetCollection<UserModel>("Users");
        return await collection.Find(_ => true).ToListAsync();
    }

    public async Task<List<UserModel>> GetUsersByIds(List<string> userIds){
        var collection = _mongoDB.GetCollection<UserModel>("Users");
        var filter = Builders<UserModel>.Filter.In("Id", userIds);
        return await collection.Find(filter).ToListAsync();
    }
}