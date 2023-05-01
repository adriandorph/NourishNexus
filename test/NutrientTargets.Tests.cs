using server.Services;
using server.Services.MealPlan;

namespace test;

public class NutrientTargetsTests 
{
    private readonly NutrientTargets t1 = new NutrientTargets
    {
        Protein = 1,
        Carbohydrates = 1,
        Sugars = 1,
        Fibres = 1,
        TotalFat = 1,
        SaturatedFat = 1,
        MonounsaturatedFat = 1,
        PolyunsaturatedFat = 1,
        TransFat = 1,
        VitaminA = 1,
        VitaminB6 = 1,
        VitaminB12 = 1,
        VitaminC = 1,
        VitaminD = 1,
        VitaminE = 1,
        Thiamin = 1,
        Riboflavin = 1,
        Niacin = 1,
        Folate = 1,
        Salt = 1,
        Potassium = 1,
        Magnesium = 1,
        Iron = 1,
        Zinc = 1,
        Phosphorus = 1,
        Copper = 1,
        Iodine = 1,
        Selenium = 1,
        Calcium = 1
    };

    private readonly NutrientTargets t2 = new NutrientTargets
    {
        Protein = 2,
        Carbohydrates = 2,
        Sugars = 2,
        Fibres = 2,
        TotalFat = 2,
        SaturatedFat = 2,
        MonounsaturatedFat = 2,
        PolyunsaturatedFat = 2,
        TransFat = 2,
        VitaminA = 2,
        VitaminB6 = 2,
        VitaminB12 = 2,
        VitaminC = 2,
        VitaminD = 2,
        VitaminE = 2,
        Thiamin = 2,
        Riboflavin = 2,
        Niacin = 2,
        Folate = 2,
        Salt = 2,
        Potassium = 2,
        Magnesium = 2,
        Iron = 2,
        Zinc = 2,
        Phosphorus = 2,
        Copper = 2,
        Iodine = 2,
        Selenium = 2,
        Calcium = 2,
    };

    private readonly NutrientTargets t4 = new NutrientTargets
    {
        Protein = 4,
        Carbohydrates = 4,
        Sugars = 4,
        Fibres = 4,
        TotalFat = 4,
        SaturatedFat = 4,
        MonounsaturatedFat = 4,
        PolyunsaturatedFat = 4,
        TransFat = 4,
        VitaminA = 4,
        VitaminB6 = 4,
        VitaminB12 = 4,
        VitaminC = 4,
        VitaminD = 4,
        VitaminE = 4,
        Thiamin = 4,
        Riboflavin = 4,
        Niacin = 4,
        Folate = 4,
        Salt = 4,
        Potassium = 4,
        Magnesium = 4,
        Iron = 4,
        Zinc = 4,
        Phosphorus = 4,
        Copper = 4,
        Iodine = 4,
        Selenium = 4,
        Calcium = 4
    };

    private readonly NutrientTargets t0to28 = new NutrientTargets
    {
        Protein = 0,
        Carbohydrates = 1,
        Sugars = 2,
        Fibres = 3,
        TotalFat = 4,
        SaturatedFat = 5,
        MonounsaturatedFat = 6,
        PolyunsaturatedFat = 7,
        TransFat = 8,
        VitaminA = 9,
        VitaminB6 = 10,
        VitaminB12 = 11,
        VitaminC = 12,
        VitaminD = 13,
        VitaminE = 14,
        Thiamin = 15,
        Riboflavin = 16,
        Niacin = 17,
        Folate = 18,
        Salt = 19,
        Potassium = 20,
        Magnesium = 21,
        Iron = 22,
        Zinc = 23,
        Phosphorus = 24,
        Copper = 25,
        Iodine = 26,
        Selenium = 27,
        Calcium = 28,
    };


    void AssertAllNutrientTargets(NutrientTargets expected, NutrientTargets actual)
    {
        //Assert
        Assert.Equal(expected.Protein, actual.Protein);
        Assert.Equal(expected.Carbohydrates, actual.Carbohydrates);
        Assert.Equal(expected.Sugars, actual.Sugars);
        Assert.Equal(expected.Fibres, actual.Fibres);
        Assert.Equal(expected.TotalFat, actual.TotalFat);
        Assert.Equal(expected.SaturatedFat, actual.SaturatedFat);
        Assert.Equal(expected.MonounsaturatedFat, actual.MonounsaturatedFat);
        Assert.Equal(expected.PolyunsaturatedFat, actual.PolyunsaturatedFat);
        Assert.Equal(expected.TransFat, actual.TransFat);
        Assert.Equal(expected.VitaminA, actual.VitaminA);
        Assert.Equal(expected.VitaminB6, actual.VitaminB6);
        Assert.Equal(expected.VitaminB12, actual.VitaminB12);
        Assert.Equal(expected.VitaminC, actual.VitaminC);
        Assert.Equal(expected.VitaminD, actual.VitaminD);
        Assert.Equal(expected.VitaminE, actual.VitaminE);
        Assert.Equal(expected.Thiamin, actual.Thiamin);
        Assert.Equal(expected.Riboflavin, actual.Riboflavin);
        Assert.Equal(expected.Niacin, actual.Niacin);
        Assert.Equal(expected.Folate, actual.Folate);
        Assert.Equal(expected.Salt, actual.Salt);
        Assert.Equal(expected.Potassium, actual.Potassium);
        Assert.Equal(expected.Magnesium, actual.Magnesium);
        Assert.Equal(expected.Iron, actual.Iron);
        Assert.Equal(expected.Zinc, actual.Zinc);
        Assert.Equal(expected.Phosphorus, actual.Phosphorus);
        Assert.Equal(expected.Copper, actual.Copper);
        Assert.Equal(expected.Iodine, actual.Iodine);
        Assert.Equal(expected.Selenium, actual.Selenium);
        Assert.Equal(expected.Calcium, actual.Calcium);
    }

    [Fact]
    void ToNutrientTargets()
    {
        //Arrange
        var expected = t0to28 * 0.5f;
        var fi = new FoodItemAmountDTO
        {
            Amount = 0.5f,
            FoodItem = new FoodItemDTO(
                0,
                "Food",
                100,
                0,
                1,
                2,
                3,
                4,
                5,
                6,
                7,
                8,
                9,
                10,
                11,
                12,
                13,
                14,
                15,
                16,
                17,
                18,
                19,
                20,
                21,
                22,
                23,
                24,
                25,
                26,
                27,
                28
            )
        };

        //Act
        var actual = NutrientTargets.ToNutrientTargets(fi);

        //Assert
        AssertAllNutrientTargets(expected, actual);
    }

    [Fact]
    void DivisionOperator()
    {
        //Arrange
        var expected = t1;

        //Act
        var actual = t2 / t2;

        //Assert
        AssertAllNutrientTargets(expected, actual);
    }

    [Fact]
    void MultiplicationOperator()
    {
        //Arrange
        var expected = t4;

        //Act
        var actual = t2 * t2;

        //Assert
        AssertAllNutrientTargets(expected, actual);
    }

    [Fact]
    void LessThanOperator()
    {
        #pragma warning disable CS1718
        //Act
        var actualLess = t1 < t2;
        var actualEqual = t2 < t2;
        var actualMore = t2 < t1;

        //Assert
        Assert.True(actualLess);
        Assert.False(actualEqual);
        Assert.False(actualMore);
    }

    [Fact]
    void LowerCount()
    {
        //Arrange
        var expected29 = 29;
        var expected0 = 0;

        //Act
        var actual29 = t1.lowerCount(t2);
        var actual0 = t2.lowerCount(t1);

        //Assert
        Assert.Equal(expected29, actual29);
        Assert.Equal(expected0, actual0);
    }

    [Fact]
    void HigherCount()
    {
        //Arrange
        var expected29 = 29;
        var expected0 = 0;

        //Act
        var actual29 = t2.HigherCount(t1);
        var actual0 = t1.HigherCount(t2);

        //Assert
        Assert.Equal(expected29, actual29);
        Assert.Equal(expected0, actual0);
    }

}