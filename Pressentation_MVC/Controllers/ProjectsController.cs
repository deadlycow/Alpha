using Business.Models;
using Business.Services;
using Domain.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Pressentation_MVC.ViewModels;

namespace Pressentation_MVC.Controllers
{
  [Authorize]
  public class ProjectsController(ProjectService projectService, ClientService clientServce, MemberService memberService, ImageService imageService) : Controller
  {
    private readonly ProjectService _projectService = projectService;
    private readonly ClientService _clientServce = clientServce;
    private readonly MemberService _memberService = memberService;
    private readonly ImageService _imageService = imageService;

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
          Members = project.Members!.Select(member => new MemberViewModel { Id = member.Id, Name = $"{member.FirstName} {member.LastName}", ProfileImage = member.ProfileImage! }),
          Budget = project.Budget,
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
      return RedirectToAction("Project");
    }

    [HttpPost("projects/delete/{id}")]
    public async Task<IActionResult> Delete(int id)
    {
      if (id < 1)
        return BadRequest("ID is null.");

      var respons = await _projectService.DeleteAsync(id);
      if (respons.Success)
        return RedirectToAction("Project");

      return BadRequest(respons.ErrorMessage);
    }
  }
}
