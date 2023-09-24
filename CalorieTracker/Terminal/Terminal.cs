using System.Globalization;

namespace CalorieTracker;

public class Terminal
{
    public Dictionary<string, ICommand> Commands { get; } = new Dictionary<string, ICommand>();

    public void AddCommand(ICommand command)
    {
        Commands.Add(command.Name.ToLower(CultureInfo.InvariantCulture), command);
    }
    public void Run(string input)
    {
        var inputAsLowerCase = input.ToLower(CultureInfo.InvariantCulture);
        var commandName = GetCommand(inputAsLowerCase);
        
        if (!Commands.ContainsKey(commandName))
            throw new InvalidOperationException("Command is invalid!");

        var command = Commands[commandName];

        var arguments = GetArguments(inputAsLowerCase);
        if (arguments.Length == 0)
            command.Execute();
        else
            command.Execute(arguments);

        if (command.Undoable)
            CommandHistory.Push(command);
    }

    static string GetCommand(string input)
    {
        var parsedInput = input.Split(' ');
        return parsedInput[0];
    }
    static string[] GetArguments(string input)
    {
        var parsedInput = input.Split(' ');
        return parsedInput.Skip(1).ToArray();
    }

}
