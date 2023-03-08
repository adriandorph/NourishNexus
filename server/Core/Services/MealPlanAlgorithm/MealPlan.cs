namespace server.Core.Services.MealPlan;

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

        public class Day
        {
            MealCreateDTO? Breakfast {get; set;}
            MealCreateDTO? Lunch {get; set;}
            MealCreateDTO? Dinner {get; set;}
            MealCreateDTO? Snacks {get; set;}
            public Day(MealCreateDTO breakfast, MealCreateDTO lunch, MealCreateDTO dinner, MealCreateDTO snacks){
                this.Breakfast = breakfast;
                this.Lunch = lunch;
                this.Dinner = dinner;
                this.Snacks = snacks;
            }
            public Day(){}
        }
    }