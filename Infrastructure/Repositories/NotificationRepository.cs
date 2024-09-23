
using Domain.Entities;
using Domain.Interfaces;

namespace Infrastructure.Repositories;

public class NotificationRepository : INotificationRepository
{
    private readonly Dictionary<string, List<DateTime>> _notificationLog = new();

    public void LogNotification(Notification notification)
    {
        string key = GetKey(notification.UserId, notification.Type);

        if (!_notificationLog.ContainsKey(key))
        {
            _notificationLog[key] = new List<DateTime>();
        }

        _notificationLog[key].Add(DateTime.UtcNow);
    }

    public int GetSentNotificationsCount(string userId, string type, TimeSpan timeWindow)
    {
        string key = GetKey(userId, type);

        if (!_notificationLog.ContainsKey(key))
        {
            return 0;
        }

        var now = DateTime.UtcNow;
        _notificationLog[key] = _notificationLog[key]
            .Where(timestamp => now - timestamp <= timeWindow)
            .ToList();

        return _notificationLog[key].Count;
    }

    private string GetKey(string userId, string type)
    {
        return $"{userId}:{type}";
    }
}
