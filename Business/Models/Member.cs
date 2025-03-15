using System.ComponentModel.DataAnnotations;

namespace Business.Models;
public class Member
{
  public int Id { get; set; }

  [Display(Name = "First Name", Prompt = "Enter first name")]
  [DataType(DataType.Text)]
  [Required(ErrorMessage = "Required")]
  public string FirstName { get; set; } = null!;

  [Display(Name = "Last Name", Prompt = "Enter first name")]
  [DataType(DataType.Text)]
  [Required(ErrorMessage = "Required")]
  public string LastName { get; set; } = null!;

  [Display(Name = "Email", Prompt = "Enter email")]
  [DataType(DataType.EmailAddress)]
  [Required(ErrorMessage = "Required")]
  public string Email { get; set; } = null!;

  public int? Phone { get; set; }

  [Display(Name = "Titel", Prompt = "Enter a titel")]
  [DataType(DataType.Text)]
  [Required(ErrorMessage = "Required")]
  public string Title { get; set; } = null!;

  [Display(Name = "Address", Prompt = "Enter Address")]
  [DataType(DataType.Text)]
  [Required(ErrorMessage = "Required")]
  public string Address { get; set; } = null!;

  [Display(Name = "Date of Birth")]
  [DataType(DataType.Date)]
  [Required(ErrorMessage = "Required")]
  public DateOnly Date { get; set; }

}
