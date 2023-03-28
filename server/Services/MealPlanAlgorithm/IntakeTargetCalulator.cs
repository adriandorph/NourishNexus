namespace server.Services.MealPlan;

public interface IInakeTargetCalculator
{
    NutrientTargets CalculateTargets(int age, Gender gender, int weight);
}

public static class IntakeTargetCalculator
{
    private static float MJPerKcal = 0.0041868f;
    public static (NutrientTargets, NutrientTargets, NutrientTargets) CalculateTargets(int age, Gender gender, int weight, float kcalPerDay)
    {
        (float ProteinLB, float ProteinII, float ProteinUB) = CalculateProtein(age, gender, weight);
        (float CarbohydratesLB, float CarbohydratesII, float CarbohydratesUB) = CalculateCarbohydrates(age, gender, kcalPerDay);
        (float SugarsLB, float SugarsII, float SugarsUB) = CalculateSugars(age, gender, kcalPerDay);
        (float FibresLB, float FibresII, float FibresUB) = CalculateFibres(age, gender, kcalPerDay);
        (float TotalFatLB, float TotalFatII, float TotalFatUB) = CalculateTotalFat(age, gender, kcalPerDay);
        (float SaturatedFatLB, float SaturatedFatII, float SaturatedFatUB) = CalculateSaturatedFat(age, gender, kcalPerDay);
        (float MonounsaturatedFatLB, float MonounsaturatedFatII, float MonounsaturatedFatUB) = CalculateMonounsaturatedFat(age, gender, kcalPerDay);
        (float PolyunsaturatedFatLB, float PolyunsaturatedFatII, float PolyunsaturatedFatUB) = CalculatePolyunsaturatedFat(age, gender, kcalPerDay);
        (float TransFatLB, float TransFatII, float TransFatUB) = CalculateTransFat(age, gender, kcalPerDay);
        (float VitaminALB, float VitaminAII, float VitaminAUB) = CalculateVitaminA(age, gender, weight);
        (float VitaminB6LB, float VitaminB6II, float VitaminB6UB) = CalculateVitaminB6(age, gender, weight);
        (float VitaminB12LB, float VitaminB12II, float VitaminB12UB) = CalculateVitaminB12(age, gender, weight);
        (float VitaminCLB, float VitaminCII, float VitaminCUB) = CalculateVitaminC(age, gender, weight);
        (float VitaminDLB, float VitaminDII, float VitaminDUB) = CalculateVitaminD(age, gender, weight);
        (float VitaminELB, float VitaminEII, float VitaminEUB) = CalculateVitaminE(age, gender, weight);
        (float ThiaminLB, float ThiaminII, float ThiaminUB) = CalculateThiamin(age, gender, kcalPerDay);
        (float RiboflavinLB, float RiboflavinII, float RiboflavinUB) = CalculateRiboflavin(age, gender, weight);
        (float NiacinLB, float NiacinII, float NiacinUB) = CalculateNiacin(age, gender, weight);
        (float FolateLB, float FolateII, float FolateUB) = CalculateFolate(age, gender, weight);
        (float SaltLB, float SaltII, float SaltUB) = CalculateSalt(age, gender, weight);
        (float PotassiumLB, float PotassiumII, float PotassiumUB) = CalculatePotassium(age, gender, weight);
        (float MagnesiumLB, float MagnesiumII, float MagnesiumUB) = CalculateMagnesium(age, gender, weight);
        (float IronLB, float IronII, float IronUB) = CalculateIron(age, gender, weight);
        (float ZincLB, float ZincII, float ZincUB) = CalculateZinc(age, gender, weight);
        (float PhosphorusLB, float PhosphorusII, float PhosphorusUB) = CalculatePhosphorus(age, gender, weight);
        (float CopperLB, float CopperII, float CopperUB) = CalculateCopper(age, gender, weight);
        (float IodineLB, float IodineII, float IodineUB) = CalculateIodine(age, gender, weight);
        (float SeleniumLB, float SeleniumII, float SeleniumUB) = CalculateSelenium(age, gender, weight);
        (float CalciumLB, float CalciumII, float CalciumUB) = CalculateCalcium(age, gender, weight);
    
        var LB = new NutrientTargets
        {
            Protein = ProteinLB,
            Carbohydrates = CarbohydratesLB,
            Sugars = SugarsLB,
            Fibres = FibresLB,
            TotalFat = TotalFatLB,
            SaturatedFat = SaturatedFatLB,
            MonounsaturatedFat = MonounsaturatedFatLB,
            PolyunsaturatedFat = PolyunsaturatedFatLB,
            TransFat = TransFatLB,
            VitaminA = VitaminALB,
            VitaminB6 = VitaminB6LB,
            VitaminB12 = VitaminB12LB,
            VitaminC = VitaminCLB,
            VitaminD = VitaminDLB,
            VitaminE = VitaminELB,
            Thiamin = ThiaminLB,
            Riboflavin = RiboflavinLB,
            Niacin = NiacinLB,
            Folate = FolateLB,
            Salt = SaltLB,
            Potassium = PotassiumLB,
            Magnesium = MagnesiumLB,
            Iron = IronLB,
            Zinc = ZincLB,
            Phosphorus = PhosphorusLB,
            Copper = CopperLB,
            Iodine = IodineLB,
            Selenium = SeleniumLB,
            Calcium = CalciumLB
        };

        var II = new NutrientTargets
        {
             Protein = ProteinII,
            Carbohydrates = CarbohydratesII,
            Sugars = SugarsII,
            Fibres = FibresII,
            TotalFat = TotalFatII,
            SaturatedFat = SaturatedFatII,
            MonounsaturatedFat = MonounsaturatedFatII,
            PolyunsaturatedFat = PolyunsaturatedFatII,
            TransFat = TransFatII,
            VitaminA = VitaminAII,
            VitaminB6 = VitaminB6II,
            VitaminB12 = VitaminB12II,
            VitaminC = VitaminCII,
            VitaminD = VitaminDII,
            VitaminE = VitaminEII,
            Thiamin = ThiaminII,
            Riboflavin = RiboflavinII,
            Niacin = NiacinII,
            Folate = FolateII,
            Salt = SaltII,
            Potassium = PotassiumII,
            Magnesium = MagnesiumII,
            Iron = IronII,
            Zinc = ZincII,
            Phosphorus = PhosphorusII,
            Copper = CopperII,
            Iodine = IodineII,
            Selenium = SeleniumII,
            Calcium = CalciumII
        };

        var UB = new NutrientTargets
        {
            Protein = ProteinUB,
            Carbohydrates = CarbohydratesUB,
            Sugars = SugarsUB,
            Fibres = FibresUB,
            TotalFat = TotalFatUB,
            SaturatedFat = SaturatedFatUB,
            MonounsaturatedFat = MonounsaturatedFatUB,
            PolyunsaturatedFat = PolyunsaturatedFatUB,
            TransFat = TransFatUB,
            VitaminA = VitaminAUB,
            VitaminB6 = VitaminB6UB,
            VitaminB12 = VitaminB12UB,
            VitaminC = VitaminCUB,
            VitaminD = VitaminDUB,
            VitaminE = VitaminEUB,
            Thiamin = ThiaminUB,
            Riboflavin = RiboflavinUB,
            Niacin = NiacinUB,
            Folate = FolateUB,
            Salt = SaltUB,
            Potassium = PotassiumUB,
            Magnesium = MagnesiumUB,
            Iron = IronUB,
            Zinc = ZincUB,
            Phosphorus = PhosphorusUB,
            Copper = CopperUB,
            Iodine = IodineUB,
            Selenium = SeleniumUB,
            Calcium = CalciumUB
        };

        return (LB, II, UB);
    }
    
    public static (float, float, float) CalculateProtein(int age, Gender gender, int weight)
    {
        float LB;
        float II;
        float UB;
        if(age >= 65)
        {
            LB = weight * 1.1f;
            II = weight * 1.2f;
            UB = weight * 1.3f;
        }
        else 
        {
            LB = weight * 0.8f;
            II = weight * 1.25f;
            UB = weight * 1.5f;
        }

        return (LB, II, UB);
    }

    public static (float, float, float) CalculateCarbohydrates(int age, Gender gender, float kcalPerDay)
    {
        float LB = kcalPerDay / 4 * 0.45f;
        float II = kcalPerDay / 4 * 0.525f;
        float UB = kcalPerDay / 4 * 0.6f;
        return (LB, II, UB);
    }

    public static (float, float, float) CalculateSugars(int age, Gender gender, float kcalPerDay)
    {
        var UB = kcalPerDay / 4 * 0.1f;
        return (0f, 0f, UB);
    }

    public static (float, float, float) CalculateFibres(int age, Gender gender, float kcalPerDay)
    {
        float MegaJoulePerDay = kcalPerDay * MJPerKcal;
        float LB;
        float II;
        float UB;
        if (age < 18)
        {
            LB = MegaJoulePerDay * 2;
            II = MegaJoulePerDay * 2.5f;
            UB = MegaJoulePerDay * 3;
        }
        else
        {
            if(gender == Gender.Male) LB = 35f;
            else LB = 25f;
            II = MegaJoulePerDay * 3;
            UB = float.PositiveInfinity;
        }
        return (LB, II, UB);
    }

    public static (float, float, float) CalculateTotalFat(int age, Gender gender, float kcalPerDay)
    {
        float LB = kcalPerDay / 9 * 0.25f;
        float II = kcalPerDay / 9 * 0.325f;
        float UB = kcalPerDay / 9 * 0.4f;
        return (LB, II, UB);
    }

    public static (float, float, float) CalculateSaturatedFat(int age, Gender gender, float kcalPerDay)
    {
        return (0f, 0f, kcalPerDay / 9 * 0.1f);
    }

    public static (float, float, float) CalculateMonounsaturatedFat(int age, Gender gender, float kcalPerDay)
    {
        float LB = kcalPerDay / 9 * 0.1f;
        float II = kcalPerDay / 9 * 0.15f;
        float UB = kcalPerDay / 9 * 0.2f;
        return (LB, II, UB);
    }

    public static (float, float, float) CalculatePolyunsaturatedFat(int age, Gender gender, float kcalPerDay)
    {
        float LB = kcalPerDay / 9 * 0.05f;
        float II = kcalPerDay / 9 * 0.075f;
        float UB = kcalPerDay / 9 * 0.1f;
        return (LB, II, UB);
    }

    public static (float, float, float) CalculateTransFat(int age, Gender gender, float kcalPerDay)
    {
        return (0f, 0f, kcalPerDay / 9 * 0.01f);
    }

    public static (float, float, float) CalculateVitaminA(int age, Gender gender, int weight)
    {
        float LB;
        float II;
        float UB;

        if (age <=5)
        {
            LB = 0f;
            II = 350f;
            UB = float.PositiveInfinity;
        }
        else if (age >= 6 && age <= 9)
        {
            LB = 0f;
            II = 400f;
            UB = float.PositiveInfinity;
        }
        else if (age >= 10 && age <= 13)
        {
            LB = 0f;
            II = 600f;
            UB = float.PositiveInfinity;
        }
        else if (gender == Gender.Female)
        {
            LB = 400f;
            II = 700f;
            UB = 1500f;
        }
        else
        {
            LB = 500f;
            II = 900f;
            UB = float.PositiveInfinity;
        }

        return (LB, II, UB);
    }

    public static (float, float, float) CalculateVitaminB6(int age, Gender gender, int weight)
    {
        float LB;
        float II;
        float UB;

        if (age <=5)
        {
            LB = 0f;
            II = 0.7f;
            UB = float.PositiveInfinity;
        }
        else if (age >= 6 && age <= 9)
        {
            LB = 0f;
            II = 1.0f;
            UB = float.PositiveInfinity;
        }
        else if (age >= 10 && age <= 13)
        {
            LB = 0f;
            II = gender == Gender.Female ? 1.1f : 1.3f;
            UB = float.PositiveInfinity;
        }
        else if (gender == Gender.Female)
        {
            LB = 0.8f;
            II = 1.2f;
            UB = 25f;
        }
        else
        {
            LB = 1.0f;
            II = 1.5f;
            UB = 25;
        }

        return (LB, II, UB);
    }

    public static (float, float, float) CalculateVitaminB12(int age, Gender gender, int weight)
    {
        float LB;
        float II;
        float UB = float.PositiveInfinity;

        if (age <=5)
        {
            LB = 0f;
            II = 0.8f;
        }
        else if (age >= 6 && age <= 9)
        {
            LB = 0f;
            II = 1.3f;
        }
        else if (age >= 10 && age <= 13)
        {
            LB = 0f;
            II = 2.0f;
        }
        else if (gender == Gender.Female)
        {
            LB = 1.0f;
            II = 2.0f;
        }
        else
        {
            LB = 1.0f;
            II = 2.0f;
            UB = float.PositiveInfinity;
        }

        return (LB, II, UB);
    }

    public static (float, float, float) CalculateVitaminC(int age, Gender gender, int weight)
    {
        float LB;
        float II;
        float UB = float.PositiveInfinity;

        if (age <=5)
        {
            LB = 0f;
            II = 30.0f;
        }
        else if (age >= 6 && age <= 9)
        {
            LB = 0f;
            II = 40.0f;
        }
        else if (age >= 10 && age <= 13)
        {
            LB = 0f;
            II = 50.0f;
        }
        else
        {
            LB = 10.0f;
            II = 75.0f;
            UB = float.PositiveInfinity;
        }

        return (LB, II, UB);
    }

    public static (float, float, float) CalculateVitaminD(int age, Gender gender, int weight)
    {
        float LB = 2.5f;
        float II;
        float UB = 100.0f;

        if (age >= 75) II = 20.0f;
        else II = 10.0f;

        return (LB, II, UB);
    }

    public static (float, float, float) CalculateVitaminE(int age, Gender gender, int weight)
    {
        float LB;
        float II;
        float UB;

        if (age <=5)
        {
            LB = 0f;
            II = 5f;
            UB = float.PositiveInfinity;
        }
        else if (age >= 6 && age <= 9)
        {
            LB = 0f;
            II = 6f;
            UB = float.PositiveInfinity;
        }
        else if (age >= 10 && age <= 13)
        {
            LB = 0f;
            II = gender == Gender.Female ? 7f : 8f;
            UB = float.PositiveInfinity;
        }
        else if (gender == Gender.Female)
        {
            LB = 3f;
            II = 8f;
            UB = 300f;
        }
        else
        {
            LB = 4f;
            II = 10f;
            UB = 300;
        }

        return (LB, II, UB);
    }

    public static (float, float, float) CalculateThiamin(int age, Gender gender, float kcalPerDay)
    {
        float LB;
        float II;
        float UB = float.PositiveInfinity;

        if (age <= 5)
        {
            LB = 0f;
            II = 0.6f;
        }
        else if (age >= 6 && age <= 9)
        {
            LB = 0f;
            II = 0.9f;
        }
        else if (age >= 10 && age <= 13)
        {
            LB = 0f;
            II = gender == Gender.Female ? 1.0f : 1.2f;
        }
        else 
        {
            II = gender == Gender.Female ? 1.1f : 1.4f;
            if (age >= 65)
            {
                LB = 1.0f;
            }
            else if (kcalPerDay < (8 / MJPerKcal))
            {
                LB = 0.8f;
            }
            else
            {
                LB = gender == Gender.Female ? 0.5f : 0.6f;
            }
        }

        return (LB, II, UB);
    }

    public static (float, float, float) CalculateRiboflavin(int age, Gender gender, int weight)
    {
        float LB;
        float II;
        float UB;

        if (age <=5)
        {
            LB = 0f;
            II = 0.7f;
            UB = float.PositiveInfinity;
        }
        else if (age >= 6 && age <= 9)
        {
            LB = 0f;
            II = 1.1f;
            UB = float.PositiveInfinity;
        }
        else if (age >= 10 && age <= 13)
        {
            LB = 0f;
            II = gender == Gender.Female ? 1.2f : 1.4f;
            UB = float.PositiveInfinity;
        }
        else if (gender == Gender.Female)
        {
            LB = 0.8f;
            II = 1.3f;
            UB = float.PositiveInfinity;
        }
        else
        {
            LB = 0.8f;
            II = 1.7f;
            UB = float.PositiveInfinity;
        }

        return (LB, II, UB);
    }

    public static (float, float, float) CalculateNiacin(int age, Gender gender, float kcalPerDay)
    {
        float LB;
        float II;
        float UB;

        if (age <= 5)
        {
            LB = 0f;
            II = 9f;
            UB = float.PositiveInfinity;
        }
        else if (age >= 6 && age <= 9)
        {
            LB = 0f;
            II = 12f;
            UB = float.PositiveInfinity;
        }
        else if (age >= 10 && age <= 13)
        {
            LB = 0f;
            II = gender == Gender.Female ? 14f : 16f;
            UB = float.PositiveInfinity;
        }
        else 
        {
            II = gender == Gender.Female ? 15f : 18f;
            UB = 35f;

            if (kcalPerDay < (8 / MJPerKcal)) //Intake of less than 8 MJ per day
            {
                LB = 8f;
            }
            else
            {
                LB = gender == Gender.Female ? 9f : 12f;
            }
        }

        return (LB, II, UB);
    }

    public static (float, float, float) CalculateFolate(int age, Gender gender, int weight)
    {
        float LB;
        float II;
        float UB = float.PositiveInfinity;

        if (age <= 5)
        {
            LB = 0f;
            II = 80f;
        }
        else if (age >= 6 && age <= 9)
        {
            LB = 0f;
            II = 130f;
        }
        else if (age >= 10 && age <= 13)
        {
            LB = 0f;
            II = 200f;
        }
        else if (gender == Gender.Female && age < 50 )
        {
            LB = 100f;
            II = 400f;
        }
        else
        {
            LB = 100f;
            II = 300f;
        }

        return (LB, II, UB);
    }

    public static (float, float, float) CalculateSalt(int age, Gender gender, int weight)
    {
        throw new NotImplementedException();
    }

    public static (float, float, float) CalculatePotassium(int age, Gender gender, int weight)
    {
        float LB;
        float II;
        float UB = float.PositiveInfinity;

        if (age <= 5)
        {
            LB = 0f;
            II = 1.8f;
        }
        else if (age >= 6 && age <= 9)
        {
            LB = 0f;
            II = 2.0f;
        }
        else if (age >= 10 && age <= 13)
        {
            LB = 0f;
            II = gender == Gender.Female ? 2.9f : 3.3f;
        }
        else
        {
            LB = 1.6f;
            II = gender == Gender.Female ? 3.1f : 3.5f; 
        }

        return (LB, II, UB);
    }

    public static (float, float, float) CalculateMagnesium(int age, Gender gender, int weight)
    {
        float LB = 0f;
        float II;
        float UB = float.PositiveInfinity;

        if (age <= 5)
        {
            II = 120f;
        }
        else if (age >= 6 && age <= 9)
        {
            II = 200f;
        }
        else if (age >= 10 && age <= 13)
        {
            II = 280f;
        }
        else
        {
            II = gender == Gender.Female ? 280f : 350f; 
        }

        return (LB, II, UB);
    }

    public static (float, float, float) CalculateIron(int age, Gender gender, int weight)
    {
        float LB;
        float II;
        float UB;

        if (age <= 5)
        {
            LB = 0f;
            II = 8f;
            UB = float.PositiveInfinity;
        }
        else if (age >= 6 && age <= 9)
        {
            LB = 0f;
            II = 9f;
            UB = float.PositiveInfinity;
        }
        else if (age >= 10 && age <= 13)
        {
            LB = 0f;
            II = 11f;
            UB = float.PositiveInfinity;
        }
        else if (gender == Gender.Female && age >= 51)
        {
            LB = 5f;
            II = 9f;
            UB = 60f;
        }
        else
        {
            LB = 7f;
            UB = 60f;
            II = gender == Gender.Female ? 15f : 9f;
        }

        return (LB, II, UB);
    }

    public static (float, float, float) CalculateZinc(int age, Gender gender, int weight)
    {
        float LB;
        float II;
        float UB = float.PositiveInfinity;

        if (age <= 5)
        {
            LB = 0f;
            II = 6f;
        }
        else if (age >= 6 && age <= 9)
        {
            LB = 0f;
            II = 7f;
        }
        else if (age >= 10 && age <= 13)
        {
            LB = 0f;
            II = gender == Gender.Female ? 8f : 11f;
        }
        else if (gender == Gender.Female)
        {
            LB = 4f;
            II = 7f; 
        }
        else
        {
            LB = 5f;
            II = 9f;
        }

        return (LB, II, UB);
    }

    public static (float, float, float) CalculatePhosphorus(int age, Gender gender, int weight)
    {
        float LB;
        float II;
        float UB;

        if (age <= 5)
        {
            LB = 0f;
            II = 470f;
            UB = float.PositiveInfinity;
        }
        else if (age >= 6 && age <= 9)
        {
            LB = 0f;
            II = 540f;
            UB = float.PositiveInfinity;
        }
        else if (age >= 10 && age <= 13)
        {
            LB = 0f;
            II = 700f;
            UB = float.PositiveInfinity;
        }
        else
        {
            LB = 300f;
            II = 600f;
            UB = 3000f;
        }

        return (LB, II, UB);
    }

    public static (float, float, float) CalculateCopper(int age, Gender gender, int weight)
    {
        float LB;
        float II;
        float UB;

        if (age <= 5)
        {
            LB = 0f;
            II = 0.4f;
            UB = float.PositiveInfinity;
        }
        else if (age >= 6 && age <= 9)
        {
            LB = 0f;
            II = 0.5f;
            UB = float.PositiveInfinity;
        }
        else if (age >= 10 && age <= 13)
        {
            LB = 0f;
            II = 0.7f;
            UB = float.PositiveInfinity;
        }
        else
        {
            LB = 0.4f;
            II = 0.9f;
            UB = 5f;
        }

        return (LB, II, UB);
    }

    public static (float, float, float) CalculateIodine(int age, Gender gender, int weight)
    {
        float LB;
        float II;
        float UB;

        if (age <= 5)
        {
            LB = 0f;
            II = 90f;
            UB = float.PositiveInfinity;
        }
        else if (age >= 6 && age <= 9)
        {
            LB = 0f;
            II = 150f;
            UB = float.PositiveInfinity;
        }
        else if (age >= 10 && age <= 13)
        {
            LB = 0f;
            II = 120f;
            UB = float.PositiveInfinity;
        }
        else
        {
            LB = 70f;
            II = 150f;
            UB = 600f;
        }

        return (LB, II, UB);
    }

    public static (float, float, float) CalculateSelenium(int age, Gender gender, int weight)
    {
        float LB;
        float II;
        float UB;

        if (age <= 5)
        {
            LB = 0f;
            II = 25f;
            UB = float.PositiveInfinity;
        }
        else if (age >= 6 && age <= 9)
        {
            LB = 0f;
            II = 30f;
            UB = float.PositiveInfinity;
        }
        else if (age >= 10 && age <= 13)
        {
            LB = 0f;
            II = 40f;
            UB = float.PositiveInfinity;
        }
        else
        {
            LB = 20f;
            II = gender == Gender.Female ? 50 : 60;
            UB = 300f;
        }

        return (LB, II, UB);
    }

    public static (float, float, float) CalculateCalcium(int age, Gender gender, int weight)
    {
        float LB;
        float II;
        float UB;

        if (age <= 5)
        {
            LB = 0f;
            II = 600f;
            UB = float.PositiveInfinity;
        }
        else if (age >= 6 && age <= 9)
        {
            LB = 0f;
            II = 700f;
            UB = float.PositiveInfinity;
        }
        else if (age >= 10 && age <= 13)
        {
            LB = 0f;
            II = 900f;
            UB = float.PositiveInfinity;
        }
        else
        {
            LB = 400f;
            II = 800f;
            UB = 2500f;
        }

        return (LB, II, UB);
    }
}