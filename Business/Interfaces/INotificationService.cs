using Data.Entities;
using Domain.Models;

namespace Business.Interfaces
{
  public interface INotificationService
  {
    Task AddNotificationAsync(NotificationCreateModel notificationEntity);
    Task DismissNotification(string notificationId, string userId);
    Task<IEnumerable<NotificationEntity>> GetNotificationsAsync(string userId, int take = 5);
  }
}