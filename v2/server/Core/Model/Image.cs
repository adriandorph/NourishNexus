namespace server.Core.Model;

public class Image
{
    [BsonId]
    [BsonRepresentation(BsonType.ObjectId)]
    public string Id { get; set; } = "";
    public string ImageBase64 { get; set; } = "";
}