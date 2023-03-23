using server.Core.Services.MealPlan;

public record DietReport(
    NutrientTargets? LB,
    NutrientTargets? II,
    NutrientTargets? UB,
    NutrientTargets? PlannedIntake,
    server.Core.Services.MealPlan.Response Response
);