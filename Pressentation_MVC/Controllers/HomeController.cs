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
      return View(new List<ProjectViewModel>());
    }
    [Route("team")]
    public async Task<IActionResult> Team()
    {
      var members = await _memberService.GetAllAsync();
      return View(members);
    }
    [Route("clients")]
    public async Task<IActionResult> Clients()
    {
      var clients = await _clientService.GetAllAsync();
      return View(clients.Data);
    }
  }
}
