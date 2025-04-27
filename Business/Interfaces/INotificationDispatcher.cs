using Domain.Models;

namespace Business.Interfaces;
public interface INotificationDispatcher
{
  Task SendNotificationAsync(NotificationCreateModel notification);
}
