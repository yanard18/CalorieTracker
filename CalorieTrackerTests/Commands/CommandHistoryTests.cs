namespace CalorieTracker;

[TestFixture]
public class CommandHistoryTests
{
    readonly TestCommand _command = new TestCommand();


    [Test]
    public void Push_Add_Command_Onto_Top_Of_History()
    {
        CommandHistory.Push(_command);

        var commandOnQueue = CommandHistory.History.Dequeue();
        Assert.That(commandOnQueue, Is.EqualTo(_command));
    }

    [Test]
    public void Pop_Get_Last_Command_And_Remove_From_History()
    {
        CommandHistory.History.Enqueue(_command);

        var poppedCommand = CommandHistory.Pop();

        Assert.That(poppedCommand, Is.EqualTo(_command));
    }

    [Test]
    public void Pop_With_Empty_History_Return_Null_Command()
    {
        var command = CommandHistory.Pop();
        
        Assert.That(command, Is.TypeOf<NullCommand>());
    }

    [TearDown]
    public void Teardown()
    {
        CommandHistory.Clear();
    }
}
