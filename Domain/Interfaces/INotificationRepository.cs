using Domain.Entities;

namespace Domain.Interfaces;

public interface INotificationRepository
{
    void LogNotification(Notification notification);
    int GetSentNotificationsCount(string userId, string type, TimeSpan timeWindow);
}
