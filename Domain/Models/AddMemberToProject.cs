namespace Domain.Models;
public class AddMemberToProject
{
  public List<string> MemberIds { get; set; } = null!;
  public int ProjectId { get; set; }
}
