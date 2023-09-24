namespace CalorieTracker;

public class NullCommand : ICommand
{

    public string Name => "null";
    public bool Undoable => false;
    public void Execute(params string[] arguments) { }
    public void Execute() { }
    public void Undo() { }
}
