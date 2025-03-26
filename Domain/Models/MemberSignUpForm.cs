using System.ComponentModel.DataAnnotations;

namespace Domain.Models;
public class MemberSignUpForm
{
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
  public string Email { get; set; } = null!;

  [Display(Name = "Password", Prompt = "Enter your password")]
  [DataType(DataType.Password)]
  [Required(ErrorMessage = "Required")]
  public string Password { get; set; } = null!;

  [Display(Name = "Confirm Password", Prompt = "Confirm your password")]
  [DataType(DataType.Password)]
  [Compare(nameof(Password),ErrorMessage = "Passwords don't match")]
  [Required(ErrorMessage = "Required")]
  public string ConfirmPassword { get; set; } = null!;
  
}
