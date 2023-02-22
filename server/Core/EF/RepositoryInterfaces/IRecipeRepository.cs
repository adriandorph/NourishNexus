namespace server.Core.EF.RepositoryInterfaces;

public interface IRecipeRepository{
    //Create
    public Task<(Response, RecipeDTO)> CreateAsync(RecipeCreateDTO item);

    //Update
    public Task<Response> UpdateAsync(RecipeUpdateDTO item);

    //Delete
    public Task<Response> RemoveAsync(int id);
    
    //Read
    public Task<Option<RecipeDTO>> ReadByIDAsync(int recipeID);

    public Task<IReadOnlyCollection<RecipeDTO>> ReadAllByAuthorIDAsync(int authorID);

    public Task<IReadOnlyCollection<RecipeDTO>> ReadAllAsync();

    public Task<Option<RecipeDTO>> ReadByAuthorIDAndTitle(int authorID, string title);

    public Task<IReadOnlyCollection<RecipeDTO>> ReadAllByCategoryIDAsync(int categoryID);
}