namespace CalorieTracker;

public class NonUndoableTestCommand : ICommand
{
    public string Name => "non-undoable";
    public bool Undoable => false;
    public void Execute(params string[] arguments) { }
    public void Execute() { }
    public void Undo() { }
}
