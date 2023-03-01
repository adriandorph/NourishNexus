namespace server.Core.EF.RepositoryInterfaces;

public interface IMealRepository
{
    //Create
    public Task<(Response, MealDTO)> CreateAsync(MealCreateDTO item);

    //Update
    public Task<Response> UpdateAsync(MealUpdateDTO item);

    //Delete
    public Task<Response> RemoveAsync(int id);
    
    //Read
    public Task<Option<MealDTO>> ReadByIDAsync(int id);
    public Task<Option<MealDTO>> ReadByUserIdDateAndMealTypeAsync(DateTime date, int userID, MealType mealType);
    public Task<IReadOnlyCollection<MealDTO>> ReadAllByDateAndUser(DateTime date, int userID);
}