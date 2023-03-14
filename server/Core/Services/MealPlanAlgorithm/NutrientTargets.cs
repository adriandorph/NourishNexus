namespace server.Core.Services.MealPlan;

public class NutrientTargets
{
    public float Protein {get; set;} = 0f;

    //Carbohydrates
    public float Carbohydrates {get; set;} = 0f;
    public float Sugars {get; set;} = 0f;
    public float Fibres {get; set;} = 0f;

    //Fats
    public float TotalFat {get; set;} = 0f;
    public float SaturatedFat {get; set;} = 0f;
    public float MonounsaturatedFat {get; set;} = 0f;
    public float PolyunsaturatedFat {get; set;} = 0f;
    public float TransFat {get; set;} = 0f;

    //Vitamins
    public float VitaminA {get; set;} = 0f;
    public float VitaminB6 {get; set;} = 0f;
    public float VitaminB12 {get; set;} = 0f;
    public float VitaminC {get; set;} = 0f;
    public float VitaminD {get; set;} = 0f;
    public float VitaminE {get; set;} = 0f;
    public float VitaminK1 {get; set;} = 0f;
    public float Thiamin {get; set;} = 0f;
    public float Riboflavin {get; set;} = 0f;
    public float Niacin {get; set;} = 0f;
    public float Folate {get; set;} = 0f;

    //Minerals
    public float Salt {get; set;} = 0f;
    public float Potassium {get; set;} = 0f;
    public float Magnesium {get; set;} = 0f;
    public float Iron {get; set;} = 0f;
    public float Zinc {get; set;} = 0f;
    public float Phosphorus {get; set;} = 0f;
    public float Copper {get; set;} = 0f;
    public float Iodine {get; set;} = 0f;
    public float Nickel {get; set;} = 0f;
    public float Selenium {get; set;} = 0f;
    public float Calcium {get; set;} = 0f;

    public static NutrientTargets operator + (NutrientTargets a, NutrientTargets b)
        => new NutrientTargets
        {
            Protein = a.Protein + b.Protein,
            Carbohydrates = a.Carbohydrates + b.Carbohydrates,
            Sugars = a.Sugars + b.Sugars,
            Fibres = a.Fibres + b.Fibres,
            TotalFat = a.TotalFat + b.TotalFat,
            SaturatedFat = a.SaturatedFat + b.SaturatedFat,
            MonounsaturatedFat = a.MonounsaturatedFat + b.MonounsaturatedFat,
            PolyunsaturatedFat = a.PolyunsaturatedFat + b.PolyunsaturatedFat,
            TransFat = a.TransFat + b.TransFat,
            VitaminA = a.VitaminA + b.VitaminA,
            VitaminB6 = a.VitaminB6 + b.VitaminB6,
            VitaminB12 = a.VitaminB12 + b.VitaminB12,
            VitaminC = a.VitaminC + b.VitaminC,
            VitaminD = a.VitaminD + b.VitaminD,
            VitaminE = a.VitaminE + b.VitaminE,
            VitaminK1 = a.VitaminK1 + b.VitaminK1,
            Thiamin = a.Thiamin + b.Thiamin,
            Riboflavin = a.Riboflavin + b.Riboflavin,
            Niacin = a.Niacin + b.Niacin,
            Folate = a.Folate + b.Folate,
            Salt = a.Salt + b.Salt,
            Potassium = a.Potassium + b.Potassium,
            Magnesium = a.Magnesium + b.Magnesium,
            Iron = a.Iron + b.Iron,
            Zinc = a.Zinc + b.Zinc,
            Phosphorus = a.Phosphorus + b.Phosphorus,
            Copper = a.Copper + b.Copper,
            Iodine = a.Iodine + b.Iodine,
            Nickel = a.Nickel + b.Nickel,
            Selenium = a.Selenium + b.Selenium,
            Calcium = a.Calcium + b.Calcium
        };
    
    public static NutrientTargets operator /(NutrientTargets a, NutrientTargets b)
        => new NutrientTargets
        {
            Protein = a.Protein / b.Protein,
            Carbohydrates = a.Carbohydrates / b.Carbohydrates,
            Sugars = a.Sugars / b.Sugars,
            Fibres = a.Fibres / b.Fibres,
            TotalFat = a.TotalFat / b.TotalFat,
            SaturatedFat = a.SaturatedFat / b.SaturatedFat,
            MonounsaturatedFat = a.MonounsaturatedFat / b.MonounsaturatedFat,
            PolyunsaturatedFat = a.PolyunsaturatedFat / b.PolyunsaturatedFat,
            TransFat = a.TransFat / b.TransFat,
            VitaminA = a.VitaminA / b.VitaminA,
            VitaminB6 = a.VitaminB6 / b.VitaminB6,
            VitaminB12 = a.VitaminB12 / b.VitaminB12,
            VitaminC = a.VitaminC / b.VitaminC,
            VitaminD = a.VitaminD / b.VitaminD,
            VitaminE = a.VitaminE / b.VitaminE,
            VitaminK1 = a.VitaminK1 / b.VitaminK1,
            Thiamin = a.Thiamin / b.Thiamin,
            Riboflavin = a.Riboflavin / b.Riboflavin,
            Niacin = a.Niacin / b.Niacin,
            Folate = a.Folate / b.Folate,
            Salt = a.Salt / b.Salt,
            Potassium = a.Potassium / b.Potassium,
            Magnesium = a.Magnesium / b.Magnesium,
            Iron = a.Iron / b.Iron,
            Zinc = a.Zinc / b.Zinc,
            Phosphorus = a.Phosphorus / b.Phosphorus,
            Copper = a.Copper / b.Copper,
            Iodine = a.Iodine / b.Iodine,
            Nickel = a.Nickel / b.Nickel,
            Selenium = a.Selenium / b.Selenium,
            Calcium = a.Calcium / b.Calcium
        };

    public static NutrientTargets operator *(NutrientTargets a, NutrientTargets b)
        => new NutrientTargets
        {
            Protein = a.Protein * b.Protein,
            Carbohydrates = a.Carbohydrates * b.Carbohydrates,
            Sugars = a.Sugars * b.Sugars,
            Fibres = a.Fibres * b.Fibres,
            TotalFat = a.TotalFat * b.TotalFat,
            SaturatedFat = a.SaturatedFat * b.SaturatedFat,
            MonounsaturatedFat = a.MonounsaturatedFat * b.MonounsaturatedFat,
            PolyunsaturatedFat = a.PolyunsaturatedFat * b.PolyunsaturatedFat,
            TransFat = a.TransFat * b.TransFat,
            VitaminA = a.VitaminA * b.VitaminA,
            VitaminB6 = a.VitaminB6 * b.VitaminB6,
            VitaminB12 = a.VitaminB12 * b.VitaminB12,
            VitaminC = a.VitaminC * b.VitaminC,
            VitaminD = a.VitaminD * b.VitaminD,
            VitaminE = a.VitaminE * b.VitaminE,
            VitaminK1 = a.VitaminK1 * b.VitaminK1,
            Thiamin = a.Thiamin * b.Thiamin,
            Riboflavin = a.Riboflavin * b.Riboflavin,
            Niacin = a.Niacin * b.Niacin,
            Folate = a.Folate * b.Folate,
            Salt = a.Salt * b.Salt,
            Potassium = a.Potassium * b.Potassium,
            Magnesium = a.Magnesium * b.Magnesium,
            Iron = a.Iron * b.Iron,
            Zinc = a.Zinc * b.Zinc,
            Phosphorus = a.Phosphorus * b.Phosphorus,
            Copper = a.Copper * b.Copper,
            Iodine = a.Iodine * b.Iodine,
            Nickel = a.Nickel * b.Nickel,
            Selenium = a.Selenium * b.Selenium,
            Calcium = a.Calcium * b.Calcium,
        };
    
    public static NutrientTargets operator *(NutrientTargets a, float scale)
        => new NutrientTargets
        {
            Protein = a.Protein * scale,
            Carbohydrates = a.Carbohydrates * scale,
            Sugars = a.Sugars * scale,
            Fibres = a.Fibres * scale,
            TotalFat = a.TotalFat * scale,
            SaturatedFat = a.SaturatedFat * scale,
            MonounsaturatedFat = a.MonounsaturatedFat * scale,
            PolyunsaturatedFat = a.PolyunsaturatedFat * scale,
            TransFat = a.TransFat * scale,
            VitaminA = a.VitaminA * scale,
            VitaminB6 = a.VitaminB6 * scale,
            VitaminB12 = a.VitaminB12 * scale,
            VitaminC = a.VitaminC * scale,
            VitaminD = a.VitaminD * scale,
            VitaminE = a.VitaminE * scale,
            VitaminK1 = a.VitaminK1 * scale,
            Thiamin = a.Thiamin * scale,
            Riboflavin = a.Riboflavin * scale,
            Niacin = a.Niacin * scale,
            Folate = a.Folate * scale,
            Salt = a.Salt * scale,
            Potassium = a.Potassium * scale,
            Magnesium = a.Magnesium * scale,
            Iron = a.Iron * scale,
            Zinc = a.Zinc * scale,
            Phosphorus = a.Phosphorus * scale,
            Copper = a.Copper * scale,
            Iodine = a.Iodine * scale,
            Nickel = a.Nickel * scale,
            Selenium = a.Selenium * scale,
            Calcium = a.Calcium * scale
        };
    
    //If all nutrient values in a are greater than or equal the coresponding values in b
    public static bool operator >=(NutrientTargets a, NutrientTargets b)
    {
        if(a.Protein < b.Protein) return false;
        if(a.Carbohydrates < b.Carbohydrates) return false;
        if(a.Sugars < b.Sugars) return false;
        if(a.Fibres < b.Fibres) return false;
        if(a.TotalFat < b.TotalFat) return false;
        if(a.SaturatedFat < b.SaturatedFat) return false;
        if(a.MonounsaturatedFat < b.MonounsaturatedFat) return false;
        if(a.PolyunsaturatedFat < b.PolyunsaturatedFat) return false;
        if(a.TransFat < b.TransFat) return false;
        if(a.VitaminA < b.VitaminA) return false;
        if(a.VitaminB6 < b.VitaminB6) return false;
        if(a.VitaminB12 < b.VitaminB12) return false;
        if(a.VitaminC < b.VitaminC) return false;
        if(a.VitaminD < b.VitaminD) return false;
        if(a.VitaminE < b.VitaminE) return false;
        if(a.VitaminK1 < b.VitaminK1) return false;
        if(a.Thiamin < b.Thiamin) return false;
        if(a.Riboflavin < b.Riboflavin) return false;
        if(a.Niacin < b.Niacin) return false;
        if(a.Folate < b.Folate) return false;
        if(a.Salt < b.Salt) return false;
        if(a.Potassium < b.Potassium) return false;
        if(a.Magnesium < b.Magnesium) return false;
        if(a.Iron < b.Iron) return false;
        if(a.Zinc < b.Zinc) return false;
        if(a.Phosphorus < b.Phosphorus) return false;
        if(a.Copper < b.Copper) return false;
        if(a.Iodine < b.Iodine) return false;
        if(a.Nickel < b.Nickel) return false;
        if(a.Selenium < b.Selenium) return false;
        if(a.Calcium < b.Calcium) return false;
        return true;
    }

    //If all nutrient values in a are less than or equal the coresponding values in b
    public static bool operator <=(NutrientTargets a, NutrientTargets b)
    {
        if(a.Protein > b.Protein) return false;
        if(a.Carbohydrates > b.Carbohydrates) return false;
        if(a.Sugars > b.Sugars) return false;
        if(a.Fibres > b.Fibres) return false;
        if(a.TotalFat > b.TotalFat) return false;
        if(a.SaturatedFat > b.SaturatedFat) return false;
        if(a.MonounsaturatedFat > b.MonounsaturatedFat) return false;
        if(a.PolyunsaturatedFat > b.PolyunsaturatedFat) return false;
        if(a.TransFat > b.TransFat) return false;
        if(a.VitaminA > b.VitaminA) return false;
        if(a.VitaminB6 > b.VitaminB6) return false;
        if(a.VitaminB12 > b.VitaminB12) return false;
        if(a.VitaminC > b.VitaminC) return false;
        if(a.VitaminD > b.VitaminD) return false;
        if(a.VitaminE > b.VitaminE) return false;
        if(a.VitaminK1 > b.VitaminK1) return false;
        if(a.Thiamin > b.Thiamin) return false;
        if(a.Riboflavin > b.Riboflavin) return false;
        if(a.Niacin > b.Niacin) return false;
        if(a.Folate > b.Folate) return false;
        if(a.Salt > b.Salt) return false;
        if(a.Potassium > b.Potassium) return false;
        if(a.Magnesium > b.Magnesium) return false;
        if(a.Iron > b.Iron) return false;
        if(a.Zinc > b.Zinc) return false;
        if(a.Phosphorus > b.Phosphorus) return false;
        if(a.Copper > b.Copper) return false;
        if(a.Iodine > b.Iodine) return false;
        if(a.Nickel > b.Nickel) return false;
        if(a.Selenium > b.Selenium) return false;
        if(a.Calcium > b.Calcium) return false;
        return true;
    }

    //All values in a are greater than the coresponding values in b.
    public static bool operator >(NutrientTargets a, NutrientTargets b)
    {
        if(a.Protein <= b.Protein) return false;
        if(a.Carbohydrates <= b.Carbohydrates) return false;
        if(a.Sugars <= b.Sugars) return false;
        if(a.Fibres <= b.Fibres) return false;
        if(a.TotalFat <= b.TotalFat) return false;
        if(a.SaturatedFat <= b.SaturatedFat) return false;
        if(a.MonounsaturatedFat <= b.MonounsaturatedFat) return false;
        if(a.PolyunsaturatedFat <= b.PolyunsaturatedFat) return false;
        if(a.TransFat <= b.TransFat) return false;
        if(a.VitaminA <= b.VitaminA) return false;
        if(a.VitaminB6 <= b.VitaminB6) return false;
        if(a.VitaminB12 <= b.VitaminB12) return false;
        if(a.VitaminC <= b.VitaminC) return false;
        if(a.VitaminD <= b.VitaminD) return false;
        if(a.VitaminE <= b.VitaminE) return false;
        if(a.VitaminK1 <= b.VitaminK1) return false;
        if(a.Thiamin <= b.Thiamin) return false;
        if(a.Riboflavin <= b.Riboflavin) return false;
        if(a.Niacin <= b.Niacin) return false;
        if(a.Folate <= b.Folate) return false;
        if(a.Salt <= b.Salt) return false;
        if(a.Potassium <= b.Potassium) return false;
        if(a.Magnesium <= b.Magnesium) return false;
        if(a.Iron <= b.Iron) return false;
        if(a.Zinc <= b.Zinc) return false;
        if(a.Phosphorus <= b.Phosphorus) return false;
        if(a.Copper <= b.Copper) return false;
        if(a.Iodine <= b.Iodine) return false;
        if(a.Nickel <= b.Nickel) return false;
        if(a.Selenium <= b.Selenium) return false;
        if(a.Calcium <= b.Calcium) return false;
        return true;
    }

    public static bool operator <(NutrientTargets a, NutrientTargets b)
    {
        if(a.Protein >= b.Protein) return false;
        if(a.Carbohydrates >= b.Carbohydrates) return false;
        if(a.Sugars >= b.Sugars) return false;
        if(a.Fibres >= b.Fibres) return false;
        if(a.TotalFat >= b.TotalFat) return false;
        if(a.SaturatedFat >= b.SaturatedFat) return false;
        if(a.MonounsaturatedFat >= b.MonounsaturatedFat) return false;
        if(a.PolyunsaturatedFat >= b.PolyunsaturatedFat) return false;
        if(a.TransFat >= b.TransFat) return false;
        if(a.VitaminA >= b.VitaminA) return false;
        if(a.VitaminB6 >= b.VitaminB6) return false;
        if(a.VitaminB12 >= b.VitaminB12) return false;
        if(a.VitaminC >= b.VitaminC) return false;
        if(a.VitaminD >= b.VitaminD) return false;
        if(a.VitaminE >= b.VitaminE) return false;
        if(a.VitaminK1 >= b.VitaminK1) return false;
        if(a.Thiamin >= b.Thiamin) return false;
        if(a.Riboflavin >= b.Riboflavin) return false;
        if(a.Niacin >= b.Niacin) return false;
        if(a.Folate >= b.Folate) return false;
        if(a.Salt >= b.Salt) return false;
        if(a.Potassium >= b.Potassium) return false;
        if(a.Magnesium >= b.Magnesium) return false;
        if(a.Iron >= b.Iron) return false;
        if(a.Zinc >= b.Zinc) return false;
        if(a.Phosphorus >= b.Phosphorus) return false;
        if(a.Copper >= b.Copper) return false;
        if(a.Iodine >= b.Iodine) return false;
        if(a.Nickel >= b.Nickel) return false;
        if(a.Selenium >= b.Selenium) return false;
        if(a.Calcium >= b.Calcium) return false;
        return true;
    }

    //Number of how many values in this that are lower than the coresponding values in b.
    public int lowerCount(NutrientTargets b)
    {
        int count = 0;
        if(this.Protein < b.Protein) count++;
        if(this.Carbohydrates < b.Carbohydrates) count++;
        if(this.Sugars < b.Sugars) count++;
        if(this.Fibres < b.Fibres) count++;
        if(this.TotalFat < b.TotalFat) count++;
        if(this.SaturatedFat < b.SaturatedFat) count++;
        if(this.MonounsaturatedFat < b.MonounsaturatedFat) count++;
        if(this.PolyunsaturatedFat < b.PolyunsaturatedFat) count++;
        if(this.TransFat < b.TransFat) count++;
        if(this.VitaminA < b.VitaminA) count++;
        if(this.VitaminB6 < b.VitaminB6) count++;
        if(this.VitaminB12 < b.VitaminB12) count++;
        if(this.VitaminC < b.VitaminC) count++;
        if(this.VitaminD < b.VitaminD) count++;
        if(this.VitaminE < b.VitaminE) count++;
        if(this.VitaminK1 < b.VitaminK1) count++;
        if(this.Thiamin < b.Thiamin) count++;
        if(this.Riboflavin < b.Riboflavin) count++;
        if(this.Niacin < b.Niacin) count++;
        if(this.Folate < b.Folate) count++;
        if(this.Salt < b.Salt) count++;
        if(this.Potassium < b.Potassium) count++;
        if(this.Magnesium < b.Magnesium) count++;
        if(this.Iron < b.Iron) count++;
        if(this.Zinc < b.Zinc) count++;
        if(this.Phosphorus < b.Phosphorus) count++;
        if(this.Copper < b.Copper) count++;
        if(this.Iodine < b.Iodine) count++;
        if(this.Nickel < b.Nickel) count++;
        if(this.Selenium < b.Selenium) count++;
        if(this.Calcium < b.Calcium) count++;
        return count;
    }

    public int HigherCount(NutrientTargets b)
    {
        int count = 0;
        if(this.Protein > b.Protein) count++;
        if(this.Carbohydrates > b.Carbohydrates) count++;
        if(this.Sugars > b.Sugars) count++;
        if(this.Fibres > b.Fibres) count++;
        if(this.TotalFat > b.TotalFat) count++;
        if(this.SaturatedFat > b.SaturatedFat) count++;
        if(this.MonounsaturatedFat > b.MonounsaturatedFat) count++;
        if(this.PolyunsaturatedFat > b.PolyunsaturatedFat) count++;
        if(this.TransFat > b.TransFat) count++;
        if(this.VitaminA > b.VitaminA) count++;
        if(this.VitaminB6 > b.VitaminB6) count++;
        if(this.VitaminB12 > b.VitaminB12) count++;
        if(this.VitaminC > b.VitaminC) count++;
        if(this.VitaminD > b.VitaminD) count++;
        if(this.VitaminE > b.VitaminE) count++;
        if(this.VitaminK1 > b.VitaminK1) count++;
        if(this.Thiamin > b.Thiamin) count++;
        if(this.Riboflavin > b.Riboflavin) count++;
        if(this.Niacin > b.Niacin) count++;
        if(this.Folate > b.Folate) count++;
        if(this.Salt > b.Salt) count++;
        if(this.Potassium > b.Potassium) count++;
        if(this.Magnesium > b.Magnesium) count++;
        if(this.Iron > b.Iron) count++;
        if(this.Zinc > b.Zinc) count++;
        if(this.Phosphorus > b.Phosphorus) count++;
        if(this.Copper > b.Copper) count++;
        if(this.Iodine > b.Iodine) count++;
        if(this.Nickel > b.Nickel) count++;
        if(this.Selenium > b.Selenium) count++;
        if(this.Calcium > b.Calcium) count++;
        return count;
    }


}