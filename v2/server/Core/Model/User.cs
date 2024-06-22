namespace server.Core.Model;

public class User 
{
    [BsonId]
    [BsonRepresentation(BsonType.ObjectId)]
    public string? Id { get; set; }
    public string[] FollowingIds { get; set; } = [];
    public string[] FriendsIds { get; set; } = [];
    public string[] SavedRecipeIds { get; set; } = [];
    public string? Email { get; set; }
    public string? Nickname { get; set; }
    public string? ProfilePictureId { get; set; }
    public string? Bio { get; set; }
}