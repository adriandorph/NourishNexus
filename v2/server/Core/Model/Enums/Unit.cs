namespace server.Core.Model.Enums;
public enum Unit {
    Gram,
    Kilogram,
    Milligram,
    Microgram,
    Nanogram,
    Liter,
    DeciLiter,
    CentiLiter,
    Milliliter,
    Kcal,
}

public static class UnitExtensions
{
    public static string GetDescription(this Unit unit)
    {
        return unit switch
        {
            Unit.Gram => "g",
            Unit.Kilogram => "kg",
            Unit.Milligram => "mg",
            Unit.Microgram => "µg",
            Unit.Nanogram => "ng",
            Unit.Liter => "l",
            Unit.DeciLiter => "dl",
            Unit.CentiLiter => "cl",
            Unit.Milliliter => "ml",
            Unit.Kcal => "kcal",
            _ => throw new StringReprentationForUnitNotDefinedException(unit)
        };
    }

    public static Unit ToUnit(this string unit)
    {
        return unit switch
        {
            "g" => Unit.Gram,
            "kg" => Unit.Kilogram,
            "mg" => Unit.Milligram,
            "µg" => Unit.Microgram,
            "ng" => Unit.Nanogram,
            "l" => Unit.Liter,
            "dl" => Unit.DeciLiter,
            "cl" => Unit.CentiLiter,
            "ml" => Unit.Milliliter,
            "kcal" => Unit.Kcal,
            _ => throw new UnitNotDefinedException(unit)
        };
    }
}

public class StringReprentationForUnitNotDefinedException(Unit unit) 
: Exception($"String representation not defined for unit: {unit}") 
{}

public class UnitNotDefinedException(string unit) : Exception($"Unit not defined: {unit}") 
{}