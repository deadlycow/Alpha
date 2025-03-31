using Business.Services;
using Domain.Models;
using Microsoft.AspNetCore.Mvc;

namespace Pressentation_MVC.Controllers;

public class AuthController(MemberService memberService) : Controller
{
  private readonly MemberService _memberService = memberService;

  [HttpPost]
  public async Task<IActionResult> CreateMember(MemberCreateForm form)
  {
    var result = await _memberService.Create(form);
    if (!result.Success)
      return BadRequest(result.ErrorMessage);
    return RedirectToAction("team", "Home");
  }
  [HttpGet]
  public async Task<IActionResult> EditMember(string id)
  {
    var member = await _memberService.GetAsync(id);
    return null;
  }
}
