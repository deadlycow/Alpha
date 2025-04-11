using Business.Factories;
using Business.Interfaces;
using Business.Models;
using Data.Interfaces;
using Domain.Models;

namespace Business.Services;
public class ClientService(IClientRepository clientRepository)
{
  private readonly IClientRepository _clientRepository = clientRepository;
  public async Task<IResult> GetAllAsync()
  {
    var response = await _clientRepository.GetAllAsync();
    if (response == null)
      return Result.NotFound("No Clients found");
    return Result<IEnumerable<Client>>.Ok(response.Data);
  }
}
