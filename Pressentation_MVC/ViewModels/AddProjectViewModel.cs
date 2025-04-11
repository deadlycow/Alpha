using Domain.Models;

namespace Pressentation_MVC.ViewModels;
public class AddProjectViewModel
{
  public  ProjectViewModel Project { get; set; } = new();
  public IEnumerable<Client> Clients { get; set; } = [];
  public IEnumerable<MemberViewModel> Members { get; set; } = [];
}
