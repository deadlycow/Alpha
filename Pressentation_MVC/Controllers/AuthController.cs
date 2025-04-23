using Business.Interfaces;
using Business.Services;
using Data.Entities;
using Domain.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using Pressentation_MVC.Hubs;
using System.Security.Claims;

namespace Pressentation_MVC.Controllers;

[Route("/auth")]
[AllowAnonymous]
public class AuthController(AuthService authService, SignInManager<MemberEntity> signInManager, UserManager<MemberEntity> userManager, INotificationService notificationService, IHubContext<NotificationHub> notificationHub) : Controller
{
  private readonly AuthService _authService = authService;
  private readonly SignInManager<MemberEntity> _signInManager = signInManager;
  private readonly UserManager<MemberEntity> _userManager = userManager;
  private readonly INotificationService _notificationService = notificationService;
  private readonly IHubContext<NotificationHub> _notificationHub = notificationHub;


  [Route("admin")]
  public IActionResult Admin()
  {
    return View();
  }
  [Route("admin")]
  [HttpPost]
  public async Task<IActionResult> Admin([FromForm] SignInForm form)
  {
    ViewBag.ErrorMessage = string.Empty;

    if (!ModelState.IsValid)
      return View(form);

    var result = await _authService.SignInAsync(form);
    if (result)
      return LocalRedirect("~/");

    ViewBag.ErrorMessage = "Incorrect email or password";
    return View(form);
  }


  #region SignIn

  [Route("signin")]
  public IActionResult SignIn()
  {
    return View();
  }

  [HttpPost]
  [Route("signin")]
  public async Task<IActionResult> SignIn([FromForm] SignInForm form)
  {
    ViewBag.ErrorMessage = string.Empty;

    if (!ModelState.IsValid)
      return View(form);

    var result = await _authService.SignInAsync(form);
    if (result)
    {
      //var user = await _userManager.FindByEmailAsync(form.Email);
      //if (user != null)
      //{
      //  var notifications = await _notificationService.GetNotificationsAsync(user.Id);
      //  var newNotification = notifications.OrderByDescending(n => n.CreatedAt).FirstOrDefault();
      //  if (newNotification != null)
      //  {
      //    await _notificationHub.Clients.User(user.Id).SendAsync("ReceiveNotification", newNotification);
      //  }
      //}
        return LocalRedirect("~/");
    }

    ViewBag.ErrorMessage = "Incorrect email or password";
    return View(form);
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
  public async Task<IActionResult> Signup([FromForm] SignUpForm form)
  {

    if (!ModelState.IsValid)
      return View(form);

    var result = await _authService.SignUpAsync(form);
    if (result.Succeeded)
      return RedirectToAction("SignIn");

    return View(form);
  }

  #endregion

  #region Logout

  public async Task<IActionResult> Logout()
  {
    await _authService.LogoutAsync();
    return RedirectToAction("SignIn");
  }

  #endregion

  #region Extrenal Authentication

  [HttpPost]
  [Route("externalSignIn")]
  public IActionResult ExternalSignIn(string provider, string returnUrl = null!)
  {
    if (string.IsNullOrEmpty(provider))
    {
      ModelState.AddModelError("", "Invalid provider");
      return View("SignIn");
    }

    var redirectUrl = Url.Action("ExternalSignInCallback", "Auth", new { returnUrl })!;
    var properties = _signInManager.ConfigureExternalAuthenticationProperties(provider, redirectUrl);
    return Challenge(properties, provider);
  }

  [Route("externalSignInCallback")]
  public async Task<IActionResult> ExternalSignInCallback(string returnUrl = null!, string remoteError = null!)
  {
    returnUrl ??= Url.Content("~/");

    if (!string.IsNullOrEmpty(remoteError))
    {
      ModelState.AddModelError("", $"Error from external provider: {remoteError}");
      return View("SignIn");
    }

    var info = await _signInManager.GetExternalLoginInfoAsync();
    if (info == null)
      return RedirectToAction("SignIn");

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

      return View("SignIn");
    }
  }
  #endregion
}
