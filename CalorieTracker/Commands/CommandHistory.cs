namespace CalorieTracker;

public static class CommandHistory
{
    public static Queue<ICommand> History { get; } = new Queue<ICommand>();

    public static void Push(ICommand command)
    {
        History.Enqueue(command);
    }
    public static ICommand Pop()
    {
        return History.Count == 0 ? new NullCommand() : History.Dequeue();
    }
    public static void Clear()
    {
        History.Clear();
    }
}
