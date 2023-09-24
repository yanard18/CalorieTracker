using System.Text.Json;

namespace CalorieTracker;

[TestFixture]
public class SessionTests
{
    [SetUp]
    public void Setup()
    {
        SessionManager.Start();
    }
    
    [TearDown]
    public void Teardown()
    {
        SessionManager.Close();
    }
    
    
    [Test]
    public void Load_Session_Should_Deserialize_Json()
    {
        var expectedSession = LoadSessionFromJson();

        var session = SessionManager.Load();

        Assert.That(session, Is.EqualTo(expectedSession));
    }


    [Test]
    public void Start_Modify_Session_Status_To_True()
    {
        var session = LoadSessionFromJson();
        Assert.That(session.Status, Is.True);
    }

    [Test]
    public void Start_If_Already_Started_Throw_Exception()
    {
        Assert.That(SessionManager.Start,
            Throws.TypeOf<InvalidOperationException>());
    }

    [Test]
    public void Close_Write_Result_To_Text_File()
    {
        var session = SessionManager.Load();
        File.WriteAllText(session.SavePath, "this is a test message");

        SessionManager.Close();

        var expectedFilePath = $"Out/{DateTime.Now:yyyy-MM-dd_hh-mm-ss}.txt";
        var isResultTextFileExist = File.Exists(expectedFilePath);
        var actualText = File.ReadAllText(expectedFilePath);
        Assert.Multiple(() =>
        {
            Assert.That(isResultTextFileExist, Is.True);
            Assert.That(actualText, Is.EqualTo("this is a test message"));
        });
        
        // Clean the result file
        if(File.Exists(expectedFilePath))
            File.Delete(expectedFilePath);
    }

    [Test]
    public void Start_Create_Temp_Text_File()
    {
        var activeSession = LoadSessionFromJson();
        var savePath = activeSession.SavePath;
        var isSaveFileExist = File.Exists(savePath);
        Assert.That(isSaveFileExist, Is.True);
    }

    [Test]
    public void Close_Destroy_Temp_Text_File()
    {
        var activeSession = LoadSessionFromJson();
        var path = activeSession.SavePath;
        
        SessionManager.Close();

        var isTextFileExist = File.Exists(path);
        Assert.That(isTextFileExist, Is.False);
    }
    [Test]
    public void Close_Set_Session_Status_To_False()
    {
        SessionManager.Close();

        var expectedSession = new Session()
        {
            Status = false,
            SavePath = ""
        };
        
        var sessionResult = LoadSessionFromJson();
        Assert.That(sessionResult, Is.EqualTo(expectedSession));
    }

    static Session LoadSessionFromJson()
    {
        var jsonContent = File.ReadAllText("Json/session.json");
        var session = JsonSerializer.Deserialize<Session>(jsonContent);
        return session;

    }

}
