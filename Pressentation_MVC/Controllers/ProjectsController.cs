using Business.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Pressentation_MVC.Controllers
{
  [Authorize]
  public class ProjectsController : Controller
  {
    //private readonly ProjectService _projectService = projectService;
    [Route("/")]
    public IActionResult Project() { 
      return View();
    }
  }
}
