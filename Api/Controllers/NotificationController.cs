using Application.Service;
using Domain.Entities;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers;

[ApiController]
[Route("[controller]")]
public class NotificationController : Controller
{
    private readonly NotificationService _notificationService;

    public NotificationController(NotificationService notificationService)
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
