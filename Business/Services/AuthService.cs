using Business.Factories;
using Data.Entities;
using Domain.Models;
using Microsoft.AspNetCore.Identity;

namespace Business.Services;
public class AuthService(SignInManager<MemberEntity> signInManager, UserManager<MemberEntity> userManager)
{
  private readonly SignInManager<MemberEntity> _signInManager = signInManager;
  private readonly UserManager<MemberEntity> _userManager = userManager;

  public async Task<bool> SignInAsync(SignInForm form)
  {
    var result = await _signInManager.PasswordSignInAsync(form.Email, form.Password, false, false);
    return result.Succeeded;
  }

  public async Task<IdentityResult> SignUpAsync(SignUpForm form)
  {
    var existingUser = await _userManager.FindByEmailAsync(form.Email);
    if (existingUser != null)
      return IdentityResult.Failed(new IdentityError { Description = "Email is already registered. Try logging in instead." });

    var entity = AuthFactory.Create(form);
    var result = await _userManager.CreateAsync(entity, form.Password);
    if (result.Succeeded)
      await _userManager.AddToRoleAsync(entity, "User");

    return result;
  }

  public async Task LogoutAsync()
  {
    await _signInManager.SignOutAsync();
  }
}