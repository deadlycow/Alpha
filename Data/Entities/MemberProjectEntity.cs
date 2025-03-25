using System.ComponentModel.DataAnnotations;

namespace Data.Entities;
public class MemberProjectEntity
{
  [Required]
  public int ProjectId { get; set; }
  public ProjectEntity Project { get; set; } = null!;

  [Required]
  public string MemberId { get; set; } = null!;
  public MemberEntity Member { get; set; } = null!;
}
