using Business.Models;
using Business.Services;
using Domain.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Pressentation_MVC.ViewModels;

namespace Pressentation_MVC.Controllers
{
  [Authorize(Roles = "Admin")]
  public class ClientsController(ClientService clientService, ImageService imageService) : Controller
  {
    private readonly ClientService _clientService = clientService;
    private readonly ImageService _imageService = imageService;

    [Authorize(Roles = "Admin")]
    [Route("clients")]
    public async Task<IActionResult> Client()
    {
      var result = await _clientService.GetAllAsync();
      if (result.Success)
        if (result is Result<IEnumerable<Client>> clients)
          return View(clients.Data);

      return View(result.ErrorMessage);
    }
    [Authorize(Roles = "Admin")]
    [HttpPost]
    public async Task<IActionResult> CreateClient([FromForm] ClientViewModel model)
    {
      if (!ModelState.IsValid)
      {
        var errors = ModelState
          .Where(x => x.Value?.Errors.Count > 0)
          .ToDictionary(
          kvp => kvp.Key,
          kvp => kvp.Value?.Errors.Select(x => x.ErrorMessage).ToList()
          );
        return BadRequest(new { Success = false, errors });
      }

      var imgUploaded = await _imageService.Upload(model.ImgFile!);
      if (imgUploaded.Success)
        model.ClientImage = imgUploaded.Message;
      else
        model.ClientImage = null;

      var client = new Client { Name = model.Name, ClientImage = model.ClientImage };

      var result = await _clientService.CreateClient(client);
      if (!result.Success)
        return BadRequest(result.ErrorMessage);

      return RedirectToAction("Client");
    }
    [Authorize(Roles = "Admin")]
    [HttpPost("clients/delete/{id}")]
    public async Task<IActionResult> DeleteClient(int id)
    {
      if (id < 1)
        return BadRequest("ID is null");

      var response = await _clientService.DeleteClient(id);
      if (response.Success)
        return RedirectToAction("Client");
      return BadRequest(response.ErrorMessage);
    }
  }
}