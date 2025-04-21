using Business.Models;
using Business.Services;
using Domain.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Pressentation_MVC.Controllers
{
  [Authorize(Roles = "Admin")]
  public class MembersController(MemberService memberService, ImageService imageService) : Controller
  {
    private readonly MemberService _memberService = memberService;
    private readonly ImageService _imageService = imageService;

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

      var imgUploaded = await _imageService.Upload(form.MemberImage!);

      if (imgUploaded.Success)
        form.ProfileImage = imgUploaded.Message;
      else
        form.ProfileImage = null;

      var result = await _memberService.Create(form);
      if (!result.Success)
        return BadRequest(result.ErrorMessage);
      return RedirectToAction("Member");
    }

    [HttpPost]
    public async Task<IActionResult> Update([FromForm] Member form)
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

      var imgUploaded = await _imageService.Upload(form.MemberImage!);
      if (imgUploaded.Success)
        form.ProfileImage = imgUploaded.Message;

      if (form.Year.HasValue && form.Month.HasValue && form.Day.HasValue)
        form.BirthDate = new DateOnly(form.Year.Value, form.Month.Value, form.Day.Value);

      var result = await _memberService.UpdateAsync(form);
      if (!result.Success)
        return BadRequest(result.ErrorMessage);

      return RedirectToAction("Member", "Members");
    }

    [HttpPost("members/delete/{id}")]
    public async Task<IActionResult> Delete(string id)
    {
      if (id == null)
        return BadRequest("ID is null.");

      var respons = await _memberService.DeleteAsync(id);
      if (respons.Success)
        return RedirectToAction("Member");

      return BadRequest(respons.ErrorMessage);
    }

    [HttpGet]
    [Route("Member/GetMember/{id}")]
    public async Task<IActionResult> GetMember(string id)
    {
      var member = await _memberService.GetAsync(id);
      if (member is Result<Member> user)
      {
        return Json(new
        {
          user.Data!.Id,
          user.Data.ProfileImage,
          user.Data.FirstName,
          user.Data.LastName,
          user.Data.Email,
          user.Data.PhoneNumber,
          user.Data.JobTitle,
          user.Data.Address,
          user.Data.BirthDate,
        });
      }
      return NotFound();
    }
  }
}
