using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Data.Entities;
public class NotificationEntity
{
  [Key]
  public string Id { get; set; } = Guid.NewGuid().ToString();

  [ForeignKey(nameof(TargetGroup))]
  public string TargetGroupId { get; set; } = null!;
  public NotificationTargetGroupEntity TargetGroup { get; set; } = null!;

  [ForeignKey(nameof(NotificationType))]
  public string NotificationTypeId { get; set; } = null!;
  public NotificationTypeEntity NotificationType { get; set; } = null!;

  public string Icon { get; set; } = null!;

  [Column(TypeName = "nvarchar(255)")]
  public string Message { get; set; } = null!;
  [Column(TypeName = "bit")]
  public bool IsRead { get; set; } = false;

  [Column(TypeName = "datetime")]
  public DateTime CreatedAt { get; set; } = DateTime.Now;
  public ICollection<NotificationDismissedEntity> DismissedNotifications { get; set; } = [];
}
