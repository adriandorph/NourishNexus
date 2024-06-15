using Microsoft.OpenApi.Extensions;

namespace server.Core;
public enum NutrientType {
        Calories,
        Protein,
        Fat,
        Carbohydrates,
        Fiber,

        //TODO: add all nutrients
}

public static class NutrientTypeExtensions {
    public static Unit GetUnit(this NutrientType nutrientType) {
        return nutrientType switch {
            NutrientType.Calories => Unit.Kcal,
            NutrientType.Protein => Unit.Gram,
            NutrientType.Fat => Unit.Gram,
            NutrientType.Carbohydrates => Unit.Gram,
            NutrientType.Fiber => Unit.Gram,
            _ => throw new NutrientUnitNotDefinedException(nutrientType)
        };
    }
}

public class NutrientUnitNotDefinedException(NutrientType nutrientType) : 
    Exception($"Unit not defined for nutrient type: {nutrientType.GetUnit()}")
{}