using Data.Entities;

namespace Business.Interfaces
{
  public interface INotificationService
  {
    Task AddNotificationAsync(NotificationEntity notificationEntity);
    Task DismissNotification(string notificationId, string userId);
    Task<IEnumerable<NotificationEntity>> GetNotificationsAsync(string userId, int take = 5);
  }
}