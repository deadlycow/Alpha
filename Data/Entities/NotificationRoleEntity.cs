using System.ComponentModel.DataAnnotations;

namespace Data.Entities;
public class NotificationRoleEntity
{
  public int NotificationId { get; set; }
  public NotificationEntity? Notification { get; set; }

  public int RoleId { get; set; }
  public RoleEntity? Role { get; set; }
}
