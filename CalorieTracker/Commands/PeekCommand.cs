using System.Text.Json;

namespace CalorieTracker;

public class PeekCommand : ICommand
{

    public string Name => "peek";
    public bool Undoable => false;
    public void Execute(params string[] arguments)
    {
        Execute();
    }
    public void Execute()
    {
        var session = SessionManager.Load();
        var savePath = session.SavePath;

        var jsonLine = File.ReadAllLines(savePath);
        var foods = jsonLine.Select(line => JsonSerializer.Deserialize<Food>(line));
        var result = FoodCalculator.Calculate(foods);
        
        Terminal.Log(result, ConsoleColor.Green);
    }
    public void Undo()
    {
    }
}
