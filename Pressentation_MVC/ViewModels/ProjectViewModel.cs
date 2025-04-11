using Domain.Models;
using System.ComponentModel.DataAnnotations;

namespace Pressentation_MVC.ViewModels;
public class ProjectViewModel
{
  [DataType(DataType.Upload)]
  public IFormFile? ProjectImage { get; set; }

  public int Id { get; set; }

  [Display(Name = "Project Name", Prompt = "Project Name")]
  [DataType(DataType.Text)]
  [Required(ErrorMessage = "Required")]
  public string Name { get; set; } = null!;

  [Display(Name = "Client Name", Prompt = "Client Name")]
  [DataType(DataType.Text)]
  [Required(ErrorMessage = "Required")]
  public Client Client { get; set; } = null!;

  [Display(Name = "Description", Prompt = "Type somthing")]
  [DataType(DataType.MultilineText)]
  public string? Description { get; set; }

  [Display(Name = "Start Date")]
  [DataType(DataType.Date)]
  [Required(ErrorMessage = "Required")]
  public DateOnly StartDate { get; set; } = new DateOnly();

  [Display(Name = "End Date")]
  [DataType(DataType.Date)]
  public DateOnly? EndDate { get; set; }

  [Display(Name = "Members", Prompt = "Select member/s")]
  public IEnumerable<MemberViewModel> Members { get; set; } = [];

  [Display(Name = "Budget", Prompt = "0")]
  [DataType(DataType.Currency)]
  public int Budget { get; set; }
}
