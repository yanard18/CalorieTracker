namespace CalorieTracker;

[TestFixture]
public class UndoCommandTests
{
    readonly UndoCommand _undoCommand = new UndoCommand();
    
    [Test]
    public void Name_Property_Return_Undo()
    {
        var name = _undoCommand.Name;
        
        Assert.That(name, Is.EqualTo("undo"));
    }

    [Test]
    public void Execute_Undo_Last_Command_From_History()
    {
        var testCommand = new TestCommand();
        CommandHistory.Push(testCommand);
        
        _undoCommand.Execute();
        
        Assert.That(testCommand.IsUndoInvoked);
    }
    
    
}
