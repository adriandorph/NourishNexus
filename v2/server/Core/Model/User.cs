namespace server.Core.Model;

public class User 
{
    [BsonId]
    [BsonRepresentation(BsonType.ObjectId)]
    public string? Id { get; set; }
    public List<string> FollowingIds { get; set; } = [];
    public List<string> FriendsIds { get; set; } = [];
    public List<string> SavedRecipeIds { get; set; } = [];
    public string? Email { get; set; }
    public string? Nickname { get; set; }
    public string? ProfilePictureId { get; set; }
    public string? Bio { get; set; }
}