namespace client.Services;
using server.Core.EF.DTO;
using System.Net.Http.Json;
using System.Net.Http;
using server.Core.EF;

public class PlanningService
{

    private readonly HttpClient _http;

    public PlanningService(HttpClient http)
    {
        _http = http;
    }

    public async Task<MealPlanResponse> GenerateMealPlan(int userID, DateTime startingDate)
    {
        var dateString = startingDate.ToString("yyyy-MM-ddTHH:mm:ss").Replace(".", "%3A");

        var result = await _http.PostAsync($"api/Planning?userID={userID}&startingDate={dateString}", null);
        return await result.Content.ReadFromJsonAsync<MealPlanResponse>();
    }

    public async Task<HttpResponseMessage> SetTargets(int userID, int age, Gender gender, float height, float weight, float pal, WeightGoal weightGoal)
    {
        var submitForm = new IntakeTargetForm(userID, age, gender, weight, height, pal, weightGoal);
        return await _http.PutAsJsonAsync<IntakeTargetForm>($"api/Planning/targets", submitForm);
    }
}