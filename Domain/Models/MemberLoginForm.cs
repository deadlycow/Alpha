using System.ComponentModel.DataAnnotations;

namespace Domain.Models;
public class MemberLoginForm
{
  [Required]
  [DataType(DataType.EmailAddress)]
  [Display(Name = "Email", Prompt = "Your email address")]
  public string Email { get; set; } = null!;

  [Required]
  [DataType(DataType.Password)]
  [Display(Name = "Password", Prompt = "Enter your password")]
  public string Password { get; set; } = null!;
} 
