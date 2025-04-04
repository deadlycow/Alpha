using System.ComponentModel.DataAnnotations;

namespace Domain.Models;
public class SignInForm
{
  [Display(Name = "Email", Prompt = "Your email address")]
  [Required(ErrorMessage = "Required")]
  [DataType(DataType.EmailAddress)]
  [RegularExpression(@"^[^@\s]+@[^@\s]+\.[^@\s]+$", ErrorMessage = "Invalid email")]
  public string Email { get; set; } = null!;

  [Display(Name = "Password", Prompt = "Enter your password")]
  [Required(ErrorMessage = "Required")]
  [DataType(DataType.Password)]
  public string Password { get; set; } = null!;
}
