using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace server.Services.DataSource.Authentication;
public class AuthenticationModel
{
    [BsonId]
    [BsonRepresentation(BsonType.ObjectId)]
    public string? Id { get; set; }
    public string? UserId { get; set; }
    public byte[]? PasswordHash { get; set; }
    public byte[]? PasswordSalt { get; set; }
}