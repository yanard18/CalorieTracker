namespace CalorieTracker;

public class SessionCommand : ICommand
{

    public string Name => "session";
    public bool Undoable => false;
    public void Execute(params string[] arguments)
    {
        var arg0 = arguments[0];
        switch (arg0)
        {
            case "start":
                SessionManager.Start();
                break;
            case "close":
                SessionManager.Close();
                break;
        }
    }
    public void Execute()
    {
        const string message = "usage: session start | session close";
        Terminal.Log(message, ConsoleColor.Red);
    }
    public void Undo() { }
}
