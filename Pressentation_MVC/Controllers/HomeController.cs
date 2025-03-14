using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Pressentation_MVC.Controllers
{
  public class HomeController : Controller
  {
    //[Authorize]
    [Route("projects")]
    public IActionResult Project()
    {
      return View();
    }
    [Route("team")]
    public IActionResult Team()
    {
      return View();
    }
  }
}
