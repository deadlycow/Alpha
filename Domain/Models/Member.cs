using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations;

namespace Domain.Models;
public class Member
{
  public string Id { get; set; } = null!;

  [DataType(DataType.Upload)]
  public IFormFile? MemberImage { get; set; }

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
  
  public string? PhoneNumber { get; set; }
  public string? JobTitle { get; set; }
  public DateOnly? DateOfBirth { get; set; }
  public Address? Address { get; set; }
}
