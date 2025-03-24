using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Data.Entities;
public class RoleEntity
{
  [Key]
  public int Id { get; set; }
  [Required]
  [Column(TypeName = "nvarchar(25)")]
  public string Name { get; set; } = null!;

  public ICollection<MemberEntity>? Members { get; set; }

  public ICollection<NotificationRoleEntity>? NotificationRoles { get; set; }
}
