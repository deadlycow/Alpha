using Data.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.SignalR;

namespace Pressentation_MVC.Hubs;
public class NotificationHub(UserManager<MemberEntity> userManager) : Hub
{
  private readonly UserManager<MemberEntity> _userManager = userManager;

  public override async Task OnConnectedAsync()
  {
    var user = Context.User;

    if (user?.Identity?.IsAuthenticated == true)
    {
      var userId = _userManager.GetUserId(user);
      var appUser = await _userManager.FindByIdAsync(userId!);

      if (appUser != null)
      {
        var roles = await _userManager.GetRolesAsync(appUser);
        Console.WriteLine($"User {appUser.Email} roles: {string.Join(",", roles)}");

        if (roles.Contains("Admin"))
        {
          await Groups.AddToGroupAsync(Context.ConnectionId, "Admins");
        }
        else if (roles.Contains("User"))
        {
          await Groups.AddToGroupAsync(Context.ConnectionId, "AllUsers");
        }
      }
    }
    await base.OnConnectedAsync();
  }
  public async Task SendNotification(NotificationEntity notification)
  {
    if (notification.NotificationTargetGroupId == 1)
      await Clients.All.SendAsync("ReceiveNotification", notification);
    else if (notification.NotificationTargetGroupId == 2)
      await Clients.Group("Admins").SendAsync("ReceiveNotification", notification);
  }
  //public async Task SendNotificationToAll(object notification)
  //{
  //  await Clients.All.SendAsync("ReceiveNotification", notification);
  //}

  public async Task<string> WhoAmI()
  {
    var user = Context.User;

    if (user?.Identity?.IsAuthenticated == true)
    {
      var userId = _userManager.GetUserId(user);
      var appUser = await _userManager.FindByIdAsync(userId!);
      if (appUser != null)
      {
        var roles = await _userManager.GetRolesAsync(appUser);
        return roles.Contains("Admin") ? "Admin" : "User";
      }
    }

    return "Anonymous";
  }
}
