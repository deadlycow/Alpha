using Domain.Models;
namespace Pressentation_MVC.ViewModels;
public class MemberViewModel
{
  public Member Member { get; set; } = null!;
  public Address Address { get; set; } = null!;
}
