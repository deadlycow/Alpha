using Domain.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Pressentation_MVC.Controllers;
[Route("/auth")]
public class AccountController : Controller
{
  [Route("login")]
  public IActionResult Login()
  {
    ViewBag.ErrorMessage = string.Empty;

    return View(new MemberLoginForm());
  }

  [HttpPost]
  [Route("login")]
  public IActionResult Login(MemberLoginForm form)
  {
    ViewBag.ErrorMessage = string.Empty;
    if (!ModelState.IsValid)
    {
      ViewBag.ErrorMessage = "Incorrect email or password";
      return View(form);
    }
    return View();
  }

  [Route("create")]
  public IActionResult Create()
  {
    return View();
  }
  
  [Route("admin")]
  public IActionResult Admin()
  {
    return View();
  }
}
