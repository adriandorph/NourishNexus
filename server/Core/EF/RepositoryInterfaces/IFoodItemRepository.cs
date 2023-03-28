namespace server.Core.EF.RepositoryInterfaces;
using server.Core.EF.DTO;

public interface IFoodItemRepository{

    //Create
    public Task<(Response, FoodItemDTO)> CreateAsync(FoodItemCreateDTO item);

    //Update
    public Task<Response> UpdateAsync(FoodItemUpdateDTO item);

    //Delete
    public Task<Response> RemoveAsync(int id);
    
    //Read
    public Task<Option<FoodItemDTO>> ReadByIDAsync(int itemID);

    public Task<IReadOnlyCollection<FoodItemDTO>> ReadAllAsync();

    public Task<IReadOnlyCollection<FoodItemAmountDTO>> ReadAllByMealId(int mealID);

    public Task<IReadOnlyCollection<FoodItemAmountDTO>> ReadAllByRecipeId(int recipeID);

}
    