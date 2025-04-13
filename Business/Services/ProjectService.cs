using Business.Interfaces;
using Business.Models;
using Data.Entities;
using Data.Interfaces;
using Data.Repositories;
using Domain.Models;
using System.Diagnostics;
using System.Linq.Expressions;

namespace Business.Services;
public class ProjectService(IProjectRepository repository)
{
  private readonly IProjectRepository _repository = repository;

  public async Task<IResult> GetAllAsync()
  {
    try
    {
      var result = await _repository.GetAllAsync();
        
      return result.Success
        ? Result<IEnumerable<Project>>.Ok(result.Data)
        : new Result<IEnumerable<Member>>();
    }
    catch (Exception ex)
    {
      Debug.WriteLine(ex.Message);
      return Result.InternalServerError("Error occurred while fetching members");
    }
  }
}
