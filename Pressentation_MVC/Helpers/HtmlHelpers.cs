using Microsoft.AspNetCore.Mvc.Rendering;

namespace Pressentation_MVC.Helpers;
public static class HtmlHelpers
{
  public static string IsActive(this IHtmlHelper html, string action, string controller)
  {
    var routeData = html.ViewContext.RouteData.Values;
    var routeAction = routeData["action"] as string;
    var routeController = routeData["controller"] as string;
    return (controller == routeController && action == routeAction) ? "active" : "";
  }
}
