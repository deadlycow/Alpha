using Business.Factories;
using Business.Interfaces;
using Business.Models;
using Data.Interfaces;
using Domain.Models;
using System.Diagnostics;

namespace Business.Services;
public class ProjectService(IProjectRepository repository)
{
  private readonly IProjectRepository _repository = repository;

  public async Task<IResult> Create(ProjectCreateForm form)
  {
    if (form == null)
      return Result.BadRequest("Form can't be null.");

    var exists = await _repository.ExistsAsync(x => x.Name == form.Name);
    if (exists.Success)
      return Result.AlreadyExists("Project already registered.");

    await _repository.BeginTransactionAsync();
    try
    {
      var entity = ProjectFactory.Create(form);
      var respons = await _repository.CreateAsync(entity);
      if (respons.Success)
      {
        var test = await _repository.SaveAsync();
        await _repository.CommitTransactionAsync();
        return Result.Ok();
      }

      await _repository.RollbackTransactionAsync();
      return Result.BadRequest("Failed to create project.");
    }
    catch (Exception ex)
    {
      await _repository.RollbackTransactionAsync();
      Debug.WriteLine($"Error creating member: {ex.Message}");
      return Result.InternalServerError("An unexpected error occurred while creating project.");
    }
  }
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
  public async Task<IResult> GetAsync(int id)
  {
    if (id < 1)
      return Result.BadRequest("ID is null.");

    var result = await _repository.GetAsync(x => x.Id == id);
    if (result.Data == null)
      return Result.NotFound("Project not found.");
    
    var project = ProjectFactory.Create(result.Data);
    return Result<Project>.Ok(project);

  }
  public async Task<IResult> DeleteAsync(int id)
  {
    await _repository.BeginTransactionAsync();
    try
    {
      var project = await _repository.GetAsync(id);
      if (project.Data == null)
      {
        await _repository.RollbackTransactionAsync();
        return Result.NotFound($"Project with id {id} not found");
      }

      var IsDeleted = _repository.Delete(project.Data);
      if (!IsDeleted.Success)
      {
        await _repository.RollbackTransactionAsync();
        return Result.BadRequest("No changes save to the database");
      }
      await _repository.SaveAsync();
      await _repository.CommitTransactionAsync();
      return Result.Ok();
    }
    catch (Exception ex)
    {
      await _repository.RollbackTransactionAsync();
      Debug.WriteLine($"Error deleting project: {id}: {ex.Message}");
      return Result.InternalServerError("An error occurred while deleting project");
    }
  }
  public async Task<IResult> UpdateAsync(Project form)
  {
    if (form == null)
      return Result.BadRequest("Form can't be null.");

    var exists = await _repository.GetAsync(p => p.Id == form.Id, p => p.MemberProject!);
    if (!exists.Success)
      return Result.NotFound("Project not found.");

    await _repository.BeginTransactionAsync();
    try
    {
      exists.Data!.MemberProject?.Clear();
      var entity = ProjectFactory.Update(form, exists.Data);
      var respons = _repository.Update(entity);
      if (respons.Success)
      {
       var test = await _repository.SaveAsync();
        await _repository.CommitTransactionAsync();
        return Result.Ok();
      }
      await _repository.RollbackTransactionAsync();
      return Result.BadRequest("Failed to update project.");
    }
    catch (Exception ex)
    {
      await _repository.RollbackTransactionAsync();
      Debug.WriteLine($"Error updating project: {ex.Message}");
      return Result.InternalServerError("An unexpected error occurred while updating project.");
    }
  }
}
