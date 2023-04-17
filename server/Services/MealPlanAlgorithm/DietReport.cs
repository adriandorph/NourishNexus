namespace server.Services.MealPlan;

public record DietReport
(
    NutrientTargets? LB,
    NutrientTargets? II,
    NutrientTargets? UB,
    NutrientTargets? PlannedIntake,
    MealPlanResponse Response,
    MealPlan? MealPlan
);