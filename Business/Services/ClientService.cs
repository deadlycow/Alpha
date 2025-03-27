using Business.Factories;
using Data.Interfaces;
using Domain.Models;
using System.Collections;
using System.Transactions;

namespace Business.Services;
public class ClientService(IClientRepository clientRepository)
{
  private readonly IClientRepository _clientRepository = clientRepository;
  public async Task<IEnumerable<Client>> GetAllAsync()
  {
    var response = await _clientRepository.GetAllAsync();
    if (response == null)
      return null!;
    return ClientFactory.CreateList(response);
  }
}
