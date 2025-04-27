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
      Status = project.Status,
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
  public override async Task<RepositoryResult<ProjectEntity>> GetAsync(Expression<Func<ProjectEntity, bool>> filterBy, params Expression<Func<ProjectEntity, object>>[] includes)
  {
    try
    {
      var result = await _dbSet.AsNoTracking().Include(project => project.Client)
        .Include(project => project.MemberProject!)
        .ThenInclude(mp => mp.Member)
        .FirstOrDefaultAsync(filterBy);

      if (result == null)
        return RepositoryResult<ProjectEntity>.NotFound("Entity");

      var entity = new ProjectEntity
      {
        Id = result.Id,
        Name = result.Name,
        Description = result.Description,
        ProjectImage = result.ProjectImage,
        StartDate = result.StartDate,
        EndDate = result.EndDate,
        Budget = result.Budget,
        ClientId = result.ClientId,
        Client = new ClientEntity
        {
          Id = result.Client!.Id,
          Name = result.Client.Name
        },
        MemberProject = result.MemberProject?.Select(mp => new MemberProjectEntity
        {
          MemberId = mp.MemberId,
          ProjectId = mp.ProjectId,
          Member = new MemberEntity
          {
            ProfileImage = mp.Member.ProfileImage,
            Id = mp.Member.Id,
            FirstName = mp.Member.FirstName,
            LastName = mp.Member.LastName,
          }
        }).ToList()

      };

      return RepositoryResult<ProjectEntity>.Ok(entity);
    }
    catch (Exception ex)
    {
      return RepositoryResult<ProjectEntity>.InternalServerError(ex.Message);
    }
  }
}
