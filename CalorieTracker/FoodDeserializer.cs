using System.Text.Json;

namespace CalorieTracker;

public class FoodDeserializer
{
    public static Food Read(string path)
    {
        if (!File.Exists(path))
            throw new FileNotFoundException($"Json file could not found at path: {path}");

        var jsonText = File.ReadAllText(path);
        var food = JsonSerializer.Deserialize<Food>(jsonText);

        if (food == null)
            throw new InvalidOperationException("Food deserialization failed!");

        return food;
    }
    public static bool IsValid(string path)
    {
        if (!File.Exists(path))
            return false;

        var jsonText = File.ReadAllText(path);
        try
        {
            var food = JsonSerializer.Deserialize<Food>(jsonText);
            if (food == null)
                return false;

            var isPropertiesValid = food.Name != string.Empty && food.Calorie > 0;
            return isPropertiesValid;
        }
        catch (Exception e)
        {
            return false;
        }
    }
}
