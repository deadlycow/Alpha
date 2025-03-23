using System.ComponentModel.DataAnnotations;

namespace Data.Entities;
public class MemberProjectEntity
{
  [Required]
  public int ProjectId { get; set; }
  public ProjectEntity Project { get; set; } = null!;

  [Required]
  public int MemberId { get; set; }
  public MemberEntity Member { get; set; } = null!;
}
