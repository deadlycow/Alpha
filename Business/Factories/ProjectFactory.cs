using Data.Entities;
using Domain.Models;

namespace Business.Factories;
public static class ProjectFactory
{
  public static Project Create(ProjectEntity entity) => new()
  {
    Id = entity.Id,
    Name = entity.Name,
    Description = entity.Description,
    StartDate = entity.StartDate,
    EndDate = entity.EndDate,
    Budget = entity.Budget,
    ProjectImage = entity.ProjectImage,
    ClientId = entity.ClientId
  };

  public static ProjectEntity Create(ProjectCreateForm form) => new()
  {
    ProjectImage = form.ProjectUrl,
    Name = form.Name,
    Description = form.Description!,
    StartDate = form.StartDate,
    EndDate = form.EndDate,
    Budget = form.Budget,
    ClientId = form.ClientId,
    MemberProject = form.MembersId?.Select(memberId => new MemberProjectEntity
    {
      MemberId = memberId,
    }).ToList()
  };

  public static ProjectEntity Update(Project form, Project entity)
  {
    entity.ProjectImage = form.ProjectImage;
    entity.Name = form.Name;
    entity.Description = form.Description!;
    entity.StartDate = form.StartDate;
    entity.EndDate = form.EndDate;
    entity.Budget = form.Budget;
    entity.ClientId = form.ClientId;
    return entity;
  }

  public static IEnumerable<Project> CreateList(IEnumerable<ProjectEntity> entities) => entities.Select(Create);
}
