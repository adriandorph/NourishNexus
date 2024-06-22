using server.Core.Infrastructure.MongoDB;

namespace server.Infrastructure.MongoDB;

public class AuthenticationRepository(IMongoDatabase mongoDB) : IAuthenticationRepository
{
    private readonly IMongoDatabase _mongoDB = mongoDB;

    public async Task<AuthenticationModel?> CreateAuthentication(AuthenticationModel authentication)
    {
        //Check if duplicate
        var collection = _mongoDB.GetCollection<AuthenticationModel>("Authentication");
        var filter = Builders<AuthenticationModel>.Filter.Eq("UserId", authentication.UserId);
        var existing = await collection.Find(filter).FirstOrDefaultAsync();
        if (existing != null) await DeleteAuthentication(authentication.UserId!);

        await collection.InsertOneAsync(authentication);
        return authentication;
    }

    public async Task DeleteAuthentication(string userId)
    {
        var collection = _mongoDB.GetCollection<AuthenticationModel>("Authentication");
        var filter = Builders<AuthenticationModel>.Filter.Eq("UserId", userId);
        await collection.DeleteOneAsync(filter);
    }

    public async Task<AuthenticationModel?> GetAuthenticationByUserId(string userId)
    {
        var collection = _mongoDB.GetCollection<AuthenticationModel>("Authentication");
        var filter = Builders<AuthenticationModel>.Filter.Eq("UserId", userId);
        return await collection.Find(filter).FirstOrDefaultAsync();
    }

    public async Task UpdateAuthentication(AuthenticationModel authentication)
    {
        var collection = _mongoDB.GetCollection<AuthenticationModel>("Authentication");
        var filter = Builders<AuthenticationModel>.Filter.Eq("UserId", authentication.UserId);
        await collection.ReplaceOneAsync(filter, authentication);
    }
}
