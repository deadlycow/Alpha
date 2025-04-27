using Business.Factories;
using Business.Interfaces;
using Data.Contexts;
using Data.Entities;
using Domain.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace Business.Services;
public class NotificationService(DataContext context, UserManager<MemberEntity> userManager) : INotificationService
{
  private readonly DataContext _context = context;
  private readonly UserManager<MemberEntity> _userManager = userManager;

  public async Task AddNotificationAsync(NotificationCreateModel model)
  {
    if (string.IsNullOrEmpty(model.Icon))
      switch (model.NotificationTypeId)
      {
        case 1:
          model.Icon = "images/profiles/1.svg";
          break;
        case 2:
          model.Icon = "images/projectIcons/1.svg";
          break;
      }

    _context.Notifications.Add(NotificationFactory.Create(model));
    await _context.SaveChangesAsync();
  }

  public async Task<IEnumerable<NotificationEntity>> GetNotificationsAsync(string userId, int take = 10)
  {
    var user = await _userManager.FindByIdAsync(userId);
    if (user == null) return [];

    var roles = await _userManager.GetRolesAsync(user);
    var tagetGroups = new List<int> { 1 };

    if (roles.Contains("Admin"))
      tagetGroups.Add(2);

    var dismissedIds = await _context.DismissedNotifications
      .Where(d => d.UserId == userId)
      .Select(d => d.NotificationId)
      .ToListAsync();

    var notifications = await _context.Notifications
      .Where(n => !dismissedIds.Contains(n.Id) && tagetGroups.Contains(n.NotificationTargetGroupId))
      .OrderByDescending(n => n.CreatedAt)
      .Take(take)
      .ToListAsync();

    return notifications;
  }

  public async Task DismissNotification(string notificationId, string userId)
  {
    var alreadyDismissed = await _context.DismissedNotifications
      .AnyAsync(d => d.UserId == userId && d.NotificationId == notificationId);
    if (!alreadyDismissed)
    {
      var dismissed = new NotificationDismissedEntity()
      {
        NotificationId = notificationId,
        UserId = userId,
      };
      _context.Add(dismissed);
      await _context.SaveChangesAsync();
    }
  }
}
