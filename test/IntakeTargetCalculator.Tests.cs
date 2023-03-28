using server.Services.MealPlan;

namespace test;

public class IntakeTargetCalculatorTests
{
    [Theory]
    [InlineData(25, Gender.Male, 70, 56f, 87.5f, 105)]
    [InlineData(10, Gender.Male, 37, 29.6f, 46.25f, 55.5f)]
    [InlineData(65, Gender.Male, 70, 77f, 84f, 91f)]
    void CalculateProtein(int age, Gender gender, int weight, float expected1, float expected2, float expected3)
    {
        //g / kg of bodyweight
        //Adults and children LB:0.8, II:1.25, UB:1.5
        //Elderly (>= 65) LB:1.1, II:1.2, UB:1.3

        //Act
        var actual = IntakeTargetCalculator.CalculateProtein(age, gender, weight);
        //Assert
        Assert.Equal(expected1, actual.Item1, 0.0001);
        Assert.Equal(expected2, actual.Item2, 0.0001);
        Assert.Equal(expected3, actual.Item3, 0.0001);
    }
    
    [Theory]
    [InlineData(25, Gender.Male, 2650f, 298.125f, 347.8125f, 397.5f)]
    void CalculateCarbohydrates(int age, Gender gender, float kcalPerDay, float expected1, float expected2, float expected3)
    {
        //Same for all people: LB: 45 E%, II: 52.5 E%, UB: 60E%
        //Act
        var actual = IntakeTargetCalculator.CalculateCarbohydrates(age, gender, kcalPerDay);
        //Assert
        Assert.Equal(expected1, actual.Item1, 0.0001);
        Assert.Equal(expected2, actual.Item2, 0.0001);
        Assert.Equal(expected3, actual.Item3, 0.0001);
    }

    
    [Theory]
    [InlineData(25, Gender.Male, 2650f, 0f, 0f, 66.25f)]
    void CalculateSugars(int age, Gender gender, float kcalPerDay, float expected1, float expected2, float expected3)
    {
        //Same for all people: LB: 0 E%, II: 0 E%, UB: 10E%
        //Act
        var actual = IntakeTargetCalculator.CalculateSugars(age, gender, kcalPerDay);
        //Assert
        Assert.Equal(expected1, actual.Item1, 0.0001);
        Assert.Equal(expected2, actual.Item2, 0.0001);
        Assert.Equal(expected3, actual.Item3, 0.0001);
    }

    
    [Theory]
    [InlineData(25, Gender.Male, 2650, 35f, 33.285f, float.PositiveInfinity)]
    [InlineData(25, Gender.Female, 2650, 25f, 33.285f, float.PositiveInfinity)]
    [InlineData(15, Gender.Male, 2650, 22.19004f, 27.73755, 33.285f)]
    
    void CalculateFibres(int age, Gender gender, float kcalPerDay, float expected1, float expected2, float expected3)
    {
        //Act
        var actual = IntakeTargetCalculator.CalculateFibres(age, gender, kcalPerDay);
        //Assert
        Assert.Equal(expected1, actual.Item1, 0.0001);
        Assert.Equal(expected2, actual.Item2, 0.0001);
        Assert.Equal(expected3, actual.Item3, 0.0001);
    }

    
    [Theory]
    [InlineData(25, Gender.Male, 2650f, 73.61111111111111f, 95.69444444444446f, 117.77777777777779f)]
    void CalculateTotalFat(int age, Gender gender, int weight, float expected1, float expected2, float expected3)
    {
        //Adults and children from 2 years of age
        //Act
        var actual = IntakeTargetCalculator.CalculateTotalFat(age, gender, weight);
        //Assert
        Assert.Equal(expected1, actual.Item1, 0.0001);
        Assert.Equal(expected2, actual.Item2, 0.0001);
        Assert.Equal(expected3, actual.Item3, 0.0001);
    }

    
    [Theory]
    [InlineData(25, Gender.Male, 2650f, 0f, 0f, 29.444444444444446f)]
    void CalculateSaturatedFat(int age, Gender gender, int weight, float expected1, float expected2, float expected3)
    {
        //Act
        var actual = IntakeTargetCalculator.CalculateSaturatedFat(age, gender, weight);
        //Assert
        Assert.Equal(expected1, actual.Item1, 0.0001);
        Assert.Equal(expected2, actual.Item2, 0.0001);
        Assert.Equal(expected3, actual.Item3, 0.0001);
    }

    
    [Theory]
    [InlineData(25, Gender.Male, 2650, 29.444444444444446f, 44.166666666666664f, 58.88888888888889f)]
    void CalculateMonounsaturatedFat(int age, Gender gender, int weight, float expected1, float expected2, float expected3)
    {
        //Act
        var actual = IntakeTargetCalculator.CalculateMonounsaturatedFat(age, gender, weight);
        //Assert
        Assert.Equal(expected1, actual.Item1, 0.0001);
        Assert.Equal(expected2, actual.Item2, 0.0001);
        Assert.Equal(expected3, actual.Item3, 0.0001);
    }

    
    [Theory]
    [InlineData(25, Gender.Male, 2650, 14.722222222222223f, 22.083333333333332f, 29.444444444444446f)]
    void CalculatePolyunsaturatedFat(int age, Gender gender, int weight, float expected1, float expected2, float expected3)
    {
        //Act
        var actual = IntakeTargetCalculator.CalculatePolyunsaturatedFat(age, gender, weight);
        //Assert
        Assert.Equal(expected1, actual.Item1, 0.0001);
        Assert.Equal(expected2, actual.Item2, 0.0001);
        Assert.Equal(expected3, actual.Item3, 0.0001);
    }

    [Theory]
    [InlineData(25, Gender.Male, 2650, 0f, 0f, 2.9444444444444446f)]
    void CalculateTransFat(int age, Gender gender, float kcalPerDay, float expected1, float expected2, float expected3)
    {
        //Act
        var actual = IntakeTargetCalculator.CalculateTransFat(age, gender, kcalPerDay);
        //Assert
        Assert.Equal(expected1, actual.Item1, 0.0001);
        Assert.Equal(expected2, actual.Item2, 0.0001);
        Assert.Equal(expected3, actual.Item3, 0.0001);
    }
    
    [Theory]
    [InlineData(25, Gender.Male, 70, 500f, 900f, float.PositiveInfinity)]
    [InlineData(25, Gender.Female, 55, 400f, 700f, 1500f)]
    [InlineData(3, Gender.Male, 13, 0f, 350f, float.PositiveInfinity)]
    [InlineData(7, Gender.Male, 25, 0f, 400f, float.PositiveInfinity)]
    [InlineData(11, Gender.Male, 30, 0f, 600f, float.PositiveInfinity)]
    void CalculateVitaminA(int age, Gender gender, int weight, float expected1, float expected2, float expected3)
    {
        //Act
        var actual = IntakeTargetCalculator.CalculateVitaminA(age, gender, weight);
        //Assert
        Assert.Equal(expected1, actual.Item1, 0.0001);
        Assert.Equal(expected2, actual.Item2, 0.0001);
        Assert.Equal(expected3, actual.Item3, 0.0001);
    }

    
    [Theory]
    [InlineData(25, Gender.Male, 70, 1.0f, 1.5f, 25f)]
    [InlineData(25, Gender.Female, 55, 0.8f, 1.2f, 25f)]
    [InlineData(3, Gender.Male, 13, 0f, 0.7f, float.PositiveInfinity)]
    [InlineData(7, Gender.Male, 25, 0f, 1.0f, float.PositiveInfinity)]
    [InlineData(11, Gender.Male, 30, 0f, 1.3f, float.PositiveInfinity)]
    [InlineData(11, Gender.Female, 30, 0f, 1.1f, float.PositiveInfinity)]
    void CalculateVitaminB6(int age, Gender gender, int weight, float expected1, float expected2, float expected3)
    {
        //Act
        var actual = IntakeTargetCalculator.CalculateVitaminB6(age, gender, weight);
        //Assert
        Assert.Equal(expected1, actual.Item1, 0.0001);
        Assert.Equal(expected2, actual.Item2, 0.0001);
        Assert.Equal(expected3, actual.Item3, 0.0001);
    }

    
    [Theory]
    [InlineData(25, Gender.Male, 70, 1.0f, 2.0f, float.PositiveInfinity)]
    [InlineData(25, Gender.Female, 55, 1.0f, 2.0f, float.PositiveInfinity)]
    [InlineData(3, Gender.Male, 13, 0f, 0.8f, float.PositiveInfinity)]
    [InlineData(7, Gender.Male, 25, 0f, 1.3f, float.PositiveInfinity)]
    [InlineData(11, Gender.Male, 30, 0f, 2.0f, float.PositiveInfinity)]
    void CalculateVitaminB12(int age, Gender gender, int weight, float expected1, float expected2, float expected3)
    {
        //Act
        var actual = IntakeTargetCalculator.CalculateVitaminB12(age, gender, weight);
        //Assert
        Assert.Equal(expected1, actual.Item1, 0.0001);
        Assert.Equal(expected2, actual.Item2, 0.0001);
        Assert.Equal(expected3, actual.Item3, 0.0001);
    }

    
    [Theory]
    [InlineData(25, Gender.Male, 70, 10.0f, 75.0f, float.PositiveInfinity)]
    [InlineData(3, Gender.Male, 13, 0f, 30f, float.PositiveInfinity)]
    [InlineData(7, Gender.Male, 25, 0f, 40f, float.PositiveInfinity)]
    [InlineData(11, Gender.Male, 30, 0f, 50f, float.PositiveInfinity)]
    void CalculateVitaminC(int age, Gender gender, int weight, float expected1, float expected2, float expected3)
    {
        //Act
        var actual = IntakeTargetCalculator.CalculateVitaminC(age, gender, weight);
        //Assert
        Assert.Equal(expected1, actual.Item1, 0.0001);
        Assert.Equal(expected2, actual.Item2, 0.0001);
        Assert.Equal(expected3, actual.Item3, 0.0001);
    }
    
    [Theory]
    [InlineData(25, Gender.Male, 70, 2.5f, 10.0f, 100.0f)]
    [InlineData(80, Gender.Male, 65, 2.5f, 20.0f, 100.0f)]
    void CalculateVitaminD(int age, Gender gender, int weight, float expected1, float expected2, float expected3)
    {
        //Act
        var actual = IntakeTargetCalculator.CalculateVitaminD(age, gender, weight);
        //Assert
        Assert.Equal(expected1, actual.Item1, 0.0001);
        Assert.Equal(expected2, actual.Item2, 0.0001);
        Assert.Equal(expected3, actual.Item3, 0.0001);
    }

    
    [Theory]
    [InlineData(25, Gender.Male, 70, 4f, 10f, 300f)]
    [InlineData(25, Gender.Female, 55, 3f, 8f, 300f)]
    [InlineData(3, Gender.Male, 13, 0f, 5f, float.PositiveInfinity)]
    [InlineData(7, Gender.Male, 25, 0f, 6f, float.PositiveInfinity)]
    [InlineData(11, Gender.Male, 30, 0f, 8f, float.PositiveInfinity)]
    [InlineData(11, Gender.Female, 30, 0f, 7f, float.PositiveInfinity)]
    void CalculateVitaminE(int age, Gender gender, int weight, float expected1, float expected2, float expected3)
    {
        //Act
        var actual = IntakeTargetCalculator.CalculateVitaminE(age, gender, weight);
        //Assert
        Assert.Equal(expected1, actual.Item1, 0.0001);
        Assert.Equal(expected2, actual.Item2, 0.0001);
        Assert.Equal(expected3, actual.Item3, 0.0001);
    }

    
    [Theory]
    [InlineData(25, Gender.Male, 2650, 0.6f, 1.4f, float.PositiveInfinity)]
    [InlineData(25, Gender.Female, 2650, 0.5f, 1.1f, float.PositiveInfinity)]
    [InlineData(3, Gender.Male, 1500, 0f, 0.6f, float.PositiveInfinity)]
    [InlineData(7, Gender.Male, 1700, 0f, 0.9f, float.PositiveInfinity)]
    [InlineData(11, Gender.Male, 1800, 0f, 1.2f, float.PositiveInfinity)]
    [InlineData(11, Gender.Female, 1800, 0f, 1.0f, float.PositiveInfinity)]
    [InlineData(75, Gender.Male, 2000, 1.0f, 1.4f, float.PositiveInfinity)]
    [InlineData(25, Gender.Male, 1600, 0.8f, 1.4f, float.PositiveInfinity)]
    void CalculateThiamin(int age, Gender gender, float kcalPerDay, float expected1, float expected2, float expected3)
    {
        //Act
        var actual = IntakeTargetCalculator.CalculateThiamin(age, gender, kcalPerDay);
        //Assert
        Assert.Equal(expected1, actual.Item1, 0.0001);
        Assert.Equal(expected2, actual.Item2, 0.0001);
        Assert.Equal(expected3, actual.Item3, 0.0001);
    }

    
    [Theory]
    [InlineData(25, Gender.Male, 70, 0.8f, 1.7f, float.PositiveInfinity)]
    [InlineData(25, Gender.Female, 55, 0.8f, 1.3f, float.PositiveInfinity)]
    [InlineData(3, Gender.Male, 13, 0f, 0.7f, float.PositiveInfinity)]
    [InlineData(7, Gender.Male, 25, 0f, 1.1f, float.PositiveInfinity)]
    [InlineData(11, Gender.Male, 30, 0f, 1.4f, float.PositiveInfinity)]
    [InlineData(11, Gender.Female, 30, 0f, 1.2f, float.PositiveInfinity)]
    void CalculateRiboflavin(int age, Gender gender, int weight, float expected1, float expected2, float expected3)
    {
        //Act
        var actual = IntakeTargetCalculator.CalculateRiboflavin(age, gender, weight);
        //Assert
        Assert.Equal(expected1, actual.Item1, 0.0001);
        Assert.Equal(expected2, actual.Item2, 0.0001);
        Assert.Equal(expected3, actual.Item3, 0.0001);
    }

    
    [Theory]
    [InlineData(25, Gender.Male, 2650, 12f, 18f, 35f)]
    [InlineData(25, Gender.Female, 2650, 9f, 15f, 35f)]
    [InlineData(3, Gender.Male, 1500, 0f, 9f, float.PositiveInfinity)]
    [InlineData(7, Gender.Male, 1700, 0f, 12f, float.PositiveInfinity)]
    [InlineData(11, Gender.Male, 1800, 0f, 16f, float.PositiveInfinity)]
    [InlineData(11, Gender.Female, 1800, 0f, 14f, float.PositiveInfinity)]
    [InlineData(25, Gender.Male, 1600, 8f, 18f, 35f)]
    void CalculateNiacin(int age, Gender gender, float kcalPerDay, float expected1, float expected2, float expected3)
    {
        //Act
        var actual = IntakeTargetCalculator.CalculateNiacin(age, gender, kcalPerDay);
        //Assert
        Assert.Equal(expected1, actual.Item1, 0.0001);
        Assert.Equal(expected2, actual.Item2, 0.0001);
        Assert.Equal(expected3, actual.Item3, 0.0001);
    }

    
    [Theory]
    [InlineData(25, Gender.Male, 2650, 100f, 300f, float.PositiveInfinity)]
    [InlineData(25, Gender.Female, 2650, 100f, 400f, float.PositiveInfinity)]
    [InlineData(60, Gender.Female, 2650, 100f, 300f, float.PositiveInfinity)]
    [InlineData(3, Gender.Male, 1500, 0f, 80f, float.PositiveInfinity)]
    [InlineData(7, Gender.Male, 1700, 0f, 130f, float.PositiveInfinity)]
    [InlineData(11, Gender.Male, 1800, 0f, 200f, float.PositiveInfinity)]
    void CalculateFolate(int age, Gender gender, int weight, float expected1, float expected2, float expected3)
    {
        //Act
        var actual = IntakeTargetCalculator.CalculateFolate(age, gender, weight);
        //Assert
        Assert.Equal(expected1, actual.Item1, 0.0001);
        Assert.Equal(expected2, actual.Item2, 0.0001);
        Assert.Equal(expected3, actual.Item3, 0.0001);
    }

    /* TODO
    [Theory]
    [InlineData(25, Gender.Male, 70, 56f, 87.5f, 105)]
    void CalculateSalt(int age, Gender gender, int weight, float expected1, float expected2, float expected3)
    {
        //Act
        var actual = IntakeTargetCalculator.CalculateSalt(age, gender, weight);
        //Assert
        Assert.Equal(expected1, actual.Item1, 0.0001);
        Assert.Equal(expected2, actual.Item2, 0.0001);
        Assert.Equal(expected3, actual.Item3, 0.0001);
    }
    */

    
    [Theory]
    [InlineData(25, Gender.Male, 70, 1.6f, 3.5f, float.PositiveInfinity)]
    [InlineData(25, Gender.Female, 55, 1.6f, 3.1f, float.PositiveInfinity)]
    [InlineData(3, Gender.Male, 13, 0f, 1.8f, float.PositiveInfinity)]
    [InlineData(7, Gender.Male, 25, 0f, 2.0f, float.PositiveInfinity)]
    [InlineData(11, Gender.Male, 30, 0f, 3.3f, float.PositiveInfinity)]
    [InlineData(11, Gender.Female, 30, 0f, 2.9f, float.PositiveInfinity)]
    void CalculatePotassium(int age, Gender gender, int weight, float expected1, float expected2, float expected3)
    {
        //Act
        var actual = IntakeTargetCalculator.CalculatePotassium(age, gender, weight);
        //Assert
        Assert.Equal(expected1, actual.Item1, 0.0001);
        Assert.Equal(expected2, actual.Item2, 0.0001);
        Assert.Equal(expected3, actual.Item3, 0.0001);
    }

    
    [Theory]
    [InlineData(25, Gender.Male, 70, 0f, 350f, float.PositiveInfinity)]
    [InlineData(25, Gender.Female, 55, 0f, 280f, float.PositiveInfinity)]
    [InlineData(3, Gender.Male, 13, 0f, 120f, float.PositiveInfinity)]
    [InlineData(7, Gender.Male, 25, 0f, 200f, float.PositiveInfinity)]
    [InlineData(11, Gender.Male, 30, 0f, 280f, float.PositiveInfinity)]
    void CalculateMagnesium(int age, Gender gender, int weight, float expected1, float expected2, float expected3)
    {
        //Act
        var actual = IntakeTargetCalculator.CalculateMagnesium(age, gender, weight);
        //Assert
        Assert.Equal(expected1, actual.Item1, 0.0001);
        Assert.Equal(expected2, actual.Item2, 0.0001);
        Assert.Equal(expected3, actual.Item3, 0.0001);
    }

    
    [Theory]
    [InlineData(25, Gender.Male, 70, 7f, 9f, 60f)]
    [InlineData(25, Gender.Female, 55, 7f, 15f, 60f)]
    [InlineData(3, Gender.Male, 13, 0f, 8f, float.PositiveInfinity)]
    [InlineData(7, Gender.Male, 25, 0f, 9f, float.PositiveInfinity)]
    [InlineData(11, Gender.Male, 30, 0f, 11f, float.PositiveInfinity)]
    [InlineData(60, Gender.Female, 55, 5f, 9f, 60f)]
    void CalculateIron(int age, Gender gender, int weight, float expected1, float expected2, float expected3)
    {
        //Act
        var actual = IntakeTargetCalculator.CalculateIron(age, gender, weight);
        //Assert
        Assert.Equal(expected1, actual.Item1, 0.0001);
        Assert.Equal(expected2, actual.Item2, 0.0001);
        Assert.Equal(expected3, actual.Item3, 0.0001);
    }

    
    [Theory]
    [InlineData(25, Gender.Male, 70, 5f, 9f, float.PositiveInfinity)]
    [InlineData(25, Gender.Female, 55, 4, 7f, float.PositiveInfinity)]
    [InlineData(3, Gender.Male, 13, 0f, 6f, float.PositiveInfinity)]
    [InlineData(7, Gender.Male, 25, 0f, 7f, float.PositiveInfinity)]
    [InlineData(11, Gender.Male, 30, 0f, 11f, float.PositiveInfinity)]
    [InlineData(11, Gender.Female, 30, 0f, 8f, float.PositiveInfinity)]
    void CalculateZinc(int age, Gender gender, int weight, float expected1, float expected2, float expected3)
    {
        //Act
        var actual = IntakeTargetCalculator.CalculateZinc(age, gender, weight);
        //Assert
        Assert.Equal(expected1, actual.Item1, 0.0001);
        Assert.Equal(expected2, actual.Item2, 0.0001);
        Assert.Equal(expected3, actual.Item3, 0.0001);
    }

    
    [Theory]
    [InlineData(25, Gender.Male, 70, 300f, 600f, 3000f)]
    [InlineData(3, Gender.Male, 13, 0f, 470f, float.PositiveInfinity)]
    [InlineData(7, Gender.Male, 25, 0f, 540f, float.PositiveInfinity)]
    [InlineData(11, Gender.Male, 30, 0f, 700f, float.PositiveInfinity)]
    void CalculatePhosphorus(int age, Gender gender, int weight, float expected1, float expected2, float expected3)
    {
        //Act
        var actual = IntakeTargetCalculator.CalculatePhosphorus(age, gender, weight);
        //Assert
        Assert.Equal(expected1, actual.Item1, 0.0001);
        Assert.Equal(expected2, actual.Item2, 0.0001);
        Assert.Equal(expected3, actual.Item3, 0.0001);
    }

    
    [Theory]
    [InlineData(25, Gender.Male, 70, 0.4f, 0.9f, 5.0f)]
    [InlineData(3, Gender.Male, 13, 0f, 0.4f, float.PositiveInfinity)]
    [InlineData(7, Gender.Male, 25, 0f, 0.5f, float.PositiveInfinity)]
    [InlineData(11, Gender.Male, 30, 0f, 0.7f, float.PositiveInfinity)]
    void CalculateCopper(int age, Gender gender, int weight, float expected1, float expected2, float expected3)
    {
        //Act
        var actual = IntakeTargetCalculator.CalculateCopper(age, gender, weight);
        //Assert
        Assert.Equal(expected1, actual.Item1, 0.0001);
        Assert.Equal(expected2, actual.Item2, 0.0001);
        Assert.Equal(expected3, actual.Item3, 0.0001);
    }

    
    [Theory]
    [InlineData(25, Gender.Male, 70, 70f, 150f, 600f)]
    [InlineData(3, Gender.Male, 13, 0f, 90f, float.PositiveInfinity)]
    [InlineData(7, Gender.Male, 25, 0f, 150f, float.PositiveInfinity)]
    [InlineData(11, Gender.Male, 30, 0f, 120f, float.PositiveInfinity)]
    void CalculateIodine(int age, Gender gender, int weight, float expected1, float expected2, float expected3)
    {
        //Act
        var actual = IntakeTargetCalculator.CalculateIodine(age, gender, weight);
        //Assert
        Assert.Equal(expected1, actual.Item1, 0.0001);
        Assert.Equal(expected2, actual.Item2, 0.0001);
        Assert.Equal(expected3, actual.Item3, 0.0001);
    }

    
    [Theory]
    [InlineData(25, Gender.Male, 70, 20f, 60f, 300f)]
    [InlineData(25, Gender.Female, 55, 20f, 50f, 300f)]
    [InlineData(3, Gender.Male, 13, 0f, 25f, float.PositiveInfinity)]
    [InlineData(7, Gender.Male, 25, 0f, 30f, float.PositiveInfinity)]
    [InlineData(11, Gender.Male, 30, 0f, 40f, float.PositiveInfinity)]
    void CalculateSelenium(int age, Gender gender, int weight, float expected1, float expected2, float expected3)
    {
        //Act
        var actual = IntakeTargetCalculator.CalculateSelenium(age, gender, weight);
        //Assert
        Assert.Equal(expected1, actual.Item1, 0.0001);
        Assert.Equal(expected2, actual.Item2, 0.0001);
        Assert.Equal(expected3, actual.Item3, 0.0001);
    }

    
    [Theory]
    [InlineData(25, Gender.Male, 70, 400f, 800f, 2500f)]
    [InlineData(3, Gender.Male, 13, 0f, 600f, float.PositiveInfinity)]
    [InlineData(7, Gender.Male, 25, 0f, 700f, float.PositiveInfinity)]
    [InlineData(11, Gender.Male, 30, 0f, 900f, float.PositiveInfinity)]
    void CalculateCalcium(int age, Gender gender, int weight, float expected1, float expected2, float expected3)
    {
        //Act
        var actual = IntakeTargetCalculator.CalculateCalcium(age, gender, weight);
        //Assert
        Assert.Equal(expected1, actual.Item1, 0.0001);
        Assert.Equal(expected2, actual.Item2, 0.0001);
        Assert.Equal(expected3, actual.Item3, 0.0001);
    }
}