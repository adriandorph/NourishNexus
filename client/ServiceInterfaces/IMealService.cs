namespace client.ServiceInterfaces;
using server.Core.EF.DTO;
using server.Core.EF;

    public interface IMealService
    {
        Task<MealDTO?> PostMeal(int userID, DateTime date, MealType type);
        Task<HttpResponseMessage> UpdateMeal(MealUpdateDTO meal);
        Task<List<MealDTO>> GetMealsByUserIDAndDate(int userId, DateTime date);
        Task<MealWithFoodDTO?> GetMealByID(int mealID);
        Task<List<MealWithFoodDTO>> GetMealsWithFoodByUserIDAndDate(int userId, DateTime date);
        Task<Week?> GetWeek(int userID, DateTime startDate);
    }