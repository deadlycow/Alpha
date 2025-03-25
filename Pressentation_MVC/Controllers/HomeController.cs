using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Pressentation_MVC.ViewModels;

namespace Pressentation_MVC.Controllers
{
  [Authorize]
  public class HomeController : Controller
  {
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
    public IActionResult Team()
    {
      var testMembers = new List<MemberViewModel>
      {
        new()
          {
          Id = 1,
          FirstName = "Andreas",
          LastName = "Karlsson",
          Title = "Kungen av mat",
          Email = "mail@mail.com",
          Phone = "070-00112378",
          },
        new()
          {
          Id = 2,
          FirstName = "Hankan",
          LastName = "Larrsoon",
          Title = "Kungen av mat",
          Email = "mail@mail.com",
          Phone = "070-00112378",
          }
      };

      return View(testMembers);
    }
    [Route("clients")]
    public IActionResult Clients()
    {
      return View();
    }
  }
}
