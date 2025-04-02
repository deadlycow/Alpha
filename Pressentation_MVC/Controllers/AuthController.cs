using Business.Models;
using Business.Services;
using Data.Entities;
using Domain.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Razor.TagHelpers;
using System.Security.Claims;

namespace Pressentation_MVC.Controllers;

[Route("/auth")]
[AllowAnonymous]
public class AuthController(AuthService authService, SignInManager<IdentityUser> signInManager, UserManager<IdentityUser> userManager) : Controller
{
  private readonly AuthService _authService = authService;
  private readonly SignInManager<IdentityUser> _signInManager = signInManager;
  private readonly UserManager<IdentityUser> _userManager = userManager;

  [Route("admin")]
  public IActionResult Admin()
  {
    return View();
  }


  #region Login

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
  #endregion

  #region Signup

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

  #endregion

  #region Logout

  public async Task<IActionResult> Logout()
  {
    await _authService.LogoutAsync();
    return LocalRedirect("/login");
  }

  #endregion

  #region Extrenal Authentication

  [HttpPost]
  public IActionResult ExternalSignIn(string provider, string returnUrl = null!)
  {
    if (string.IsNullOrEmpty(provider))
    {
      ModelState.AddModelError("", "Invalid provider");
      return View("Login");
    }

    var redirectUrl = Url.Action("ExternalSignInCallback", "Auth", new { returnUrl })!;
    var properties = _signInManager.ConfigureExternalAuthenticationProperties(provider, redirectUrl);
    return Challenge(properties, provider);
  }

  public async Task<IActionResult> ExternalSignInCallback(string returnUrl = null!, string remoteError = null!)
  {
    returnUrl ??= Url.Content("~/");

    if (!string.IsNullOrEmpty(remoteError))
    {
      ModelState.AddModelError("", $"Error from external provider: {remoteError}");
      return View("Login");
    }

    var info = await _signInManager.GetExternalLoginInfoAsync();
    if (info == null)
      return RedirectToAction("Login");

    var signInResult = await _signInManager.ExternalLoginSignInAsync(info.LoginProvider, info.ProviderKey, isPersistent: false, bypassTwoFactor: true);
    if (signInResult.Succeeded)
    {
      return LocalRedirect(returnUrl);
    }
    else
    {
      string firstName = string.Empty;
      string lastName = string.Empty;

      try
      {
        firstName = info.Principal.FindFirstValue(ClaimTypes.GivenName)!;
        lastName = info.Principal.FindFirstValue(ClaimTypes.Surname)!;
      }
      catch { }

      string email = info.Principal.FindFirstValue(ClaimTypes.Email)!;
      string username = $"ext_{info.LoginProvider.ToLower()}_{email}";

      var user = new MemberEntity { UserName = username, Email = email, FirstName = firstName, LastName = lastName };

      var identityResult = await _userManager.CreateAsync(user);
      if (identityResult.Succeeded)
      {
        await _userManager.AddLoginAsync(user, info);
        await _signInManager.SignInAsync(user, isPersistent: false);
        return LocalRedirect(returnUrl);
      }

      foreach (var error in identityResult.Errors)
        ModelState.AddModelError("", error.Description);

      return View("Login");
    }
  }
  #endregion
}
