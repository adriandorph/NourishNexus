namespace server.Services.MealPlan;

public class MealPlan
{
    private List<Day> days;
    public List<Day> Days 
    {
        get
        {
            return days;
        }
        set
        {
            if (value.Count != 7) throw new Exception($"There can only be 7 days. Tried to update to {value.Count} days");
            days = value;
        }
    }


    public MealPlan()
    {
        days = new List<Day>();
        for (int i = 0; i<7; i++)
        {
            days.Add(new Day());
        }
    }

    public NutrientTargets CalculateNutrientSums()
    {
        var nutrientSums = new NutrientTargets();

        foreach(var day in this.Days)
        {
            NutrientTargets sumBreakfast = day.Breakfast != null ? day.Breakfast.CalculateNutrientSums() : new NutrientTargets();
            NutrientTargets sumLunch = day.Lunch != null ? day.Lunch.CalculateNutrientSums() : new NutrientTargets();
            NutrientTargets sumDinner = day.Dinner != null ? day.Dinner.CalculateNutrientSums() : new NutrientTargets();
            NutrientTargets sumSnacks = day.Snacks != null ? day.Snacks.CalculateNutrientSums() : new NutrientTargets();
            nutrientSums += sumBreakfast + sumLunch + sumDinner + sumSnacks;
        }

        return nutrientSums;
    }
}

public class Day
{
    private PlannedMeal? breakfast;
    public PlannedMeal? Breakfast 
    {
        get {return breakfast;}
        set 
        {
            if (!BreakfastLocked) breakfast = value;
            else throw new Exception("Breakfast was locked!");
        }
    }
    public bool BreakfastLocked{get; set;} = false;
    private PlannedMeal? lunch;
    public PlannedMeal? Lunch 
    {
        get {return lunch;}
        set 
        {
            if (!LunchLocked) lunch = value;
            else throw new Exception("Lunch was locked!");
        }
    }
    public bool LunchLocked{get; set;} = false;

    
    private PlannedMeal? dinner;
    public PlannedMeal? Dinner
    {
        get {return dinner;}
        set 
        {
            if (!DinnerLocked) dinner = value;
            else throw new Exception("Dinner was locked!");
        }
    }
    public bool DinnerLocked{get; set;} = false;
    

    private PlannedMeal? snacks;
    public PlannedMeal? Snacks
    {
        get {return snacks;}
        set 
        {
            if (!SnacksLocked) snacks = value;
            else throw new Exception("Snacks was locked!");
        }
    }
    public bool SnacksLocked{get; set;} = false;
    public Day(PlannedMeal? breakfast, PlannedMeal? lunch, PlannedMeal? dinner, PlannedMeal snacks){
        this.Breakfast = breakfast;
        this.Lunch = lunch;
        this.Dinner = dinner;
        this.Snacks = snacks;
    }
    public Day(){}
}