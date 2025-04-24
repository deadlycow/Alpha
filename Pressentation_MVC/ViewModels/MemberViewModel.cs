using Domain.Models;

namespace Pressentation_MVC.ViewModels;
public class MemberViewModel
{
  public string Id { get; set; } = null!;
  public string ProfileImage { get; set; } = null!;
  public string Name { get; set; } = null!;
  public string Email { get; set; } = null!;
  public string? PhoneNumber { get; set; }
  public string? JobTitle { get; set; }
  public List<string> RoleType { get; set; } = null!;
}
