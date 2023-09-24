using CalorieTracker;

FoodContainer.Init();
var terminal = new Terminal();
terminal.AddCommand(new AddCommand());
terminal.AddCommand(new SessionCommand());

while (true)
{
    var input = Console.ReadLine();
    if (input != null) terminal.Run(input);
}
