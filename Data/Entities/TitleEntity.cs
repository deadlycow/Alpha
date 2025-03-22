using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Data.Entities;
public class TitleEntity
{
  [Key]
  public int Id { get; set; }
  [Required]
  [Column(TypeName = "nvarchar(50)")]
  public string Name { get; set; } = null!;
  public ICollection<MemberEntity>? MemberEntities { get; set; }
}
