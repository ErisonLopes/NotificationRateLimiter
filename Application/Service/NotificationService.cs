using Domain.Entities;
using Domain.Interfaces;
using Infrastructure.External;
using Microsoft.Extensions.Logging;

namespace Application.Service;

public class NotificationService : INotificationService
{
    private readonly INotificationRepository _notificationRepository;
    private readonly RateLimitService _rateLimitService;
    private readonly IGateway _gateway;
    private readonly ILogger<NotificationService> _logger;

    public NotificationService(INotificationRepository notificationRepository, RateLimitService rateLimitService, IGateway gateway, ILogger<NotificationService>logger)
    {
        _notificationRepository = notificationRepository;
        _rateLimitService = rateLimitService;
        _gateway = gateway;
        _logger = logger;
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

            _logger.LogInformation($"Notification limit reached for user {notification.UserId} and type {notification.Type}. Message not sent.");
            return;
        }
        _gateway.Send(notification.UserId, notification.Message);
        _notificationRepository.LogNotification(notification);
        
    }
}
