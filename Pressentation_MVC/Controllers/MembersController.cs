using Business.Services;
using Domain.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Pressentation_MVC.Controllers
{
  [Authorize(Roles = "Admin")]
  public class MembersController(MemberService memberService) : Controller
  {
    private readonly MemberService _memberService = memberService;

    [Route("team")]
    public async Task<IActionResult> Member()
    {
      var members = await _memberService.GetAllAsync();
      return View(members);
    }

    [HttpPost]
    public async Task<IActionResult> Create([FromForm] MemberCreateForm form)
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

      //var result = await _memberService.Create(form);
      //if (!result.Success)
      //  return BadRequest(result.ErrorMessage);
      return Ok();
    }

    [HttpPut]
    public async Task<IActionResult> Update(string id, [FromForm] Member form)
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


      //var member = await _memberService.GetAsync(id);
      //if (member is Result<Member> user)
      //  return View(user.Data);
      return Ok();
    }
  }
}
