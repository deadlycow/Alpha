namespace Pressentation_MVC.Models;
public class LinkItem
{
  public string ImageUrl { get; set; } = null!;
  public string Text { get; set; } = null!;
  public string Controller { get; set; } = null!;
  public string Action { get; set; } = null!;
  public string LinkClass { get; set; } = null!;
  public List<string> Classes { get; set; } = [];
  public List<string> ImgClasses { get; set; } = [];
}
