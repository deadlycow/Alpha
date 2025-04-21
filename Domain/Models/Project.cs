using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations;

namespace Domain.Models;
public class Project
{
  public int Id { get; set; }

  [Display(Name = "Project Name", Prompt = "Project Name")]
  [DataType(DataType.Text)]
  [Required(ErrorMessage = "Required")]
  public string Name { get; set; } = null!;

  [Display(Name = "Description", Prompt = "Type somthing")]
  [DataType(DataType.Text)]
  [Required(ErrorMessage = "Required")]
  public string? Description { get; set; }

  [Display(Name = "Start Date")]
  [DataType(DataType.Date)]
  [Required(ErrorMessage = "Required")]
  public DateOnly StartDate { get; set; }
  
  public DateOnly? EndDate { get; set; }

  [Display(Name = "Budget")]
  [DataType(DataType.Currency)]
  [Required(ErrorMessage = "Required")]
  public decimal? Budget { get; set; }
  
  public IFormFile? FormFile { get; set; }
  public string? ProjectImage { get; set; }
  public bool Status { get; set; } = false;

  [Display(Name = "Client")]
  [Range(1, int.MaxValue, ErrorMessage = "Required")]
  public int ClientId { get; set; }

  public Client? Client { get; set; }
  public ICollection<Member>? Members { get; set; }

  [Display(Name = "Members")]
  [DataType(DataType.Date)]
  public List<string>? MembersId { get; set; } 
}