using Business.Interfaces;
using Business.Models;
using Microsoft.AspNetCore.Http;
using System.Diagnostics;

namespace Business.Services;
public class ImageService
{
  public async Task<IResult> Upload(IFormFile file, string? oldImagePath = null)
  {
    if (file == null || file.Length == 0)
      return Result.BadRequest("File can't be null or empty");

    var allowedExtensions = new[] { ".svg", ".jpg", ".jpeg", ".png", ".gif", ".webp" };
    var fileExtensions = Path.GetExtension(file.FileName).ToLower();
    if (!allowedExtensions.Contains(fileExtensions))
      return Result.BadRequest("Ivalid file type. Only images are allowed");

    var uploadsPath = Path.Combine("wwwroot", "images", "uploads");
    var uploadsFolderPath = Path.Combine(Directory.GetCurrentDirectory(), uploadsPath);

    if (!Directory.Exists(uploadsFolderPath))
      Directory.CreateDirectory(uploadsFolderPath);

    var uniqueFileName = $"{Path.GetFileNameWithoutExtension(file.FileName)}_{Guid.NewGuid():N}{fileExtensions}";

    var fullPath = Path.Combine(uploadsFolderPath, uniqueFileName);
    try
    {
      if (!string.IsNullOrEmpty(oldImagePath))
      {
        var oldFilePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", oldImagePath.TrimStart('/'));
        if (File.Exists(oldFilePath))
          File.Delete(oldFilePath);
      }

      using (var stream = new FileStream(fullPath, FileMode.Create))
      {
        await file.CopyToAsync(stream);
      }
      string webPath = Path.Combine("/", "images", "uploads", uniqueFileName).Replace("\\", "/");
      return Result.Ok(webPath);
    }
    catch (Exception ex)
    {
      Debug.WriteLine($"Error uploading image: {ex.Message}");
      return Result.InternalServerError("An error occurred while uploading image");
    }
  }
}
