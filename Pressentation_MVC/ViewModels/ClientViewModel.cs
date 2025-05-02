using System.ComponentModel.DataAnnotations;

namespace Pressentation_MVC.ViewModels;
public class ClientViewModel
{
  public int Id { get; set; }
  [Display(Name = "Name", Prompt = "Enter clients name")]
  public string Name { get; set; } = null!;
  
  public IFormFile? ImgFile { get; set; }
  public string? ClientImage { get; set; }
}
