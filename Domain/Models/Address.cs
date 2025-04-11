using System.ComponentModel.DataAnnotations;

namespace Domain.Models;
public class Address
{
  [Display(Name = "Street", Prompt = "Your street")]
  [Required(ErrorMessage = "Required")]
  public string Street { get; set; } = null!;
  
  [Display(Name = "City", Prompt = "Your city")]
  [Required(ErrorMessage = "Required")]
  public string City { get; set; } = null!;

  [Display(Name = "Postal code", Prompt = "Your postal code")]
  [Required(ErrorMessage = "Required")]
  public int PostalCode { get; set; }
}