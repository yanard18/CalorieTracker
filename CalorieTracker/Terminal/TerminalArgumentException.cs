namespace CalorieTracker;

public class TerminalArgumentException : Exception
{
    public TerminalArgumentException() { }

    public TerminalArgumentException(string message)
        : base(message) { }

    public TerminalArgumentException(string message, Exception innerException)
        : base(message, innerException) { }
}
