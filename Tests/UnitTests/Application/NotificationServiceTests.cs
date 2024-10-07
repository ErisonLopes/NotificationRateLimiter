using Application.Service;
using Castle.Core.Logging;
using Domain.Entities;
using Domain.Interfaces;
using Infrastructure.External;
using Microsoft.Extensions.Logging;
using Moq;

namespace UnitTests.Application
{
    public class NotificationServiceTests
    {
        private readonly Mock<INotificationRepository> _notificationRepositoryMock;
        private readonly Mock<IGateway> _gatewayMock;
        private readonly Mock<ILogger<NotificationService>> _logger;
        private readonly NotificationService _notificationService;
        private readonly RateLimitService _rateLimitService;

        public NotificationServiceTests()
        {
            _notificationRepositoryMock = new Mock<INotificationRepository>();
            _gatewayMock = new Mock<IGateway>();
            _rateLimitService = new RateLimitService();
            _logger = new Mock<ILogger<NotificationService>>();
            _notificationService = new NotificationService(_notificationRepositoryMock.Object, _rateLimitService, _gatewayMock.Object, _logger.Object);
        }

        [Fact]
        public void SendNotification_ShouldSend_WhenUnderLimit()
        {
            // Arrange
            var notification = new Notification("status", "user1", "This is a status update");
            _notificationRepositoryMock.Setup(x => x.GetSentNotificationsCount(notification.UserId, notification.Type, It.IsAny<TimeSpan>())).Returns(1);

            // Act
            _notificationService.SendNotification(notification);

            // Assert
            _gatewayMock.Verify(g => g.Send(notification.UserId, notification.Message), Times.Once);
            _notificationRepositoryMock.Verify(x => x.LogNotification(notification), Times.Once);
        }

        [Fact]
        public void SendNotification_ShouldNotSend_WhenOverLimit()
        {
            // Arrange
            var notification = new Notification("status", "user1", "This is a status update");
            _notificationRepositoryMock.Setup(x => x.GetSentNotificationsCount(notification.UserId, notification.Type, It.IsAny<TimeSpan>())).Returns(2);

            // Act
            _notificationService.SendNotification(notification);

            // Assert
            _gatewayMock.Verify(g => g.Send(It.IsAny<string>(), It.IsAny<string>()), Times.Never);
            _notificationRepositoryMock.Verify(x => x.LogNotification(It.IsAny<Notification>()), Times.Never);
        }

        [Fact]
        public void SendNotification_ShouldThrowException_WhenNoRuleExists()
        {
            // Arrange
            var notification = new Notification("unknown", "user1", "This is a test message");

            // Act & Assert
            Assert.Throws<ArgumentException>(() => _notificationService.SendNotification(notification));
        }
    }
}
