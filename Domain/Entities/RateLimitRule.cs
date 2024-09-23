namespace Domain.Entities;

public class RateLimitRule
{
    public string NotificationType { get; }
    public int Limit { get; }
    public TimeSpan TimeWindow { get; }

    public RateLimitRule(string notificationType, int limit, TimeSpan timeWindow)
    {
        NotificationType = notificationType;
        Limit = limit;
        TimeWindow = timeWindow;
    }
}
