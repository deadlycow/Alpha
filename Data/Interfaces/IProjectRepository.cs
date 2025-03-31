using Domain.Models;
using Data.Entities;

namespace Data.Interfaces;
public interface IProjectRepository : IBaseRepository<ProjectEntity, Project>
{
}