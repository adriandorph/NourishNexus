namespace server.Core.Model.Enums;
public enum NutrientType {
        Energy,
        Protein,
        Fat,
        Carbs,
        Fibre,
        Sugar,
        SaturatedFat,
        MonounsaturatedFat,
        PolyunsaturatedFat,
        TransFat,
        VitaminA,
        VitaminB6,
        VitaminB12,
        VitaminC,
        VitaminD,
        VitaminE,
        Thiamin,
        Riboflavin,
        Niacin,
        Folate,
        Salt,
        Potassium,
        Magnesium,
        Iron,
        Zinc,
        Phosphorus,
        Copper,
        Iodine,
        Selenium,
        Calcium
}

public static class NutrientTypeExtensions {
    public static Unit GetUnit(this NutrientType nutrientType) {
        return nutrientType switch {
            NutrientType.Energy => Unit.Kcal,
            NutrientType.Protein => Unit.Gram,
            NutrientType.Fat => Unit.Gram,
            NutrientType.Carbs => Unit.Gram,
            NutrientType.Fibre => Unit.Gram,
            NutrientType.Sugar => Unit.Gram,
            NutrientType.SaturatedFat => Unit.Gram,
            NutrientType.MonounsaturatedFat => Unit.Gram,
            NutrientType.PolyunsaturatedFat => Unit.Gram,
            NutrientType.TransFat => Unit.Gram,
            NutrientType.VitaminA => Unit.Microgram,
            NutrientType.VitaminB6 => Unit.Milligram,
            NutrientType.VitaminB12 => Unit.Microgram,
            NutrientType.VitaminC => Unit.Milligram,
            NutrientType.VitaminD => Unit.Microgram,
            NutrientType.VitaminE => Unit.Milligram,
            NutrientType.Thiamin => Unit.Milligram,
            NutrientType.Riboflavin => Unit.Milligram,
            NutrientType.Niacin => Unit.Milligram,
            NutrientType.Folate => Unit.Microgram,
            NutrientType.Salt => Unit.Gram,
            NutrientType.Potassium => Unit.Milligram,
            NutrientType.Magnesium => Unit.Milligram,
            NutrientType.Iron => Unit.Milligram,
            NutrientType.Zinc => Unit.Milligram,
            NutrientType.Phosphorus => Unit.Milligram,
            NutrientType.Copper => Unit.Milligram,
            NutrientType.Iodine => Unit.Microgram,
            NutrientType.Selenium => Unit.Microgram,
            NutrientType.Calcium => Unit.Milligram,
            _ => throw new NutrientUnitNotDefinedException(nutrientType)
        };
    
    }
    public static string GetDescription(this NutrientType nutrientType)
        => nutrientType switch {
            NutrientType.Energy => "Energy",
            NutrientType.Protein => "Protein",
            NutrientType.Fat => "Fat",
            NutrientType.Carbs => "Carbs",
            NutrientType.Fibre => "Fibre",
            NutrientType.Sugar => "Sugar",
            NutrientType.SaturatedFat => "Saturated Fat",
            NutrientType.MonounsaturatedFat => "Monounsaturated Fat",
            NutrientType.PolyunsaturatedFat => "Polyunsaturated Fat",
            NutrientType.TransFat => "Trans Fat",
            NutrientType.VitaminA => "Vitamin A",
            NutrientType.VitaminB6 => "Vitamin B6",
            NutrientType.VitaminB12 => "Vitamin B12",
            NutrientType.VitaminC => "Vitamin C",
            NutrientType.VitaminD => "Vitamin D",
            NutrientType.VitaminE => "Vitamin E",
            NutrientType.Thiamin => "Thiamin",
            NutrientType.Riboflavin => "Riboflavin",
            NutrientType.Niacin => "Niacin",
            NutrientType.Folate => "Folate",
            NutrientType.Salt => "Salt",
            NutrientType.Potassium => "Potassium",
            NutrientType.Magnesium => "Magnesium",
            NutrientType.Iron => "Iron",
            NutrientType.Zinc => "Zinc",
            NutrientType.Phosphorus => "Phosphorus",
            NutrientType.Copper => "Copper",
            NutrientType.Iodine => "Iodine",
            NutrientType.Selenium => "Selenium",
            NutrientType.Calcium => "Calcium",
            _ => throw new StringReprentationForNutrientTypeNotDefinedException(nutrientType.ToString())
        };

    public static NutrientType ToNutrientType(this string nutrientType)
        => nutrientType switch {
            "Energy" => NutrientType.Energy,
            "Protein" => NutrientType.Protein,
            "Fat" => NutrientType.Fat,
            "Carbs" => NutrientType.Carbs,
            "Fibre" => NutrientType.Fibre,
            "Sugar" => NutrientType.Sugar,
            "Saturated Fat" => NutrientType.SaturatedFat,
            "Monounsaturated Fat" => NutrientType.MonounsaturatedFat,
            "Polyunsaturated Fat" => NutrientType.PolyunsaturatedFat,
            "Trans Fat" => NutrientType.TransFat,
            "Vitamin A" => NutrientType.VitaminA,
            "Vitamin B6" => NutrientType.VitaminB6,
            "Vitamin B12" => NutrientType.VitaminB12,
            "Vitamin C" => NutrientType.VitaminC,
            "Vitamin D" => NutrientType.VitaminD,
            "Vitamin E" => NutrientType.VitaminE,
            "Thiamin" => NutrientType.Thiamin,
            "Riboflavin" => NutrientType.Riboflavin,
            "Niacin" => NutrientType.Niacin,
            "Folate" => NutrientType.Folate,
            "Salt" => NutrientType.Salt,
            "Potassium" => NutrientType.Potassium,
            "Magnesium" => NutrientType.Magnesium,
            "Iron" => NutrientType.Iron,
            "Zinc" => NutrientType.Zinc,
            "Phosphorus" => NutrientType.Phosphorus,
            "Copper" => NutrientType.Copper,
            "Iodine" => NutrientType.Iodine,
            "Selenium" => NutrientType.Selenium,
            "Calcium" => NutrientType.Calcium,
            _ => throw new NutrientTypeNotDefinedException(nutrientType)
        };

}

public class NutrientUnitNotDefinedException(NutrientType nutrientType) : 
    Exception($"Unit not defined for nutrient type: {nutrientType.GetUnit()}")
{}

public class NutrientTypeNotDefinedException(string nutrientType) : 
    Exception($"Nutrient type not defined: {nutrientType}")
{}

public class StringReprentationForNutrientTypeNotDefinedException(string nutrientType) : 
    Exception($"String representation not defined for nutrient type: {nutrientType}")
{}