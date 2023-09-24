namespace CalorieTracker;

public class SessionCommandTests
{
    Terminal _terminal;
    SessionCommand _sessionCommand;

    [SetUp]
    public void Setup()
    {
        _terminal = new Terminal();
        _sessionCommand = new SessionCommand();
        _terminal.AddCommand(_sessionCommand);
    }


    [Test]
    public void Execute_With_Start_Argument_Start_Session()
    {
        _terminal.Run("session start");

        var session = SessionManager.Load();
        var isSessionOpened = session.Status;
        Assert.That(isSessionOpened, Is.True);

        SessionManager.Close();
    }

    [Test]
    public void Execute_With_Stop_Argument_Close_Session()
    {
        SessionManager.Start();
        _terminal.Run("session close");

        var session = SessionManager.Load();
        var isSessionOpened = session.Status;
        Assert.That(isSessionOpened, Is.False);
    }

}
