using Business.Interfaces;
using Domain.Models;
using Microsoft.AspNetCore.SignalR;
using Pressentation_MVC.Hubs;

namespace Pressentation_MVC.Dispatchers;
public class NotificationDispatcher(IHubContext<NotificationHub> hubContext) : INotificationDispatcher
{
  private readonly IHubContext<NotificationHub> _hubContext = hubContext;

  public async Task SendNotificationAsync(NotificationCreateModel notification)
  {
    if (notification.NotificationTargetGroupId == 1)
      await _hubContext.Clients.Group("AllUsers").SendAsync("ReceiveNotification", notification);
    else if (notification.NotificationTargetGroupId == 2)
      await _hubContext.Clients.Group("Admins").SendAsync("ReceiveNotification", notification);
  }
}
