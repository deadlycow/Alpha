using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations;

namespace Domain.Models;
public class Member
{
  public string Id { get; set; } = null!;

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
  [Required(ErrorMessage = "Required")]
  [RegularExpression(@"^(?!\.)(?!.*\.\.)([a-zA-Z0-9._%+-]{1,64})@([a-zA-Z0-9-]{1,63}\.)+[a-zA-Z]{2,}$", ErrorMessage = "Invalid email")]
  public string Email { get; set; } = null!;
  
  public string? PhoneNumber { get; set; }
  [Display(Name ="Job Title")]
  public string? JobTitle { get; set; }
  public Address? Address { get; set; }

  public int? Day { get; set; }
  public int? Month { get; set; }
  public int? Year { get; set; }
  [Display(Name = "Date of Birth")]
  public DateOnly? BirthDate { get; set; }

  public string Name => $"{FirstName} {LastName}";
}
