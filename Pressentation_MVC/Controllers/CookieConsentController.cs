using Domain.Models;
using Microsoft.AspNetCore.Mvc;

namespace Pressentation_MVC.Controllers
{
  public class CookieConsentController : Controller
  {
    [HttpPost]
    public IActionResult Setcookies([FromBody] CookieConsent consent)
    {
      if (consent == null)
        return BadRequest();

      Response.Cookies.Append("SessionCookie", "Essential", new CookieOptions
      {
        IsEssential = true,
        Expires = DateTimeOffset.UtcNow.AddYears(1)
      });

      if (consent.Functional)
      {
        Response.Cookies.Append("FunctionalCookie", "Non-Essential", new CookieOptions
        {
          IsEssential = false,
          Expires = DateTimeOffset.UtcNow.AddDays(90),
          SameSite = SameSiteMode.Lax,
          Path = "/"
        });
      }
      else
      {
        Response.Cookies.Delete("FunctionalCookie");
      }
      if (consent.Analytics)
      {
        Response.Cookies.Append("AnalyticsCookie", "Non-Essential", new CookieOptions
        {
          IsEssential = false,
          Expires = DateTimeOffset.UtcNow.AddDays(90),
          SameSite = SameSiteMode.Lax,
          Path = "/"
        });
      }
      else
      {
        Response.Cookies.Delete("AnalyticsCookie");
      }
      if (consent.Marketing)
      {
        Response.Cookies.Append("MarketingCookie", "Non-Essential", new CookieOptions
        {
          IsEssential = false,
          Expires = DateTimeOffset.UtcNow.AddDays(90),
          SameSite = SameSiteMode.Lax,
          Path = "/"
        });
      }
      else
      {
        Response.Cookies.Delete("MarketingCookie");
      }
      return Ok();
    }
  }
}
