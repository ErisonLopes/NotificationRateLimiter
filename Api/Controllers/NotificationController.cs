using Application.Service;
using Domain.Entities;
using Domain.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers;

[ApiController]
[Route("[controller]")]
public class NotificationController : Controller
{
    private readonly INotificationService _notificationService;

    public NotificationController(INotificationService notificationService)
    {
        _notificationService = notificationService;
    }

    [HttpPost]
    public IActionResult SendNotification(string type, string userId, string message)
    {
        var notification = new Notification(type, userId, message);

        _notificationService.SendNotification(notification);

        return Ok();
    }
}
