using Business.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Pressentation_MVC.ViewModels;
using System.Threading.Tasks;

namespace Pressentation_MVC.Controllers
{
  [Authorize]
  public class HomeController(MemberService memberService) : Controller
  {
    private readonly MemberService _memberService = memberService;

    //[Route("projects")]
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
    public IActionResult Clients()
    {
      return View();
    }
  }
}
