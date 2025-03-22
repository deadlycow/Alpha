using System.ComponentModel.DataAnnotations;

namespace Data.Entities;
public class MemberProjectEntity
{
  [Key]
  public int Id { get; set; }
  public int ProjectId { get; set; }
  public ProjectEntity Project { get; set; } = null!;
  public int MemberId { get; set; }
  public MemberEntity Member { get; set; } = null!;
}
