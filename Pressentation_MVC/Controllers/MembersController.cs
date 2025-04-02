using Azure.Core;
using Business.Models;
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
      var respons = await _memberService.GetAllAsync();
      if (respons is Result<IEnumerable<Member>> members)
        return View(members.Data);

      return View();
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

      var result = await _memberService.Create(form);
      if (!result.Success)
        return BadRequest(result.ErrorMessage);
      return Ok();
    }

    [HttpPut]
    public async Task<IActionResult> Update(string id, [FromForm] Member? form)
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

    [HttpPost("{id}")]
    public async Task<IActionResult> Delete(string id)
    {
      if (id == null)
        return BadRequest("ID is null.");

      Console.WriteLine(id);
      var respons = await _memberService.DeleteAsync(id);
      if (respons.Success)
        return Ok();

      return BadRequest(respons.ErrorMessage);
    }
  }
}
