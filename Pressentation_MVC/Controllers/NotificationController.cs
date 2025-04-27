using Business.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace Pressentation_MVC.Controllers
{
  [Authorize(Roles ="Admin, User")]
  [Route("api/[controller]")]
  [ApiController]
  public class NotificationController( INotificationService notificationService) : ControllerBase
  {
    private readonly INotificationService _notificationService = notificationService;

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
      return Ok(new { success = true });
    }
  }
}
