using Business.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Pressentation_MVC.ViewModels;
using System.Threading.Tasks;

namespace Pressentation_MVC.Controllers
{
  [Authorize(Roles = "Admin, User")]
  public class HomeController(MemberService memberService, ClientService clientService) : Controller
  {
    private readonly MemberService _memberService = memberService;
    private readonly ClientService _clientService = clientService;

    [Authorize(Roles ="Admin, User")]
    public IActionResult Project()
    {
      List<ProjectViewModel> testProjects = [
      new() {
        Id = 1,
        Name = "Project nr 1",
        Client = "Client 1",
      },
      new() {
        Id = 2,
        Name = "Project nr 2",
        Client = "Client 2",
      },
      new() {
        Id = 3,
        Name = "Project nr 3",
        Client = "Client 3",
      }
        ];
      return View(testProjects);
    }
    [Route("team")]
    public async Task<IActionResult> Team()
    {
      var members = await _memberService.GetAllMembers();
      return View(members);
    }
    [Route("clients")]
    public async Task<IActionResult> Clients()
    {
      var clients = await _clientService.GetAllAsync();
      return View(clients);
    }
  }
}
