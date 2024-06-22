namespace server.Core.Model;
public class Fork
{
    public string? ForkedFromRecipeId { get; set; }
    public string? ForkedFromAuthorId { get; set; }

    public Fork() {}
    public Fork(string recipeId, string authorId) {
        ForkedFromRecipeId = recipeId;
        ForkedFromAuthorId = authorId;
    }
}