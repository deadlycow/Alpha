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
    Phone = entity.PhoneNumber,
    JobTitle = entity.JobTitle,
  };

  public static IEnumerable<Member> CreateList(IEnumerable<MemberEntity> entities) => entities.Select(Create);
}
