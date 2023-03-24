using server.Services.MealPlan;

public record DietReport(
    NutrientTargets? LB,
    NutrientTargets? II,
    NutrientTargets? UB,
    NutrientTargets? PlannedIntake,
    server.Services.MealPlan.Response Response
);