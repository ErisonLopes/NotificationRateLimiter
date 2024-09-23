using Domain.Entities;
using Domain.Interfaces;
using Infrastructure.External;

namespace Application.Service;

public class NotificationService
{
    private readonly INotificationRepository _notificationRepository;
    private readonly RateLimitService _rateLimitService;
    private readonly IGateway _gateway;

    public NotificationService(INotificationRepository notificationRepository, RateLimitService rateLimitService, IGateway gateway)
    {
        _notificationRepository = notificationRepository;
        _rateLimitService = rateLimitService;
        _gateway = gateway;
    }

    public void SendNotification(Notification notification)
    {
        var rule = _rateLimitService.GetRateLimitRule(notification.Type);

        if (rule == null)
        {
            throw new ArgumentException($"No rate limit rule defined for notification type: {notification.Type}");
        }

        int sentNotificationsCount = _notificationRepository.GetSentNotificationsCount(notification.UserId, notification.Type, rule.TimeWindow);

        if (sentNotificationsCount >= rule.Limit)
        {
            Console.WriteLine($"Notification limit reached for user {notification.UserId} and type {notification.Type}. Message not sent.");
            return;
        }

        _notificationRepository.LogNotification(notification);
        _gateway.Send(notification.UserId, notification.Message);
    }
}
