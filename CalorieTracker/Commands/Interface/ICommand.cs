namespace CalorieTracker;

public interface ICommand
{
    string Name { get; }
    bool Undoable { get; }
    void Execute(params string[] arguments);
    void Execute();
    void Undo();
}
