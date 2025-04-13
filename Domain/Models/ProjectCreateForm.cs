using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations;

namespace Domain.Models;
public class ProjectCreateForm
{
  [DataType(DataType.Upload)]
  public IFormFile? ProjectImage { get; set; }
  public string? ProjectUrl { get; set; }
  public string Name { get; set; } = null!;
  public string? Description { get; set; }
  public DateOnly StartDate { get; set; }
  public DateOnly? EndDate { get; set; }
  public decimal Budget { get; set; }
  public int ClientId { get; set; }
  public List<int>? MembersId { get; set; } = [];
}
