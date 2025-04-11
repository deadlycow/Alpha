using Business.Models;
using Business.Services;
using Domain.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Pressentation_MVC.Controllers
{
  [Authorize(Roles = "Admin")]
  public class ClientsController(ClientService clientService) : Controller
  {
    private readonly ClientService _clientService = clientService;

    [Route("clients")]
    public async Task<IActionResult> Client()
    {
      var result = await _clientService.GetAllAsync();
      if (result.Success)
        if (result is Result<IEnumerable<Client>> clients)
          return View(clients);

      return View(result.ErrorMessage);
    }
  }
}