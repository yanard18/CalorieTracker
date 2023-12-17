using System.Text.Json;

namespace CalorieTracker;

public class AddCommand : ICommand
{

    public string Name => "add";
    public bool Undoable => true;

    public void Execute(params string[] arguments)
    {
        var foodName = arguments[0];
        var amountString = arguments[1];
        var amount = int.Parse(amountString);

        if (!FoodContainer.Foods.ContainsKey(foodName))
            throw new TerminalArgumentException("invalid food name");

        var food = FoodContainer.Foods[foodName];
        var actualFood = new Food();
        switch (food.WeightType)
        {
            case Food.EWeightType.PER100G:
                actualFood.Name = food.Name;
                actualFood.Calorie = food.Calorie / 100f * amount;
                actualFood.Carb = food.Carb / 100f * amount;
                actualFood.Protein = food.Protein / 100f * amount;
                actualFood.Fat = food.Fat / 100f * amount;
                actualFood.WeightType = food.WeightType;
                break;
            case Food.EWeightType.ONE:
                actualFood.Name = food.Name;
                actualFood.Calorie = food.Calorie * amount;
                actualFood.Carb = food.Carb * amount;
                actualFood.Protein = food.Protein * amount;
                actualFood.Fat = food.Fat * amount;
                actualFood.WeightType = food.WeightType;
                break;
            default:
                throw new InvalidOperationException($"{food.Name} has an undefined weight type.");
        }


        var resultText = JsonSerializer.Serialize(actualFood);
        //var resultText = actualFood.ToString();
        var session = SessionManager.Load();

        using var writer = new StreamWriter(session.SavePath, true);
        writer.WriteLine(resultText);
    }
    public void Execute()
    {
        const string message = "add command needs arguments, usage: add [food_name] [amount]";
        Terminal.Log(message, ConsoleColor.Red);
    }
    public void Undo()
    {
        throw new NotImplementedException();
    }
}
