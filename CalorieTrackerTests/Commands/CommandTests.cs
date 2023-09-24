namespace CalorieTracker;

public class CommandTests
{
    TestCommand _command = new TestCommand();

    [SetUp]
    public void Setup()
    {
        _command = new TestCommand();
    }

    [Test]
    public void Execute_Invoked()
    {
        _command.Execute();

        Assert.That(_command.IsInvoked, Is.True);
    }

    [Test]
    public void Execute_With_One_Argument_Invoke()
    {
        var args = new[]
        {
            
            "a0", "a1"
        };

        _command.Execute(args);

        Assert.That(_command.Arguments[1], Is.EqualTo("a1"));

    }

    [Test]
    public void Undo_Invoked()
    {
        _command.Undo();

        Assert.That(_command.IsUndoInvoked, Is.True);

    }

    [Test]
    public void Name_Property_Returns_Name()
    {
        var name = _command.Name;

        Assert.That(name, Is.EqualTo("Test"));
    }

}
