using Business.Interfaces;
using Business.Models;
using Business.Services;
using Domain.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Pressentation_MVC.Controllers
{
  public class MembersController(MemberService memberService, ImageService imageService, MpService mpService, INotificationService notificationService, INotificationDispatcher notificationDispatcher) : Controller
  {
    private readonly MemberService _memberService = memberService;
    private readonly ImageService _imageService = imageService;
    private readonly MpService _mpService = mpService;
    private readonly INotificationService _notificationService = notificationService;
    private readonly INotificationDispatcher _notificationDispatcher = notificationDispatcher;

    [Authorize(Roles = "Admin")]
    [Route("team")]
    public async Task<IActionResult> Member()
    {
      var respons = await _memberService.GetAllAsync();
      if (respons is Result<IEnumerable<Member>> members)
        return View(members.Data);

      return View();
    }

    [Authorize(Roles = "Admin")]
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

      var notification = new NotificationCreateModel
      {
        Icon = $"{form.ProfileImage}",
        Message = $"Member \"{form.FirstName} {form.LastName}\" created.",
        CreatedAt = DateTime.UtcNow,
        NotificationTypeId = 1,
        NotificationTargetGroupId = 2,
      };

      await _notificationService.AddNotificationAsync(notification);
      await _notificationDispatcher.SendNotificationAsync(notification);
      return RedirectToAction("Member");
    }

    [Authorize(Roles = "Admin")]
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

      var notification = new NotificationCreateModel
      {
        Icon = $"{form.ProfileImage}",
        Message = $"Member \"{form.FirstName} {form.LastName}\" updated.",
        CreatedAt = DateTime.UtcNow,
        NotificationTypeId = 1,
        NotificationTargetGroupId = 2,
      };

      await _notificationService.AddNotificationAsync(notification);
      await _notificationDispatcher.SendNotificationAsync(notification);
      return RedirectToAction("Member", "Members");
    }

    [Authorize(Roles = "Admin")]
    [HttpPost("members/delete/{id}/{name}")]
    public async Task<IActionResult> Delete(string id, string name)
    {
      if (id == null)
        return BadRequest("ID is null.");

      var respons = await _memberService.DeleteAsync(id);
      if (respons.Success)
      {
        //var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
        var notification = new NotificationCreateModel
        {
          Icon = "images/delete-icon.svg",
          Message = $"Member \"{name}\" deleted.",
          CreatedAt = DateTime.UtcNow,
          NotificationTypeId = 1,
          NotificationTargetGroupId = 2,
        };

        await _notificationService.AddNotificationAsync(notification);
        await _notificationDispatcher.SendNotificationAsync(notification);
        return RedirectToAction("Member");
      }

      return BadRequest(respons.ErrorMessage);
    }

    [Authorize(Roles = "Admin")]
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
    [Authorize(Roles = "Admin, User")]
    [HttpGet]
    [Route("Members/GetAll")]
    public async Task<IActionResult> GetAll()
    {
      var respons = await _memberService.GetAllAsync();
      if (respons is Result<IEnumerable<Member>> members)
      {
        var result = members.Data!.Select(m => new
        {
          m.Id,
          m.Name,
        });

        return Json(result);
      }
      return NotFound();
    }

    [Authorize(Roles = "Admin, User")]
    [HttpPost]
    [Route("Members/Update/Project/")]
    public async Task<IActionResult> UpdateProjectMembers([FromBody] AddMemberToProject dto)
    {
      if (dto == null)
        return BadRequest("It's empty");
      var result = await _mpService.Update(dto.ProjectId, dto.MemberIds);
      if (result.Success)
        return Ok(result.Message);

      return BadRequest("Failed to update project members");
    }

    [Authorize(Roles = "Admin")]
    [HttpPost("Members/{id}/role")]
    public async Task<IActionResult> UpdateRole(string id, [FromBody] string role)
    {
      if (string.IsNullOrEmpty(id) || string.IsNullOrEmpty(role))
        return BadRequest("ID or role is null.");

      var result = await _memberService.UpdateRoleAsync(id, role);
      if (result.Success)
        return Ok("Role updated successfully");

      return BadRequest(result.ErrorMessage);
    }
  }
}
