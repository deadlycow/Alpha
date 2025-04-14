using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations;

namespace Domain.Models;
public class ProjectCreateForm
{
  [DataType(DataType.Upload)]
  public IFormFile? ProjectImage { get; set; }
  public string? ProjectUrl { get; set; }

  [Display(Name = "Project Name", Prompt = "Project Name")]
  [DataType(DataType.Text)]
  [Required(ErrorMessage = "Required")]
  public string Name { get; set; } = null!;

  [Display(Name = "Description", Prompt = "Type somthing")]
  [DataType(DataType.Text)]
  public string? Description { get; set; }

  [Display(Name = "Start Date")]
  [DataType(DataType.Date)]
  [Required(ErrorMessage = "Required")]
  public DateOnly StartDate { get; set; }

  [Display(Name = "End Date")]
  [DataType(DataType.Date)]
  public DateOnly? EndDate { get; set; }
  public decimal Budget { get; set; }

  [Display(Name = "Client")]
  [Required(ErrorMessage = "Required")]
  public int ClientId { get; set; }

  [Display(Name = "Members")]
  [DataType(DataType.Date)]
  public List<string>? MembersId { get; set; } = [];
}
