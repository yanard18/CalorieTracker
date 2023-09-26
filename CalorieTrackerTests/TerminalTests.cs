namespace CalorieTracker;

[TestFixture]
public class TerminalTests
{
    Terminal _terminal = new Terminal();
    TestCommand _testCommand = new TestCommand();

    [SetUp]
    public void Setup()
    {
        _terminal = new Terminal();
        _testCommand = new TestCommand();
        _terminal.AddCommand(_testCommand);
    }
    [Test]
    public void Add_Command_Register_Command()
    {
        var testTerminal = new Terminal();
        ICommand testCommand = new TestCommand();

        testTerminal.AddCommand(testCommand);

        var isCommandRegistered = testTerminal.Commands.ContainsValue(testCommand);
        Assert.That(isCommandRegistered, Is.True);
    }

    [Test]
    public void Run_Undefined_Command_Does_Not_Throw_Exception()
    {
        Assert.DoesNotThrow(() => _terminal.Run("invalid"));
    }

    [Test]
    public void Run_With_Single_Argument_Parse_Arguments()
    {
        _terminal.Run("test a0");

        Assert.That(_testCommand.Arguments[0], Is.EqualTo("a0"));
    }
    [Test]
    public void Run_With_Multiple_Arguments_Parse_Arguments()
    {
        _terminal.Run("test a0 a1");

        Assert.That(_testCommand.Arguments[0], Is.EqualTo("a0"));
        Assert.That(_testCommand.Arguments[1], Is.EqualTo("a1"));
    }

    [Test]
    public void Run_With_Valid_Command_Execute_Command()
    {
        _terminal.Run("Test");

        var isInvoked = _testCommand.IsInvoked;
        Assert.That(isInvoked, Is.True);
    }

    [Test]
    public void Run_Add_Command_To_History_When_Command_Undoable()
    {
        var nonUndoableCommand = new NonUndoableTestCommand();
        _terminal.AddCommand(nonUndoableCommand);

        _terminal.Run("non-undoable");
        _terminal.Run("Test");

        var testCommandInHistory = CommandHistory.Pop();
        var nonUndoableCommandInHistory = CommandHistory.Pop();
        Assert.That(testCommandInHistory, Is.EqualTo(_testCommand));
        Assert.That(nonUndoableCommandInHistory, Is.Not.EqualTo(nonUndoableCommand));
    }

    [Test]
    public void Terminal_Not_Case_Sensitive()
    {
        _terminal.Run("test");

        var isInvoked = _testCommand.IsInvoked;
        Assert.That(isInvoked, Is.True);
    }


    [TearDown]
    public void Teardown()
    {
        CommandHistory.Clear();
    }


}
