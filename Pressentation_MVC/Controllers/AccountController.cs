using Business.Services;
using Domain.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace Pressentation_MVC.Controllers;
[Route("/auth")]
public class AccountController(AuthService authService) : Controller
{
  private readonly AuthService _authService = authService;

  [Route("login")]
  public IActionResult Login()
  {
    ViewBag.ErrorMessage = string.Empty;
    return View(new MemberLoginForm());
  }

  [HttpPost]
  [Route("login")]
  public async Task<IActionResult> Login(MemberLoginForm form)
  {
    ViewBag.ErrorMessage = string.Empty;
    if (ModelState.IsValid)
    {
      if (await _authService.LoginAsync(form))
        return LocalRedirect("~/");
    }

    ViewBag.ErrorMessage = "Incorrect email or password";
    return View(form);
  }

  [Route("signup")]
  public IActionResult Signup()
  {
    return View(new MemberSignUpForm());
  }

  [HttpPost]
  [Route("signup")]
  public async Task<IActionResult> Signup(MemberSignUpForm form)
  {
    if (ModelState.IsValid)
    {
      var result = await _authService.SignUpAsync(form);
      if (result.Succeeded)
        return LocalRedirect("~/");
      else 
        ViewBag.ErrorMessage = string.Join(", ", result.Errors.Select(e => e.Description));
    }
    return View(form);
  }

  [Route("admin")]
  public IActionResult Admin()
  {
    return View();
  }
  public async Task<IActionResult> Logout()
  {
    await _authService.LogoutAsync();
    return LocalRedirect("~/");
  }
}
