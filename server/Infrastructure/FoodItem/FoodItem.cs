namespace server.Infrastructure;
using server.Core.EF;

public class FoodItem 
{
    public int Id {get; set;}
    public string Name {get; set;}
    public float Calories {get; set;} //kcal/100g
    public float Protein {get; set;} //g/100g

    //Carbohydrates
    public float Carbohydrates {get; set;}
    public float Sugars {get; set;}
    public float Fibres {get; set;}

    //Fats
    public float TotalFat {get; set;}
    public float SaturatedFat {get; set;}
    public float MonounsaturatedFat {get; set;}
    public float PolyunsaturatedFat {get; set;}
    public float TransFat {get; set;}

    //Vitamins
    public float VitaminA {get; set;}
    public float VitaminB6 {get; set;}
    public float VitaminB12 {get; set;}
    public float VitaminC {get; set;}
    public float VitaminD {get; set;}
    public float VitaminE {get; set;}
    public float Thiamin {get; set;}
    public float Riboflavin {get; set;}
    public float Niacin {get; set;}
    public float Folate {get; set;}

    //Minerals
    public float Salt {get; set;}
    public float Potassium {get; set;}
    public float Magnesium {get; set;}
    public float Iron {get; set;}
    public float Zinc {get; set;}
    public float Phosphorus {get; set;}
    public float Copper {get; set;}
    public float Iodine {get; set;}
    public float Selenium {get; set;}
    public float Calcium {get; set;}


    public FoodItem(
        string name,
        float calories,
        float protein,
        float carbohydrates,
        float sugars,
        float fibres,
        float totalfat,
        float saturatedfat,
        float monounsaturatedfat,
        float polyunsaturatedfat,
        float transfat,
        float vitaminA,
        float vitaminB6,
        float vitaminB12,
        float vitaminC,
        float vitaminD,
        float vitaminE,
        float thiamin,
        float riboflavin,
        float niacin,
        float folate,
        float salt,
        float potassium,
        float magnesium,
        float iron,
        float zinc,
        float phosphorus,
        float copper,
        float iodine,
        float selenium,
        float calcium

    )
    {
        this.Name = name;
        this.Calories = calories;
        this.Protein = protein;
        this.Carbohydrates = carbohydrates;
        this.Sugars = sugars;
        this.Fibres = fibres;
        this.TotalFat = totalfat;
        this.SaturatedFat = saturatedfat;
        this.MonounsaturatedFat = monounsaturatedfat;
        this.PolyunsaturatedFat = polyunsaturatedfat;
        this.TransFat = transfat;
        this.VitaminA = vitaminA;
        this.VitaminB6 = vitaminB6;
        this.VitaminB12 = vitaminB12;
        this.VitaminC = vitaminC;
        this.VitaminD = vitaminD;
        this.VitaminE = vitaminE;
        this.Thiamin = thiamin;
        this.Riboflavin = riboflavin;
        this.Niacin = niacin;
        this.Folate = folate;
        this.Salt = salt;
        this.Potassium = potassium;
        this.Magnesium = magnesium;
        this.Iron = iron;
        this.Zinc = zinc;
        this.Phosphorus = phosphorus;
        this.Copper = copper;
        this.Iodine = iodine;
        this.Selenium = selenium;
        this.Calcium = calcium;
    }

    #nullable disable
    public FoodItem() {}

    public FoodItemDTO ToDTO()
            => new FoodItemDTO(
                this.Id,
                this.Name,
                this.Calories,
                this.Protein,
                this.Carbohydrates,
                this.Sugars,
                this.Fibres,
                this.TotalFat,
                this.SaturatedFat,
                this.MonounsaturatedFat,
                this.PolyunsaturatedFat,
                this.TransFat,
                this.VitaminA,
                this.VitaminB6,
                this.VitaminB12,
                this.VitaminC,
                this.VitaminD,
                this.VitaminE,
                this.Thiamin,
                this.Riboflavin,
                this.Niacin,
                this.Folate,
                this.Salt,
                this.Potassium,
                this.Magnesium,
                this.Iron,
                this.Zinc,
                this.Phosphorus,
                this.Copper,
                this.Iodine,
                this.Selenium,
                this.Calcium
            );
}