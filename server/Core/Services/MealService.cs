namespace server.Core.Services;

public interface IMealService
{
    public Task<IActionResult> ReportMeal(MealUpdateDTO mealUpdateDTO);
}

public class MealService
{
    IMealRepository _repo;

    public MealService(IMealRepository repo)
    {
        _repo = repo;
    }

    public Task<IActionResult> ReportMeal(MealUpdateDTO mealUpdateDTO)
    {
        //If the meal doesnt exist, create it
        //If the meal exists update the fooditems and recipes in the meal
        //If the meal is updated to have no fooditems or recipes, delete it.
        throw new NotImplementedException();
    }
}