﻿using Business.Factories;
using Business.Interfaces;
using Business.Models;
using Data.Entities;
using Data.Interfaces;
using Domain.Models;
using Domain.Extensions;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;
namespace Business.Services;
public class MemberService(UserManager<MemberEntity> userManager, IMemberRepository repository)
{
  private readonly IMemberRepository _repository = repository;
  private readonly UserManager<MemberEntity> _userManager = userManager;

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
    var entity = await _userManager.Users.FirstOrDefaultAsync(x => x.Id == id);
    if (entity == null)
      return Result.NotFound("Member not found.");
    var member = MappExtension.MapTo<Member>(entity);
    return Result<Member>.Ok(member);
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
