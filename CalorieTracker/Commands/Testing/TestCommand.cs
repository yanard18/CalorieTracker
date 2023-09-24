namespace CalorieTracker;

public class TestCommand : ICommand
{
    public bool IsInvoked { get; private set; }
    public bool IsUndoInvoked { get; private set; }

    public string[] Arguments;
    public string Name => "Test";
    public bool Undoable => true;
    public void Execute(params string[] arguments)
    {
        Arguments = arguments;
    }
    public void Execute()
    {
        IsInvoked = true;
    }
    public void Undo()
    {
        IsUndoInvoked = true;
    }
}
