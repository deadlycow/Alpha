using Business.Models;
using Business.Services;
using Domain.Models;
using Microsoft.AspNetCore.Mvc;

namespace Pressentation_MVC.Views.Shared.Components.UserName;
public class UserNameViewComponent(MemberService memberService) : ViewComponent
{
  private readonly MemberService _memberService = memberService;

  public async Task<IViewComponentResult> InvokeAsync()
  {
    var userResult = await _memberService.GetUserAsync(UserClaimsPrincipal);
    var name = userResult is Result<Member> user ? user.Data?.Name : null;
    return View("Default", name);
  }
}
