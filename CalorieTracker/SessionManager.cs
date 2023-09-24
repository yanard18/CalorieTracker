using System.Text;
using System.Text.Json;

namespace CalorieTracker;

public static class SessionManager
{
    const string PATH = "Json/session.json";
    public static Session Load()
    {
        var jsonContent = File.ReadAllText(PATH);
        var session = JsonSerializer.Deserialize<Session>(jsonContent);

        if (session == null)
            throw new InvalidOperationException();

        return session;
    }
    public static void Start()
    {
        if (!File.Exists(PATH))
            throw new FileNotFoundException($"Couldn't find session json for given path: {PATH}");

        var currentSession = Load();
        if (currentSession.Status == true)
            throw new InvalidOperationException("Session is already open");

        var newSession = new Session()
        {
            Status = true,
            SavePath = CreateTextFileWithUniqueName()
        };
        WriteSessionToJsonFile(newSession);
    }

    public static void Close()
    {
        var currentSession = Load();
        if(currentSession.Status == false)
            return;
        
        var closedSession = new Session()
        {
            Status = false,
            SavePath = ""
        };

        WriteResultFile();
        DestroyTempFile();
        WriteSessionToJsonFile(closedSession);
    }
    static void WriteResultFile()
    {
        var session = Load();
        var fileInfo = new FileInfo(session.SavePath);
        if(fileInfo.Length == 0)
            return;
        
        var resultContent = File.ReadAllText(session.SavePath);

        var fileName = $"{DateTime.Now:yyyy-MM-dd_hh-mm-ss}.txt";
        using var fs = File.Create($"Out/{fileName}");
        var content = new UTF8Encoding(true).GetBytes(resultContent);
        fs.Write(content, 0, content.Length);
        fs.Close();
    }
    static void DestroyTempFile()
    {
        var activeSession = Load();
        if(File.Exists(activeSession.SavePath))
            File.Delete(activeSession.SavePath);
    }
    static string CreateTextFileWithUniqueName()
    {
        var uniqueFileName = $"{Guid.NewGuid()}.txt";
        var file = File.Create($"Temp/{uniqueFileName}");
        file.Close();
        return $"Temp/{uniqueFileName}";
    }

    static void WriteSessionToJsonFile(Session session)
    {
        var options = new JsonSerializerOptions
        {
            WriteIndented = true
        };

        var jsonContentToSave = JsonSerializer.Serialize(session, options);
        File.WriteAllText(PATH, jsonContentToSave);
    }
}
