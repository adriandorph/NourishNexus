using System.Collections.Generic;
using System.Collections.ObjectModel;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace server.Services.DataSource;

public class UserModel {
    [BsonId]
    [BsonRepresentation(BsonType.ObjectId)]
    public string? Id { get; set; }
    public string[] FollowingIds { get; set; } = [];
    public string[] FriendsIds { get; set; } = [];
    public string? Email { get; set; }
    public string? Username { get; set; }
    public string? ProfilePictureBase64 { get; set; }
    public string? Bio { get; set; }
}