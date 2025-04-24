using Business.Interfaces;
using Data.Contexts;
using Data.Entities;
using Microsoft.EntityFrameworkCore;

namespace Business.Services;
public class NotificationService(DataContext context) : INotificationService
{
  private readonly DataContext _context = context;

  public async Task AddNotificationAsync(NotificationEntity notificationEntity)
  {
    if (string.IsNullOrEmpty(notificationEntity.Icon))
      switch (notificationEntity.NotificationTypeId)
      {
        case 1:
          notificationEntity.Icon = "images/profiles/1.svg";
          break;
        case 2:
          notificationEntity.Icon = "images/projectIcons/1.svg";
          break;
      }
    
    _context.Add(notificationEntity);
    await _context.SaveChangesAsync();
  }

  public async Task<IEnumerable<NotificationEntity>> GetNotificationsAsync(string userId, int take = 10)
  {
    var dismissedIds = await _context.DismissedNotifications
      .Where(d => d.UserId == userId)
      .Select(d => d.NotificationId)
      .ToListAsync();
    var notifications = await _context.Notifications
      .Where(n => !dismissedIds.Contains(n.Id))
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
