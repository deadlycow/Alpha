using Business.Models;
using Business.Services;
using Domain.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Pressentation_MVC.ViewModels;

namespace Pressentation_MVC.Controllers
{
  [Authorize]
  public class ProjectsController(ProjectService projectService) : Controller
  {
    private readonly ProjectService _projectService = projectService;

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
  }
}
