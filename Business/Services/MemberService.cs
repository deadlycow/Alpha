using Business.Factories;
using Business.Interfaces;
using Business.Models;
using Data.Entities;
using Data.Interfaces;
using Domain.Models;
using Domain.Extensions;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;
using System.Security.Claims;

namespace Business.Services;
public class MemberService(UserManager<MemberEntity> userManager, IMemberRepository repository)
{
  private readonly IMemberRepository _repository = repository;
  private readonly UserManager<MemberEntity> _userManager = userManager;

  public async Task<IResult> GetUserAsync(ClaimsPrincipal user)
  {
    var userId = user.FindFirstValue(ClaimTypes.NameIdentifier);
    if (userId == null)
      return Result.NotFound("User not found");
    var entity = await _userManager.Users.FirstOrDefaultAsync(x => x.Id == userId);
    if (entity == null)
      return Result.NotFound("User not found");
    var member = entity.MapTo<Member>();
    return Result<Member>.Ok(member);
  }
  public async Task<IResult> Create(MemberCreateForm form)
  {
    if (form == null)
      return Result.BadRequest("Form can't be null.");

    var exists = await _userManager.FindByEmailAsync(form.Email);
    if (exists != null)
      return Result.AlreadyExists("Email is already registered.");

    await _repository.BeginTransactionAsync();
    try
    {
      var newMember = MemberFactory.Create(form);
      var respons = await _userManager.CreateAsync(newMember, "Bytmig123!");
      if (respons.Succeeded)
      {
        await _userManager.AddToRoleAsync(newMember, "User");
        await _repository.CommitTransactionAsync();
        return Result.Ok();
      }

      await _repository.RollbackTransactionAsync();
      return Result.BadRequest("Failed to create member.");
    }
    catch (Exception ex)
    {
      await _repository.RollbackTransactionAsync();
      Debug.WriteLine($"Error creating member: {ex.Message}");
      return Result.InternalServerError("An unexpected error occurred while creating member.");
    }
  }
  public async Task<IResult> GetAllAsync()
  {
    try
    {
      var members = await _userManager.Users.ToListAsync();
      return members.Count > 0
        ? Result<IEnumerable<Member>>.Ok(MemberFactory.CreateList(members))
        : new Result<IEnumerable<Member>>();
    }
    catch (Exception ex)
    {
      Debug.WriteLine(ex.Message);
      return Result.InternalServerError("Error occurred while fetching members");
    }
  }
  public async Task<IResult> GetAsync(string id)
  {
    var entity = await _userManager.Users.Include(a => a.Address).FirstOrDefaultAsync(x => x.Id == id);
    if (entity == null)
      return Result.NotFound("Member not found.");
    
    var member = entity.MapTo<Member>();
    member.Address = entity.Address?.MapTo<Address>();
    return Result<Member>.Ok(member);
  }
  public async Task<IResult> UpdateAsync(Member form)
  {
    if (form == null)
      return Result.BadRequest("Form can't be null");
    await _repository.BeginTransactionAsync();
    try
    {
      var exists = await _userManager.Users.Include(a => a.Address).FirstOrDefaultAsync(x => x.Id == form.Id);
      if (exists == null)
        return Result.NotFound("Member not found");

      exists = MemberFactory.Update(form, exists);
      var result = await _userManager.UpdateAsync(exists);
      if (!result.Succeeded)
      {
        await _repository.RollbackTransactionAsync();
        return Result.InternalServerError("No changes where made");
      }
      await _repository.CommitTransactionAsync();
      return Result.Ok();
    }
    catch (Exception ex)
    {
      await _repository.RollbackTransactionAsync();
      Debug.WriteLine($"Error updating member: {ex.Message}");
      return Result.InternalServerError("An error occurred while updating member");
    }
  }
  public async Task<IResult> DeleteAsync(string id)
  {
    await _repository.BeginTransactionAsync();
    try
    {
      var member = await _userManager.Users.FirstOrDefaultAsync(x => x.Id == id);
      if (member == null)
      {
        await _repository.RollbackTransactionAsync();
        return Result.NotFound($"Member with id {id} not found");
      }

      var IsDeleted = await _userManager.DeleteAsync(member);
      if (!IsDeleted.Succeeded)
      {
        await _repository.RollbackTransactionAsync();
        return Result.BadRequest("No changes save to the database");
      }
      await _repository.CommitTransactionAsync();
      return Result.Ok();
    }
    catch (Exception ex)
    {
      await _repository.RollbackTransactionAsync();
      Debug.WriteLine($"Error deleting member: {id}: {ex.Message}");
      return Result.InternalServerError("An error occurred while deleting member");
    }
  }
}
