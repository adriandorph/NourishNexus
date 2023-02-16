namespace server.Core.EF.DTO;

public record MealDTO(
    int Id,
    int mealType,
    int UserId,
    DateTime Date
);