using Data.Entities;
using Domain.Models;

namespace Business.Factories;
public static class NotificationFactory
{
  public static NotificationEntity Create(NotificationCreateModel model) => new NotificationEntity
  {
    Icon = model.Icon,
    Message = model.Message,
    CreatedAt = DateTime.Now,
    NotificationTypeId = model.NotificationTypeId,
    NotificationTargetGroupId = model.NotificationTargetGroupId,
  };
}
