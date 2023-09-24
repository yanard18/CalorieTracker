namespace CalorieTracker;

public class UndoCommand : ICommand
{

    public string Name => "undo";
    public bool Undoable => false;
    public void Execute(params string[] arguments) { }
    public void Execute()
    {
        var command = CommandHistory.Pop();
        command.Undo();
    }
    public void Undo() { }
}
