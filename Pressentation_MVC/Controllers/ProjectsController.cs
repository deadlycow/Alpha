﻿using Business.Interfaces;
using Business.Models;
using Business.Services;
using Domain.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Pressentation_MVC.ViewModels;

namespace Pressentation_MVC.Controllers
{
  [Authorize]
  public class ProjectsController(ProjectService projectService, ClientService clientServce, MemberService memberService, ImageService imageService, MpService mpService, INotificationService notificationService, INotificationDispatcher notificationDispatcher) : Controller
  {
    private readonly ProjectService _projectService = projectService;
    private readonly ClientService _clientServce = clientServce;
    private readonly MemberService _memberService = memberService;
    private readonly ImageService _imageService = imageService;
    private readonly MpService _mpService = mpService;
    private readonly INotificationService _notificationService = notificationService;
    private readonly INotificationDispatcher _notificationDispatcher = notificationDispatcher;

    [Route("/")]
    public async Task<IActionResult> Project()
    {
      var result = await _projectService.GetAllAsync();
      if (result is Result<IEnumerable<Project>> projects)
      {
        var projectViewModels = projects.Data!.Select(project => new ProjectViewModel
        {
          ProjectImage = project.ProjectImage,
          Id = project.Id,
          Name = project.Name,
          Client = project.Client!,
          Description = project.Description,
          StartDate = project.StartDate,
          EndDate = project.EndDate,
          Status = project.Status,
          Members = project.Members!.Select(member => new MemberViewModel { Id = member.Id, Name = $"{member.FirstName} {member.LastName}", ProfileImage = member.ProfileImage! }),
          Budget = project.Budget ?? 0m,
        }).ToList();
        return View(projectViewModels);
      }
      return View();
    }

    [HttpGet]
    [Route("Project/FormDataLoader/Members")]
    public async Task<IActionResult> FormDataLoaderMembers()
    {
      var members = await _memberService.GetAllAsync();

      return Json(new
      {
        members = members is Result<IEnumerable<Member>> memberResult ? memberResult.Data : null
      });
    }

    [HttpGet]
    [Route("Project/FormDataLoader/Clients")]
    public async Task<IActionResult> FormDataLoaderClients()
    {
      var clients = await _clientServce.GetAllAsync();

      return Json(new
      {
        clients = clients is Result<IEnumerable<Client>> clientResult ? clientResult.Data : null,
      });
    }

    [HttpPost]
    public async Task<IActionResult> Create([FromForm] ProjectCreateForm form)
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

      var imgUploaded = await _imageService.Upload(form.ProjectImage!);
      if (imgUploaded.Success)
        form.ProjectUrl = imgUploaded.Message;
      else
        form.ProjectImage = null;

      var result = await _projectService.Create(form);
      if (!result.Success)
        return BadRequest(result.ErrorMessage);

      var notification = new NotificationCreateModel
      {
        Icon = $"{form.ProjectUrl}",
        Message = $"Project \"{form.Name}\" has been created.",
        CreatedAt = DateTime.UtcNow,
        NotificationTypeId = 2,
      };

      await _notificationService.AddNotificationAsync(notification);
      await _notificationDispatcher.SendNotificationAsync(notification);

      return RedirectToAction("Project");
    }

    [HttpPost("projects/delete/{id}/{project}")]
    public async Task<IActionResult> Delete(int id, string project)
    {
      if (id < 1)
        return BadRequest("ID is null.");

      var respons = await _projectService.DeleteAsync(id);
      if (respons.Success)
      {
        var notification = new NotificationCreateModel
        {
          Icon = "images/delete-icon.svg",
          Message = $"Project \"{project}\" deleted.",
          CreatedAt = DateTime.UtcNow,
          NotificationTypeId = 2,
        };

        await _notificationService.AddNotificationAsync(notification);
        await _notificationDispatcher.SendNotificationAsync(notification);
        return RedirectToAction("Project");
      }

      return BadRequest(respons.ErrorMessage);
    }

    [HttpGet("Project/GetProject/{id}")]
    public async Task<IActionResult> GetProject(int id)
    {
      var project = await _projectService.GetAsync(id);

      if (project == null)
        return NotFound(id);

      if (project is Result<Project> projectResult)
        return Json(new
        {
          projectResult.Data!.Id,
          projectResult.Data.ProjectImage,
          projectResult.Data.Name,
          projectResult.Data.Description,
          projectResult.Data.StartDate,
          projectResult.Data.EndDate,
          projectResult.Data.Budget,
          projectResult.Data.ClientId,
          Members = projectResult.Data.Members!.Select(member => new
          {
            member.Id,
            member.Name,
            member.ProfileImage
          }),
        });

      return NotFound();
    }
    [HttpPost("Project/Update")]
    public async Task<IActionResult> Update([FromForm] Project form)
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

      if (form.FormFile != null)
      {
        var imgUploaded = await _imageService.Upload(form.FormFile!);
        if (imgUploaded.Success)
          form.ProjectImage = imgUploaded.Message;
      }

      var projectResult = await _projectService.UpdateAsync(form);
      if (projectResult.Success)
      {
        var mpResult = await _mpService.Update(form.Id, form.MembersId!);
        if (!mpResult.Success)
          return BadRequest("Faild to update members.");

        var notification = new NotificationCreateModel
        {
          Icon = form.ProjectImage!,
          Message = $"Project \"{form.Name}\" updated.",
          CreatedAt = DateTime.UtcNow,
          NotificationTypeId = 2,
        };

        await _notificationService.AddNotificationAsync(notification);
        await _notificationDispatcher.SendNotificationAsync(notification);
      }
      return RedirectToAction("Project");
    }
    [HttpPost("Project/Update/{id}/{status}")]
    public async Task<IActionResult> UpdateStatus(int id, bool status)
    {
      if (id < 1) return BadRequest();

      var form = await _projectService.GetAsync(id);

      if (form is Result<Project> project)
      {
        project.Data!.Status = status;
        var res = await _projectService.UpdateAsync(project.Data);
        if (!res.Success)
          return BadRequest("Faild to update status.");

        var notification = new NotificationCreateModel
        {
          Icon = project.Data.ProjectImage!,
          Message = $"Project \"{project.Data.Name}\" status changed.",
          CreatedAt = DateTime.UtcNow,
          NotificationTypeId = 2,
        };

        await _notificationService.AddNotificationAsync(notification);
        await _notificationDispatcher.SendNotificationAsync(notification);
        return RedirectToAction("Project");
      }
      return BadRequest("Failed to update project status.");
    }
  }
}
