namespace Domain.Models;
public class NotificationCreateModel
{
  public int NotificationTargetGroupId { get; set; } = 1;
  public int NotificationTypeId { get; set; }
  public string Icon { get; set; } = null!;
  public string Message { get; set; } = null!;
  public DateTime CreatedAt { get; set; } = DateTime.Now;
}
