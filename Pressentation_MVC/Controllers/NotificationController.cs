using Business.Interfaces;
using Data.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using Pressentation_MVC.Hubs;
using System.Security.Claims;

namespace Pressentation_MVC.Controllers
{
  [Authorize(Roles ="Admin, User")]
  [Route("api/[controller]")]
  [ApiController]
  public class NotificationController(IHubContext<NotificationHub> notificationHub, INotificationService notificationService) : ControllerBase
  {
    private readonly IHubContext<NotificationHub> _notificationHub = notificationHub;
    private readonly INotificationService _notificationService = notificationService;

    [HttpPost]
    public async Task<IActionResult> CreateNotification(NotificationEntity notification)
    {
      await _notificationService.AddNotificationAsync(notification);
      var notifications = await _notificationService.GetNotificationsAsync("anonymous");
      var newNotification = notifications.OrderByDescending(n => n.CreatedAt).FirstOrDefault();
      if (newNotification != null)
      {
        await _notificationHub.Clients.All.SendAsync("ReceiveNotification", newNotification);
      }
      return Ok(new { success = true });
    }

    [HttpGet]
    public async Task<IActionResult> GetNotifications()
    {
      var userId = User.FindFirstValue(ClaimTypes.NameIdentifier) ?? "anonymous";
      if (string.IsNullOrEmpty(userId))
        return Unauthorized();

      var notifications = await _notificationService.GetNotificationsAsync(userId);
      return Ok(notifications);
    }

    [HttpPost("dismiss/{id}")]
    public async Task<IActionResult> DismissNotification(string id)
    {
      var userId = User.FindFirstValue(ClaimTypes.NameIdentifier) ?? "anonymous";
      if (string.IsNullOrEmpty(userId))
        return Unauthorized();

      await _notificationService.DismissNotification(id, userId);
      await _notificationHub.Clients.User(userId).SendAsync("DismissNotification", id);
      return Ok(new { success = true });
    }
  }
}
