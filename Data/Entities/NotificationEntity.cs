using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Data.Entities;
public class NotificationEntity
{
  [Key]
  public int Id { get; set; }
  [Required]
  [Column(TypeName = "nvarchar(255)")]
  public string Message { get; set; } = null!;
  [Column(TypeName = "bit")]
  public bool IsRead { get; set; } = false;
  [Required]
  [Column(TypeName = "datetime")]
  public DateTime CreatedAt { get; set; }
  
  public int MemberId { get; set; }
  public MemberEntity? Member { get; set; }

  public int ProjectId { get; set; }
  public ProjectEntity? Project { get; set; }
}
