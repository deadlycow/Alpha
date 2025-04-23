using Microsoft.AspNetCore.SignalR;

namespace Pressentation_MVC.Hubs;
public class NotificationHub : Hub
{
  public async Task SendNotification(object notification)
  {
    await Clients.All.SendAsync("ReceiveNotification", notification);
  }
  //public async Task SendNotificationToAll(object notification)
  //{
  //  await Clients.All.SendAsync("ReceiveNotification", notification);
  //}
}
