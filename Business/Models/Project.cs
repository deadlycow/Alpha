namespace Business.Models;
public class Project
{
  public int Id { get; set; }
  public string Name { get; set; } = null!;
  public string Client { get; set; } = null!;
  public string Description { get; set; } = null!;
  public DateOnly StartDate { get; set; } = new DateOnly();
  public DateOnly? EndDate { get; set; }
  public int Budget { get; set; }
}
