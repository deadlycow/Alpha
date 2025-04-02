using Business.Models;
using Business.Services;
using Domain.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Pressentation_MVC.Controllers;

[Route("/auth")]
[AllowAnonymous]
public class AuthController(AuthService authService) : Controller
{
  private readonly AuthService _authService = authService;

  [Route("login")]
  public IActionResult Login()
  {

    if (User.Identity!.IsAuthenticated)
      return LocalRedirect("~/");

    //ViewBag.ErrorMessage = string.Empty;
    return View();
  }

  [HttpPost]
  [Route("login")]
  public async Task<IActionResult> Login(MemberLoginForm form)
  {
    ViewBag.ErrorMessage = string.Empty;

    if (!ModelState.IsValid)
    {
      var errors = ModelState
        .Where(x => x.Value?.Errors.Count > 0)
        .ToDictionary(
        kvp => kvp.Key,
        kvp => kvp.Value?.Errors.Select(x => x.ErrorMessage).ToList()
        );
      return BadRequest(new { Success = false, errors });
    }

    //if (ModelState.IsValid)
    //{
    //  if (await _authService.LoginAsync(form))
    //    return LocalRedirect("~/");
    //}

    ViewBag.ErrorMessage = "Incorrect email or password";
    await _authService.LoginAsync(form);
    return LocalRedirect("~/");
  }

  [Route("signup")]
  public IActionResult Signup()
  {
    return View();
  }

  [HttpPost]
  [Route("signup")]
  public async Task<IActionResult> Signup([FromForm] MemberSignUpForm form)
  {

    if (!ModelState.IsValid)
    {
      var errors = ModelState
        .Where(x => x.Value?.Errors.Count > 0)
        .ToDictionary(
        kvp => kvp.Key,
        kvp => kvp.Value?.Errors.Select(x => x.ErrorMessage).ToList()
        );

      return BadRequest(new { Success = false, errors });
    }

    var result = await _authService.SignUpAsync(form);
    if (!result.Succeeded)
      return View(form);

    return LocalRedirect("~/");
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
