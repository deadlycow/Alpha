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
    Status = entity.Status,
    ClientId = entity.ClientId,

    Members = entity.MemberProject?.Select(mp => new Member
    {
      ProfileImage = mp.Member.ProfileImage,
      Id = mp.Member.Id,
      FirstName = mp.Member.FirstName,
      LastName = mp.Member.LastName,
    }).ToList(),
  };

  public static ProjectEntity Create(ProjectCreateForm form) => new()
  {
    ProjectImage = form.ProjectUrl,
    Name = form.Name,
    Description = form.Description!,
    StartDate = form.StartDate,
    EndDate = form.EndDate,
    Budget = form.Budget ?? 0m,
    ClientId = form.ClientId,
    MemberProject = form.MembersId?.Select(memberId => new MemberProjectEntity
    {
      MemberId = memberId,
    }).ToList()
  };

  public static ProjectEntity Update(Project form, ProjectEntity entity)
  {
    entity.ProjectImage = form.ProjectImage;
    entity.Name = form.Name;
    entity.Description = form.Description!;
    entity.StartDate = form.StartDate;
    entity.EndDate = form.EndDate;
    entity.Budget = form.Budget ?? 0m;
    entity.ClientId = form.ClientId;
    entity.Client = null;
    entity.Status = form.Status;
    return entity;
  }

  public static IEnumerable<Project> CreateList(IEnumerable<ProjectEntity> entities) => entities.Select(Create);
}
