namespace client.ServiceInterfaces;
using server.Core.EF.DTO;
using System.Net.Http;
using server.Core.EF;

public interface IPlanningService
{
    Task<MealPlanResponse> GenerateMealPlan(int userID, DateTime startingDate);
    Task<HttpResponseMessage> SetTargets(int userID, int age, Gender gender, float height, float weight, float pal, WeightGoal weightGoal);
}