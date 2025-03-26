using Data.Entities;
using Domain.Models;
using Microsoft.AspNetCore.Identity;

namespace Business.Services;
public class AuthService(SignInManager<MemberEntity> signInManager, UserManager<MemberEntity> userManager)
{
  private readonly SignInManager<MemberEntity> _signInManager = signInManager;
  private readonly UserManager<MemberEntity> _userManager = userManager;


  public async Task<bool> LoginAsync(MemberLoginForm loginForm)
  {
    var result = await _signInManager.PasswordSignInAsync(loginForm.Email, loginForm.Password, false, false);

    return result.Succeeded;
  }

  public async Task<IdentityResult> SignUpAsync(MemberSignUpForm signupForm)
  {
    var existingUser = await _userManager.FindByEmailAsync(signupForm.Email);
    if (existingUser != null)
      return IdentityResult.Failed(new IdentityError { Description = "Email is alredy in use" });

    var memberEntity = new MemberEntity
    {
      UserName = signupForm.Email,
      Email = signupForm.Email,
      FirstName = signupForm.FirstName,
      LastName = signupForm.LastName,
    };

    return await _userManager.CreateAsync(memberEntity, signupForm.Password);
  }

  public async Task LogoutAsync()
  {
    await _signInManager.SignOutAsync();
  }
}

//3.03.59
