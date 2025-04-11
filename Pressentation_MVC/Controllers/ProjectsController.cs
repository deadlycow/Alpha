using Business.Models;
using Business.Services;
using Domain.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Pressentation_MVC.ViewModels;

namespace Pressentation_MVC.Controllers
{
  [Authorize]
  public class ProjectsController(ClientService clientService, MemberService memberService) : Controller
  {
    //private readonly ProjectService _projectService = projectService;
    private readonly ClientService _clientService = clientService;
    private readonly MemberService _memberService = memberService;

    [Route("/")]
    public async Task<IActionResult> Project()
    {
      List<MemberViewModel> member = [];
      var cResult = await _clientService.GetAllAsync();
      var mResult = await _memberService.GetAllAsync();
      //var pResult = await _projectService.GetAllAsync();

      if (mResult is Result<IEnumerable<Member>> members)
      {
        foreach (var user in members.Data!)
        {
          member.Add(new() { Id = user.Id, Name = $"{user.FirstName} {user.LastName}", ProfileImage = user.ProfileImage! });
        }
      }

      var AddProject = new AddProjectViewModel
      {
        Clients = cResult is Result<IEnumerable<Client>> clients && clients.Data != null ? clients.Data : [],
        Members = member
      };
      return View(AddProject);
    }
  }
}
