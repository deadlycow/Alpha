

namespace Domain.Models;
public class Project
{
  public int Id { get; set; }
  public string Name { get; set; } = null!;
  public string? Description { get; set; }
  public DateOnly StartDate { get; set; }
  public DateOnly? EndDate { get; set; }
  public decimal Budget { get; set; }
  public string? ProjectImage { get; set; }
  public bool Status { get; set; } = false;
  public int ClientId { get; set; }
  public Client? Client { get; set; }
  public ICollection<Member>? Members{ get; set; }
}