using Data.Entities;
using Domain.Models;

namespace Business.Factories;
public static class AuthFactory
{
  public static MemberEntity Create(SignUpForm form) => new()
  {
    UserName = form.Email,
    Email = form.Email,
    FirstName = form.FirstName,
    LastName = form.LastName,
  };
}
