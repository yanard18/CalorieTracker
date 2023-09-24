namespace CalorieTracker;

public class Session
{

    public bool Status { get; set; }
    public string SavePath { get; set; }
    
    public override bool Equals(object? obj)
    {
        if (obj == null || GetType() != obj.GetType())
            return false;

        var otherSession = (Session)obj;
        return
            Status == otherSession.Status &&
            SavePath == otherSession.SavePath;
    }
}
