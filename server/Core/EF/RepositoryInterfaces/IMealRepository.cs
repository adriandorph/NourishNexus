namespace server.Core.EF.RepositoryInterfaces;
using server.Core.EF.DTO;
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
    public Task<IReadOnlyCollection<MealDTO>> ReadAllByDateRangeAndUser(int userID, DateTime startDate, DateTime endDate);
    public Task<Option<MealWithFoodDTO>> ReadWithFoodByIDAsync(int id);
    public Task<IReadOnlyCollection<MealWithFoodDTO>> ReadAllWithFoodByUserAndDateAsync(int userID, DateTime date);
}