using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Data.Entities;
public class NotificationEntity
{
  [Key]
  public string Id { get; set; } = Guid.NewGuid().ToString();

  [ForeignKey(nameof(TargetGroup))]
  public int NotificationTargetGroupId { get; set; } = 1;
  public NotificationTargetGroupEntity TargetGroup { get; set; } = null!;

  [ForeignKey(nameof(NotificationType))]
  public int NotificationTypeId { get; set; }
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
