namespace server.Core.EF.DTO;

public record IntakeTargetForm(int UserID, int Age, Gender Gender, float Weight, float Height, float PAL, WeightGoal WeightGoal);