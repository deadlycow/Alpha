using Data.Entities;
using Domain.Models;

namespace Business.Factories;
public static class ClientFactory
{
  public static Client Create(ClientEntity entity)
  {
    return new Client
    {
      Name = entity.Name,
    };
  }
  public static IEnumerable<Client> CreateList(IEnumerable<ClientEntity> entities) => entities.Select(Create);
}
