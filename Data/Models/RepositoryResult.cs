namespace Data.Models;

public class RepositoryResult<T>
{
  public bool Success { get; set; }
  public int StatusCode { get; set; }
  public string? ErrorMessage { get; set; }
  public T? Data { get; set; }

  public static RepositoryResult<T> Ok(T? data = default) => new() { Success = true, StatusCode = 200 , Data = data};
  public static RepositoryResult<T> Created() => new() { Success = true, StatusCode = 201 };
  public static RepositoryResult<T> BadRequest(string message) => new() { Success = false, StatusCode = 400, ErrorMessage = $"{message} can't be null." };
  public static RepositoryResult<T> NotFound(string message) => new() { Success = false, StatusCode = 404, ErrorMessage = $"{message} not found" };
  public static RepositoryResult<T> AlreadyExists() => new() { Success = false, StatusCode = 409, ErrorMessage = "Already exists" };
  public static RepositoryResult<T> InternalServerError(string message) => new() { Success = false, StatusCode = 500, ErrorMessage = message };

}

