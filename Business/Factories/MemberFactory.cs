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
    BirthDate = entity.BirthDate,
  };
  public static MemberEntity Create(MemberCreateForm form) => new()
  {
    FirstName = form.FirstName,
    LastName = form.LastName,
    PhoneNumber = form.PhoneNumber,
    JobTitle = form.JobTitle,
    UserName = form.Email,
    Email = form.Email,
    BirthDate = form.BirthDate,
    ProfileImage = form.ProfileImage,
    Address = form.Address != null ? new AddressEntity
    {
      Street = form.Address.Street,
      City = form.Address.City,
      PostalCode = form.Address.PostalCode,
    } : null
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
    entity.BirthDate = form.BirthDate;
    entity.Address = form.Address != null ? new AddressEntity
    {
      Street = form.Address.Street,
      City = form.Address.City,
      PostalCode = form.Address.PostalCode,
    } : null;
    return entity;
  }

  public static IEnumerable<Member> CreateList(IEnumerable<MemberEntity> entities) => entities.Select(Create);
}
