using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Pressentation_MVC.Controllers;
public class AccountController : Controller
{
  [AllowAnonymous]
  public IActionResult Login()
  {
    return View();
  }
  [AllowAnonymous]
  public IActionResult Create()
  {
    return View();
  }
  [AllowAnonymous]
  public IActionResult AdminLogin()
  {
    return View();
  }
}
