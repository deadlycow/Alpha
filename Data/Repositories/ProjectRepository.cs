using Data.Contexts;
using Data.Entities;
using Data.Interfaces;
using Data.Models;
using Domain.Models;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace Data.Repositories;
public class ProjectRepository(DataContext context) : BaseRepository<ProjectEntity, Project>(context), IProjectRepository
{
  public override async Task<RepositoryResult<IEnumerable<Project>>> GetAllAsync(bool orderByDescending = false, Expression<Func<ProjectEntity, object>>? sortBy = null, Expression<Func<ProjectEntity, bool>>? filterBy = null, params Expression<Func<ProjectEntity, object>>[] includes)
  {
    var projectEntities = await _dbSet.Include(project => project.Client)
                                      .Include(project => project.MemberProject!)
                                      .ThenInclude(mp => mp.Member)
                                      .ToListAsync();

    var projectModels = projectEntities.Select(project => new Project
    {
      Id = project.Id,
      Name = project.Name,
      Description = project.Description,
      ProjectImage = project.ProjectImage,
      StartDate = project.StartDate,
      EndDate = project.EndDate,
      Budget = project.Budget,
      ClientId = project.ClientId,
      Client = new Client
      {
        Id = project.Client!.Id,
        Name = project.Client.Name
      },
      Members = project.MemberProject?.Select(mp => new Member
      {
        ProfileImage = mp.Member.ProfileImage,
        Id = mp.Member.Id,
        FirstName = mp.Member.FirstName,
        LastName = mp.Member.LastName,
      }).ToList()
    });

    return RepositoryResult<IEnumerable<Project>>.Ok(projectModels);
  }
  public override async Task<RepositoryResult<Project>> GetAsync(Expression<Func<ProjectEntity, bool>> filterBy, params Expression<Func<ProjectEntity, object>>[] includes)
  {
    try
    {
      var result = await _dbSet.Include(project => project.Client)
        .Include(project => project.MemberProject!)
        .ThenInclude(mp => mp.Member)
        .FirstOrDefaultAsync(filterBy);

      if (result == null)
        return RepositoryResult<Project>.NotFound("Entity");

      var entity = new Project
      {
        Id = result.Id,
        Name = result.Name,
        Description = result.Description,
        ProjectImage = result.ProjectImage,
        StartDate = result.StartDate,
        EndDate = result.EndDate,
        Budget = result.Budget,
        ClientId = result.ClientId,
        Client = new Client
        {
          Id = result.Client!.Id,
          Name = result.Client.Name
        },
        Members = result.MemberProject?.Select(mp => new Member
        {
          ProfileImage = mp.Member.ProfileImage,
          Id = mp.Member.Id,
          FirstName = mp.Member.FirstName,
          LastName = mp.Member.LastName,
        }).ToList()
      };

      return RepositoryResult<Project>.Ok(entity);
    }
    catch (Exception ex)
    {
      return RepositoryResult<Project>.InternalServerError(ex.Message);
    }
  }
}
