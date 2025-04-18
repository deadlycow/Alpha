using Business.Interfaces;
using Business.Models;
using Data.Entities;
using Data.Interfaces;
using Domain.Extensions;
using Domain.Models;

namespace Business.Services;
public class MpService(IMemberProjectRepository repository)
{
  private readonly IMemberProjectRepository _repository = repository;

  public async Task<IResult> Update(int id, List<string> members)
  {
    if (id < 1)
      return Result.BadRequest("ID is null.");

    await _repository.BeginTransactionAsync();
    try
    {
      var existing = await _repository.GetAllAsync(x => x.ProjectId == id);

      if (existing.Data == null)
      {
        await _repository.RollbackTransactionAsync();
        return Result.Ok("No project found.");
      }

      members ??= [];

      var updatedMembers = members.Select(x => new MemberProject
      {
        MemberId = x,
        ProjectId = id
      }).ToList();

      foreach (var member in existing.Data!)
      {
        if (!updatedMembers.Any(m => m.MemberId == member.MemberId))
          _repository.Delete(member);
      }

      foreach (var member in updatedMembers)
      {
        if (!existing.Data.Any(x => x.MemberId == member.MemberId))
          await _repository.CreateAsync(member.MapTo<MemberProjectEntity>());
      }
      await _repository.SaveAsync();
      await _repository.CommitTransactionAsync();
      return Result.Ok("Members updated successfully.");
    }
    catch (Exception ex)
    {
      await _repository.RollbackTransactionAsync();
      return Result.InternalServerError($"Error occurred while updating members: {ex.Message}");
    }

  }
}
