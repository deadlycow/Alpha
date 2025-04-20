using Business.Models;
using Business.Services;
using Domain.Models;
using Microsoft.AspNetCore.Mvc;

namespace Pressentation_MVC.Views.Shared.Components.ProfileImage;
public class ProfileImageViewComponent(MemberService memberService) : ViewComponent
{
  private readonly MemberService _memberService = memberService;

  public async Task<IViewComponentResult> InvokeAsync()
  {
    var userResult = await _memberService.GetUserAsync(UserClaimsPrincipal);
    var profileImage = userResult is Result<Member> user ? user.Data?.ProfileImage : null;
    return View("Default", profileImage);
  }
}
