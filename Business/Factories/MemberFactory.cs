using Data.Entities;
using Domain.Models;

namespace Business.Factories;
public static class MemberFactory
{
  public static Member Create(MemberEntity entity) => new()
  {
    Id = entity.Id,
    FirstName = entity.FirstName,
    LastName = entity.LastName,
    PhoneNumber = entity.PhoneNumber,
    JobTitle = entity.JobTitle,
    Email = entity.Email!,
    ProfileImage = entity.ProfileImage,
  };
  public static MemberEntity Create(MemberCreateForm form) => new()
  {
    FirstName = form.FirstName,
    LastName = form.LastName,
    PhoneNumber = form.PhoneNumber,
    JobTitle = form.JobTitle,
    UserName = form.Email,
    Email = form.Email,
    ProfileImage = form.ProfileImage,
  };
  public static MemberEntity Update(Member form, MemberEntity entity)
  {
    entity.ProfileImage = form.ProfileImage;
    entity.FirstName = form.FirstName!;
    entity.LastName = form.LastName!;
    entity.PhoneNumber = form.PhoneNumber;
    entity.JobTitle = form.JobTitle;
    entity.UserName = form.Email;
    entity.Email = form.Email;
    return entity;
  }




  public static IEnumerable<Member> CreateList(IEnumerable<MemberEntity> entities) => entities.Select(Create);
}
