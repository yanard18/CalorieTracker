using System.Text.Json.Serialization;

namespace CalorieTracker;

public class Food
{
    [JsonConverter(typeof(JsonStringEnumConverter))]
    public enum EWeightType
    {
        PER100G,
        ONE
    }

    public string Name { get; set; }

    public float Calorie { get; set; }

    public float Carb { get; set; }

    public float Fat { get; set; }

    public float Protein { get; set; }


    [JsonConverter(typeof(JsonStringEnumConverter))]
    public EWeightType WeightType { get; set; }


    public override bool Equals(object? obj)
    {
        if (obj == null || GetType() != obj.GetType())
            return false;

        var otherFood = (Food)obj;
        return
            string.Equals(Name, otherFood.Name) &&
            Math.Abs(Calorie - otherFood.Calorie) < .01f &&
            Math.Abs(Carb - otherFood.Carb) < .01f &&
            Math.Abs(Fat - otherFood.Fat) < .01f &&
            Math.Abs(Protein - otherFood.Protein) < .01f &&
            Equals(WeightType, otherFood.WeightType);
    }

    public override string ToString()
    {
        return $"Name: {Name} | Calorie: {Calorie} | Carb: {Carb} | Protein: {Protein} | Fat: {Fat}";
    }

}
