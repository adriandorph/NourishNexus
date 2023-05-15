namespace server.Core.EF.RepositoryInterfaces;
using server.Core.EF.DTO;
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
    public Task<IReadOnlyCollection<RecipeDTO>> ReadAllPublicAsync();
    public Task<IReadOnlyCollection<RecipeAmountDTO>> ReadAllByMealId(int mealID);
    public Task<IReadOnlyCollection<RecipeDTO>> ReadSavedBySearchWord(string word, int userID);
    public Task<IReadOnlyCollection<RecipeDTO>> ReadPublicBySearchWord(string word);
}