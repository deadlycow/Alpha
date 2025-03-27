﻿namespace Domain.Models;
public class Member
{
  public string Id { get; set; } = null!;
  public string? FirstName { get; set; }
  public string? LastName { get; set; }
  public string? Email { get; set; }
  public string? Phone { get; set; }
  public string? JobTitle { get; set; }
  public MemberAddress Address { get; set; } = new();
}
