namespace server.Core.Services;  

public static class FoodItemSeeding
{
    public static void Seed()
    {
        using(var reader = new StreamReader(@"../../../../data.csv"))
        {
            List<string> listA = new List<string>();
            List<string> listB = new List<string>();
            reader.ReadLine();//Skip first line
            while (!reader.EndOfStream)
            {
                string line = reader.ReadLine()!;
                string[] values = line.Split(';');
                Console.WriteLine($"Name: {values[1]}; Energy: {values[4]} kcal/100g; Protein: {values[5]}g/100g");
                /*
                FoodItemCreateDTO foodItem = new FoodItemCreateDTO
                {
                    Name = values[1],

                };
                */
            }
        }
    }
}