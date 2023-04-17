using System.Net;
namespace client.Services;
using server.Core.EF.DTO;
using System.Net.Http.Json;
using System.Net.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.JSInterop;
using server.Core.EF;
using server.Core;

public class PlanningService
{

    private readonly HttpClient _http;

    public PlanningService(HttpClient http)
    {
        _http = http;
        if (_http.BaseAddress != new Uri("http://localhost:5288"))
        {
            _http.BaseAddress = new Uri("http://localhost:5288");
        }
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