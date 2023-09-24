namespace CalorieTracker;

public static class FoodContainer
{
    public static readonly Dictionary<string, Food> Foods = new Dictionary<string, Food>();

    public static void Init()
    {
        var jsonFiles = Directory.GetFiles("Json", "*.json");
        foreach (var file in jsonFiles)
        {
            if (!FoodDeserializer.IsValid(file))
                continue;
            
            var food = FoodDeserializer.Read(file);
            Foods.Add(food.Name, food);
        }
    }

    public static void Destroy() => Foods.Clear();

}
