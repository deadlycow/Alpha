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

  public static IEnumerable<Project> CreateList(IEnumerable<ProjectEntity> entities) => entities.Select(Create);
}
