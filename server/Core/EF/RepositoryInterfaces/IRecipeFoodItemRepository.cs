namespace server.Core.EF.RepositoryInterfaces;

public interface IRecipeFoodItemRepository{
    //Create
    public Task<(Response, RecipeFoodItemDTO)> CreateAsync(RecipeFoodItemCreateDTO item);

    //Delete
    public Task<Response> RemoveAsync(int id);
    
    //Read
    public Task<Option<RecipeFoodItemDTO>> ReadByIDAsync(int Id);
    public Task<IReadOnlyCollection<RecipeFoodItemDTO>> ReadAllByRecipeIDAsync(int recipeID);
}