using Business.Factories;
using Business.Models;
using Data.Interfaces;
using Domain.Models;

namespace Business.Services;
public class ClientService(IClientRepository clientRepository)
{
  private readonly IClientRepository _clientRepository = clientRepository;
  public async Task<Result<IEnumerable<Client>>> GetAllAsync()
  {
    var response = await _clientRepository.GetAllAsync();
    if (response == null)
      return null!;
    return Result<IEnumerable<Client>>.Ok(response.Data);
  }
}
