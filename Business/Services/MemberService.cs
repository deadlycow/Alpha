using Business.Factories;
using Business.Interfaces;
using Business.Models;
using Data.Entities;
using Data.Interfaces;
using Domain.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;
using Domain.Extensions;

namespace Business.Services;
public class MemberService(UserManager<MemberEntity> userManager, IMemberRepository repository)
{
  private readonly IMemberRepository _repository = repository;
  private readonly UserManager<MemberEntity> _userManager = userManager;

  public async Task<IResult> Create(MemberCreateForm form)
  {
    if (form == null)
      return Result.BadRequest("Form can't be null.");

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
      //var affectedRows = await _userManager.save;
      //if (affectedRows == 0)
      //{
      //  await _memberRepsitory.RollbackTransactionAsync();
      //  return Result.InternalServerError("No changes saved to database.");
      //}
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
  public async Task<IEnumerable<Member>> GetAllAsync()
  {
    var list = await _userManager.Users.ToListAsync();
    var test = MemberFactory.CreateList(list);
    //var members = list.Select(x => new Member
    //{
    //  Id = x.Id,
    //  FirstName = x.FirstName,
    //  LastName = x.LastName,
    //  Email = x.Email,
    //  Phone = x.PhoneNumber,
    //  JobTitle = x.JobTitle,
    //});
    return test;
  }
  public async Task<IResult> GetAsync(string id)
  {
    var entity = await _userManager.Users.FirstOrDefaultAsync(x => x.Id == id);
    if (entity == null)
      return Result.NotFound("Member not found.");
    var member = MappExtension.MapTo<Member>(entity);
    return Result<Member>.Ok(member);
  }
}
