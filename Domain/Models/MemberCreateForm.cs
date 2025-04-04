﻿using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations;

namespace Domain.Models;
public class MemberCreateForm
{
  [DataType(DataType.Upload)]
  public IFormFile? MemberImage { get; set; }

  [Display(Name = "First Name", Prompt = "Your first name")]
  [DataType(DataType.Text)]
  [Required(ErrorMessage = "Required")]
  public string FirstName { get; set; } = null!;

  [Display(Name = "Last Name", Prompt = "Your last name")]
  [DataType(DataType.Text)]
  [Required(ErrorMessage = "Required")]
  public string LastName { get; set; } = null!;

  [Display(Name = "Email", Prompt = "Your email address")]
  [DataType(DataType.EmailAddress)]
  [Required(ErrorMessage = "Required")]
  [RegularExpression(@"^[^@\s]+@[^@\s]+\.[^@\s]+$", ErrorMessage = "Invalid email")]
  public string Email { get; set; } = null!;

  [Display(Name = "Phone", Prompt = "Your phone number")]
  public string? Phone { get; set; }

  [Display(Name = "Job Title", Prompt = "Your job title")]
  [DataType(DataType.Text)]
  [Required(ErrorMessage = "Required")]
  public string? JobTitle { get; set; }

  [Display(Name = "Address", Prompt = "Your address")]
  [DataType(DataType.Text)]
  public string? Address { get; set; }

  [Display(Name = "Date of birth")]
  [DataType(DataType.Text)]
  public DateOnly? DateOfBirth { get; set; }
}
