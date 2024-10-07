using Domain.Entities;
using Infrastructure.Repositories;

namespace UnitTests.Repositories;

public class NotificationRepositoryTests
{
    private readonly NotificationRepository _repository;

    public NotificationRepositoryTests()
    {
        _repository = new NotificationRepository();
    }

    [Fact]
    public void LogNotification_ShouldStoreNotification()
    {
        // Arrange
        var notification = new Notification("status", "user1", "This is a test");

        // Act
        _repository.LogNotification(notification);

        // Assert
        var count = _repository.GetSentNotificationsCount("user1", "status", TimeSpan.FromMinutes(1));
        Assert.Equal(1, count);
    }

    [Fact]
    public void GetSentNotificationsCount_ShouldReturnCorrectCount()
    {
        // Arrange
        var notification = new Notification("status", "user1", "This is a test");
        _repository.LogNotification(notification);

        // Simulate delay
        Thread.Sleep(1000); // 1 second delay
        _repository.LogNotification(notification);

        // Act
        var countWithinWindow = _repository.GetSentNotificationsCount("user1", "status", TimeSpan.FromSeconds(2));

        // Assert
        Assert.Equal(2, countWithinWindow);
    }
}
