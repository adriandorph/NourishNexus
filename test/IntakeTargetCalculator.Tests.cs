using server.Services.MealPlan;

namespace test;

public class IntakeTargetCalculatorTests
{
    private readonly IntakeTargetCalculator _intakeTargetCalculator;
    public IntakeTargetCalculatorTests()
    {
        _intakeTargetCalculator = new IntakeTargetCalculator();
    }


    [Theory]
    [InlineData(25, Gender.Male, 2650f, 66.25f, 99.375f, 132.5f)]
    [InlineData(10, Gender.Male, 1500, 37.5f, 56.25f, 75.0f)]
    [InlineData(65, Gender.Male, 2300f, 86.25f, 100.625f, 115.0f)]
    void CalculateProtein(int age, Gender gender, float kcalPerDay, float expected1, float expected2, float expected3)
    {
        //Act
        var actual = _intakeTargetCalculator.CalculateProtein(age, gender, kcalPerDay);
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
        var actual = _intakeTargetCalculator.CalculateCarbohydrates(age, gender, kcalPerDay);
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
        var actual = _intakeTargetCalculator.CalculateSugars(age, gender, kcalPerDay);
        //Assert
        Assert.Equal(expected1, actual.Item1, 0.0001);
        Assert.Equal(expected2, actual.Item2, 0.0001);
        Assert.Equal(expected3, actual.Item3, 0.0001);
    }

    
    [Theory]
    [InlineData(25, Gender.Male, 2650, 35f, 33.285f, float.MaxValue)]
    [InlineData(25, Gender.Female, 2650, 25f, 33.285f, float.MaxValue)]
    [InlineData(15, Gender.Male, 2650, 22.19004f, 27.73755, 33.285f)]
    
    void CalculateFibres(int age, Gender gender, float kcalPerDay, float expected1, float expected2, float expected3)
    {
        //Act
        var actual = _intakeTargetCalculator.CalculateFibres(age, gender, kcalPerDay);
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
        var actual = _intakeTargetCalculator.CalculateTotalFat(age, gender, weight);
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
        var actual = _intakeTargetCalculator.CalculateSaturatedFat(age, gender, weight);
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
        var actual = _intakeTargetCalculator.CalculateMonounsaturatedFat(age, gender, weight);
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
        var actual = _intakeTargetCalculator.CalculatePolyunsaturatedFat(age, gender, weight);
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
        var actual = _intakeTargetCalculator.CalculateTransFat(age, gender, kcalPerDay);
        //Assert
        Assert.Equal(expected1, actual.Item1, 0.0001);
        Assert.Equal(expected2, actual.Item2, 0.0001);
        Assert.Equal(expected3, actual.Item3, 0.0001);
    }
    
    [Theory]
    [InlineData(25, Gender.Male, 500f, 900f, float.MaxValue)]
    [InlineData(25, Gender.Female, 400f, 700f, 1500f)]
    [InlineData(3, Gender.Male, 0f, 350f, float.MaxValue)]
    [InlineData(7, Gender.Male, 0f, 400f, float.MaxValue)]
    [InlineData(11, Gender.Male, 0f, 600f, float.MaxValue)]
    void CalculateVitaminA(int age, Gender gender, float expected1, float expected2, float expected3)
    {
        //Act
        var actual = _intakeTargetCalculator.CalculateVitaminA(age, gender);
        //Assert
        Assert.Equal(expected1, actual.Item1, 0.0001);
        Assert.Equal(expected2, actual.Item2, 0.0001);
        Assert.Equal(expected3, actual.Item3, 0.0001);
    }

    
    [Theory]
    [InlineData(25, Gender.Male, 1.0f, 1.5f, 25f)]
    [InlineData(25, Gender.Female, 0.8f, 1.2f, 25f)]
    [InlineData(3, Gender.Male, 0f, 0.7f, float.MaxValue)]
    [InlineData(7, Gender.Male, 0f, 1.0f, float.MaxValue)]
    [InlineData(11, Gender.Male, 0f, 1.3f, float.MaxValue)]
    [InlineData(11, Gender.Female, 0f, 1.1f, float.MaxValue)]
    void CalculateVitaminB6(int age, Gender gender, float expected1, float expected2, float expected3)
    {
        //Act
        var actual = _intakeTargetCalculator.CalculateVitaminB6(age, gender);
        //Assert
        Assert.Equal(expected1, actual.Item1, 0.0001);
        Assert.Equal(expected2, actual.Item2, 0.0001);
        Assert.Equal(expected3, actual.Item3, 0.0001);
    }

    
    [Theory]
    [InlineData(25, Gender.Male, 1.0f, 2.0f, float.MaxValue)]
    [InlineData(25, Gender.Female, 1.0f, 2.0f, float.MaxValue)]
    [InlineData(3, Gender.Male, 0f, 0.8f, float.MaxValue)]
    [InlineData(7, Gender.Male, 0f, 1.3f, float.MaxValue)]
    [InlineData(11, Gender.Male, 0f, 2.0f, float.MaxValue)]
    void CalculateVitaminB12(int age, Gender gender, float expected1, float expected2, float expected3)
    {
        //Act
        var actual = _intakeTargetCalculator.CalculateVitaminB12(age, gender);
        //Assert
        Assert.Equal(expected1, actual.Item1, 0.0001);
        Assert.Equal(expected2, actual.Item2, 0.0001);
        Assert.Equal(expected3, actual.Item3, 0.0001);
    }

    
    [Theory]
    [InlineData(25, 10.0f, 75.0f, float.MaxValue)]
    [InlineData(3, 0f, 30f, float.MaxValue)]
    [InlineData(7, 0f, 40f, float.MaxValue)]
    [InlineData(11, 0f, 50f, float.MaxValue)]
    void CalculateVitaminC(int age, float expected1, float expected2, float expected3)
    {
        //Act
        var actual = _intakeTargetCalculator.CalculateVitaminC(age);
        //Assert
        Assert.Equal(expected1, actual.Item1, 0.0001);
        Assert.Equal(expected2, actual.Item2, 0.0001);
        Assert.Equal(expected3, actual.Item3, 0.0001);
    }
    
    [Theory]
    [InlineData(25, 2.5f, 10.0f, 100.0f)]
    [InlineData(80, 2.5f, 20.0f, 100.0f)]
    void CalculateVitaminD(int age, float expected1, float expected2, float expected3)
    {
        //Act
        var actual = _intakeTargetCalculator.CalculateVitaminD(age);
        //Assert
        Assert.Equal(expected1, actual.Item1, 0.0001);
        Assert.Equal(expected2, actual.Item2, 0.0001);
        Assert.Equal(expected3, actual.Item3, 0.0001);
    }

    
    [Theory]
    [InlineData(25, Gender.Male, 4f, 10f, 300f)]
    [InlineData(25, Gender.Female, 3f, 8f, 300f)]
    [InlineData(3, Gender.Male, 0f, 5f, float.MaxValue)]
    [InlineData(7, Gender.Male, 0f, 6f, float.MaxValue)]
    [InlineData(11, Gender.Male, 0f, 8f, float.MaxValue)]
    [InlineData(11, Gender.Female, 0f, 7f, float.MaxValue)]
    void CalculateVitaminE(int age, Gender gender, float expected1, float expected2, float expected3)
    {
        //Act
        var actual = _intakeTargetCalculator.CalculateVitaminE(age, gender);
        //Assert
        Assert.Equal(expected1, actual.Item1, 0.0001);
        Assert.Equal(expected2, actual.Item2, 0.0001);
        Assert.Equal(expected3, actual.Item3, 0.0001);
    }

    
    [Theory]
    [InlineData(25, Gender.Male, 2650, 0.6f, 1.4f, float.MaxValue)]
    [InlineData(25, Gender.Female, 2650, 0.5f, 1.1f, float.MaxValue)]
    [InlineData(3, Gender.Male, 1500, 0f, 0.6f, float.MaxValue)]
    [InlineData(7, Gender.Male, 1700, 0f, 0.9f, float.MaxValue)]
    [InlineData(11, Gender.Male, 1800, 0f, 1.2f, float.MaxValue)]
    [InlineData(11, Gender.Female, 1800, 0f, 1.0f, float.MaxValue)]
    [InlineData(75, Gender.Male, 2000, 1.0f, 1.4f, float.MaxValue)]
    [InlineData(25, Gender.Male, 1600, 0.8f, 1.4f, float.MaxValue)]
    void CalculateThiamin(int age, Gender gender, float kcalPerDay, float expected1, float expected2, float expected3)
    {
        //Act
        var actual = _intakeTargetCalculator.CalculateThiamin(age, gender, kcalPerDay);
        //Assert
        Assert.Equal(expected1, actual.Item1, 0.0001);
        Assert.Equal(expected2, actual.Item2, 0.0001);
        Assert.Equal(expected3, actual.Item3, 0.0001);
    }

    
    [Theory]
    [InlineData(25, Gender.Male, 0.8f, 1.7f, float.MaxValue)]
    [InlineData(25, Gender.Female, 0.8f, 1.3f, float.MaxValue)]
    [InlineData(3, Gender.Male, 0f, 0.7f, float.MaxValue)]
    [InlineData(7, Gender.Male, 0f, 1.1f, float.MaxValue)]
    [InlineData(11, Gender.Male, 0f, 1.4f, float.MaxValue)]
    [InlineData(11, Gender.Female, 0f, 1.2f, float.MaxValue)]
    void CalculateRiboflavin(int age, Gender gender, float expected1, float expected2, float expected3)
    {
        //Act
        var actual = _intakeTargetCalculator.CalculateRiboflavin(age, gender);
        //Assert
        Assert.Equal(expected1, actual.Item1, 0.0001);
        Assert.Equal(expected2, actual.Item2, 0.0001);
        Assert.Equal(expected3, actual.Item3, 0.0001);
    }

    
    [Theory]
    [InlineData(25, Gender.Male, 2650, 12f, 18f, 35f)]
    [InlineData(25, Gender.Female, 2650, 9f, 15f, 35f)]
    [InlineData(3, Gender.Male, 1500, 0f, 9f, float.MaxValue)]
    [InlineData(7, Gender.Male, 1700, 0f, 12f, float.MaxValue)]
    [InlineData(11, Gender.Male, 1800, 0f, 16f, float.MaxValue)]
    [InlineData(11, Gender.Female, 1800, 0f, 14f, float.MaxValue)]
    [InlineData(25, Gender.Male, 1600, 8f, 18f, 35f)]
    void CalculateNiacin(int age, Gender gender, float kcalPerDay, float expected1, float expected2, float expected3)
    {
        //Act
        var actual = _intakeTargetCalculator.CalculateNiacin(age, gender, kcalPerDay);
        //Assert
        Assert.Equal(expected1, actual.Item1, 0.0001);
        Assert.Equal(expected2, actual.Item2, 0.0001);
        Assert.Equal(expected3, actual.Item3, 0.0001);
    }

    
    [Theory]
    [InlineData(25, Gender.Male, 100f, 300f, float.MaxValue)]
    [InlineData(25, Gender.Female, 100f, 400f, float.MaxValue)]
    [InlineData(60, Gender.Female, 100f, 300f, float.MaxValue)]
    [InlineData(3, Gender.Male, 0f, 80f, float.MaxValue)]
    [InlineData(7, Gender.Male, 0f, 130f, float.MaxValue)]
    [InlineData(11, Gender.Male, 0f, 200f, float.MaxValue)]
    void CalculateFolate(int age, Gender gender, float expected1, float expected2, float expected3)
    {
        //Act
        var actual = _intakeTargetCalculator.CalculateFolate(age, gender);
        //Assert
        Assert.Equal(expected1, actual.Item1, 0.0001);
        Assert.Equal(expected2, actual.Item2, 0.0001);
        Assert.Equal(expected3, actual.Item3, 0.0001);
    }

    
    [Theory]
    [InlineData(25, 2650, 1.5f, 4f, 6f)]
    [InlineData(7, 1700, 0f, 3.55878, 6f)]
    void CalculateSalt(int age, float kcalPerDay, float expected1, float expected2, float expected3)
    {
        //Act
        var actual = _intakeTargetCalculator.CalculateSalt(age, kcalPerDay);
        //Assert
        Assert.Equal(expected1, actual.Item1, 0.0001);
        Assert.Equal(expected2, actual.Item2, 0.0001);
        Assert.Equal(expected3, actual.Item3, 0.0001);
    }
    

    
    [Theory]
    [InlineData(25, Gender.Male, 1.6f, 3.5f, float.MaxValue)]
    [InlineData(25, Gender.Female, 1.6f, 3.1f, float.MaxValue)]
    [InlineData(3, Gender.Male, 0f, 1.8f, float.MaxValue)]
    [InlineData(7, Gender.Male, 0f, 2.0f, float.MaxValue)]
    [InlineData(11, Gender.Male, 0f, 3.3f, float.MaxValue)]
    [InlineData(11, Gender.Female, 0f, 2.9f, float.MaxValue)]
    void CalculatePotassium(int age, Gender gender, float expected1, float expected2, float expected3)
    {
        //Act
        var actual = _intakeTargetCalculator.CalculatePotassium(age, gender);
        //Assert
        Assert.Equal(expected1, actual.Item1, 0.0001);
        Assert.Equal(expected2, actual.Item2, 0.0001);
        Assert.Equal(expected3, actual.Item3, 0.0001);
    }

    
    [Theory]
    [InlineData(25, Gender.Male, 0f, 350f, float.MaxValue)]
    [InlineData(25, Gender.Female, 0f, 280f, float.MaxValue)]
    [InlineData(3, Gender.Male, 0f, 120f, float.MaxValue)]
    [InlineData(7, Gender.Male, 0f, 200f, float.MaxValue)]
    [InlineData(11, Gender.Male, 0f, 280f, float.MaxValue)]
    void CalculateMagnesium(int age, Gender gender, float expected1, float expected2, float expected3)
    {
        //Act
        var actual = _intakeTargetCalculator.CalculateMagnesium(age, gender);
        //Assert
        Assert.Equal(expected1, actual.Item1, 0.0001);
        Assert.Equal(expected2, actual.Item2, 0.0001);
        Assert.Equal(expected3, actual.Item3, 0.0001);
    }

    
    [Theory]
    [InlineData(25, Gender.Male, 7f, 9f, 60f)]
    [InlineData(25, Gender.Female, 7f, 15f, 60f)]
    [InlineData(3, Gender.Male, 0f, 8f, float.MaxValue)]
    [InlineData(7, Gender.Male, 0f, 9f, float.MaxValue)]
    [InlineData(11, Gender.Male, 0f, 11f, float.MaxValue)]
    [InlineData(60, Gender.Female, 5f, 9f, 60f)]
    void CalculateIron(int age, Gender gender, float expected1, float expected2, float expected3)
    {
        //Act
        var actual = _intakeTargetCalculator.CalculateIron(age, gender);
        //Assert
        Assert.Equal(expected1, actual.Item1, 0.0001);
        Assert.Equal(expected2, actual.Item2, 0.0001);
        Assert.Equal(expected3, actual.Item3, 0.0001);
    }

    
    [Theory]
    [InlineData(25, Gender.Male, 5f, 9f, float.MaxValue)]
    [InlineData(25, Gender.Female, 4f, 7f, float.MaxValue)]
    [InlineData(3, Gender.Male, 0f, 6f, float.MaxValue)]
    [InlineData(7, Gender.Male, 0f, 7f, float.MaxValue)]
    [InlineData(11, Gender.Male, 0f, 11f, float.MaxValue)]
    [InlineData(11, Gender.Female, 0f, 8f, float.MaxValue)]
    void CalculateZinc(int age, Gender gender, float expected1, float expected2, float expected3)
    {
        //Act
        var actual = _intakeTargetCalculator.CalculateZinc(age, gender);
        //Assert
        Assert.Equal(expected1, actual.Item1, 0.0001);
        Assert.Equal(expected2, actual.Item2, 0.0001);
        Assert.Equal(expected3, actual.Item3, 0.0001);
    }

    
    [Theory]
    [InlineData(25, 300f, 600f, 3000f)]
    [InlineData(3, 0f, 470f, float.MaxValue)]
    [InlineData(7, 0f, 540f, float.MaxValue)]
    [InlineData(11, 0f, 700f, float.MaxValue)]
    void CalculatePhosphorus(int age, float expected1, float expected2, float expected3)
    {
        //Act
        var actual = _intakeTargetCalculator.CalculatePhosphorus(age);
        //Assert
        Assert.Equal(expected1, actual.Item1, 0.0001);
        Assert.Equal(expected2, actual.Item2, 0.0001);
        Assert.Equal(expected3, actual.Item3, 0.0001);
    }

    
    [Theory]
    [InlineData(25, 0.4f, 0.9f, 5.0f)]
    [InlineData(3, 0f, 0.4f, float.MaxValue)]
    [InlineData(7, 0f, 0.5f, float.MaxValue)]
    [InlineData(11, 0f, 0.7f, float.MaxValue)]
    void CalculateCopper(int age, float expected1, float expected2, float expected3)
    {
        //Act
        var actual = _intakeTargetCalculator.CalculateCopper(age);
        //Assert
        Assert.Equal(expected1, actual.Item1, 0.0001);
        Assert.Equal(expected2, actual.Item2, 0.0001);
        Assert.Equal(expected3, actual.Item3, 0.0001);
    }

    
    [Theory]
    [InlineData(25, 70f, 150f, 600f)]
    [InlineData(3, 0f, 90f, float.MaxValue)]
    [InlineData(7, 0f, 150f, float.MaxValue)]
    [InlineData(11, 0f, 120f, float.MaxValue)]
    void CalculateIodine(int age, float expected1, float expected2, float expected3)
    {
        //Act
        var actual = _intakeTargetCalculator.CalculateIodine(age);
        //Assert
        Assert.Equal(expected1, actual.Item1, 0.0001);
        Assert.Equal(expected2, actual.Item2, 0.0001);
        Assert.Equal(expected3, actual.Item3, 0.0001);
    }

    
    [Theory]
    [InlineData(25, Gender.Male, 20f, 60f, 300f)]
    [InlineData(25, Gender.Female, 20f, 50f, 300f)]
    [InlineData(3, Gender.Male, 0f, 25f, float.MaxValue)]
    [InlineData(7, Gender.Male, 0f, 30f, float.MaxValue)]
    [InlineData(11, Gender.Male, 0f, 40f, float.MaxValue)]
    void CalculateSelenium(int age, Gender gender, float expected1, float expected2, float expected3)
    {
        //Act
        var actual = _intakeTargetCalculator.CalculateSelenium(age, gender);
        //Assert
        Assert.Equal(expected1, actual.Item1, 0.0001);
        Assert.Equal(expected2, actual.Item2, 0.0001);
        Assert.Equal(expected3, actual.Item3, 0.0001);
    }

    
    [Theory]
    [InlineData(25, Gender.Male, 400f, 800f, 2500f)]
    [InlineData(3, Gender.Male, 0f, 600f, float.MaxValue)]
    [InlineData(7, Gender.Male, 0f, 700f, float.MaxValue)]
    [InlineData(11, Gender.Male, 0f, 900f, float.MaxValue)]
    void CalculateCalcium(int age, Gender gender, float expected1, float expected2, float expected3)
    {
        //Act
        var actual = _intakeTargetCalculator.CalculateCalcium(age, gender);
        //Assert
        Assert.Equal(expected1, actual.Item1, 0.0001);
        Assert.Equal(expected2, actual.Item2, 0.0001);
        Assert.Equal(expected3, actual.Item3, 0.0001);
    }
}