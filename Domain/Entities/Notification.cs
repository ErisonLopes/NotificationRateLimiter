namespace Domain.Entities;

public class Notification
{
    public string Type { get; set; }
    public string UserId { get; set; }
    public string Message { get; set; }

    public Notification(string type, string userId, string message)
    {
        Type = type;
        UserId = userId;
        Message = message;
    }
}
