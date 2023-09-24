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
                actualFood.Calorie = food.Calorie;
                actualFood.Carb = food.Carb;
                actualFood.Protein = food.Protein;
                actualFood.Fat = food.Fat;
                actualFood.WeightType = food.WeightType;
                break;
            default:
                throw new InvalidOperationException($"{food.Name} has an undefined weight type.");
        }


        var resultText = actualFood.ToString();
        var session = SessionManager.Load();

        using var writer = new StreamWriter(session.SavePath, true);
        writer.WriteLine(resultText);
    }
    public void Execute()
    {
        throw new TerminalArgumentException
            ("add command needs an argument as food, usage: add [food_name]");
    }
    public void Undo()
    {
        throw new NotImplementedException();
    }
}
