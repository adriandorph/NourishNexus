namespace server.Core.EF.RepositoryInterfaces;

public interface IRecipeFoodItemRepository{
    //Create
    public Task<(Response, RecipeDTO)> CreateAsync(RecipeCreateDTO item);

    //Delete
    public Task<Response> RemoveAsync(int id);
    
    //Read
    public Task<Option<RecipeDTO>> ReadByIDAsync(int Id);
    public Task<Option<RecipeDTO>> ReadAllByRecipeIDAsync(int recipeID);
}