using Business.Interfaces;
using Business.Models;
using Data.Entities;
using Data.Interfaces;
using Domain.Models;
using System.Diagnostics;

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

  public async Task<IResult> CreateClient(Client model)
  {
    if (model == null)
      return Result.BadRequest("Form can't be null.");

    await _clientRepository.BeginTransactionAsync();
    try
    {
      var entity = new ClientEntity { Name = model.Name, ClientImage = model.ClientImage };

      var res = await _clientRepository.CreateAsync(entity);
      if (res.Success)
      {
        await _clientRepository.SaveAsync();
        await _clientRepository.CommitTransactionAsync();
        return Result.Ok();
      }

      await _clientRepository.RollbackTransactionAsync();
      return Result.BadRequest("Failed to create client");
    }
    catch (Exception ex)
    {
      await _clientRepository.RollbackTransactionAsync();
      Debug.WriteLine($"Error creating client: {ex.Message}");
      return Result.InternalServerError("An unexpected error occurred while creating client.");
    }
  }

  public async Task<IResult> DeleteClient(int id)
  {
    await _clientRepository.BeginTransactionAsync();

    try
    {
      var entity = await _clientRepository.GetAsync(id);
      if (entity == null)
      {
        await _clientRepository.RollbackTransactionAsync();
        return Result.NotFound($"Client with id {id} not found");
      }

      var isDeleted = _clientRepository.Delete(entity.Data!);
      if (!isDeleted.Success)
      {
        await _clientRepository.RollbackTransactionAsync();
        return Result.BadRequest("No changes saved to the database");
      }

      await _clientRepository.SaveAsync();
      await _clientRepository.CommitTransactionAsync();
      return Result.Ok();
    }
    catch (Exception ex) { 
      await _clientRepository.RollbackTransactionAsync();
      Debug.WriteLine($"Error deleting client: {id}: {ex.Message}");
      return Result.InternalServerError("An error occurred while deleting client.");
    }
  }
}
