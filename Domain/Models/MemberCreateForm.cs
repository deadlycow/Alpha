using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations;

namespace Domain.Models;
public class MemberCreateForm
{
  [DataType(DataType.Upload)]
  public IFormFile? MemberImage { get; set; }
  public string? ProfileImage { get; set; }

  [Display(Name = "First Name", Prompt = "Your first name")]
  [DataType(DataType.Text)]
  [Required(ErrorMessage = "Required")]
  public string FirstName { get; set; } = null!;

  [Display(Name = "Last Name", Prompt = "Your last name")]
  [DataType(DataType.Text)]
  [Required(ErrorMessage = "Required")]
  public string LastName { get; set; } = null!;

  [Display(Name = "Email", Prompt = "Your email address")]
  [DataType(DataType.EmailAddress)]
  [Required(ErrorMessage = "Required")]
  [RegularExpression(@"^[^@\s]+@[^@\s]+\.[^@\s]+$", ErrorMessage = "Invalid email")]
  public string Email { get; set; } = null!;

  [Display(Name = "Phone", Prompt = "Your phone number")]
  public string? PhoneNumber { get; set; }

  [Display(Name = "Job Title", Prompt = "Your job title")]
  [DataType(DataType.Text)]
  [Required(ErrorMessage = "Required")]
  public string? JobTitle { get; set; }

  public Address? Address { get; set; }

  public int? Day { get; set; }
  public int? Month { get; set; }
  public int? Year { get; set; }
  [Display(Name = "Date of Birth")]
  public DateOnly? BirthDate { get; set; }
}
