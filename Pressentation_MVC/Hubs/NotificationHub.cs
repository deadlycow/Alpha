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
}
