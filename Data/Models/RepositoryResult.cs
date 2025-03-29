namespace Data.Models;
public abstract class RepositoryResult : IRepositoryResult
{
  public bool Success { get; protected set; }
  public int StatusCode { get; protected set; }
  public string? ErrorMessage { get; protected set; }

  public static RepositoryResult Ok() => new SuccessResult(200);
  public static RepositoryResult Created() => new SuccessResult(201);
  public static RepositoryResult BadRequest(string message) => new ErrorResult(400, message);
  public static RepositoryResult NotFound(string message) => new ErrorResult(404, message);
  public static RepositoryResult AlreadyExists(string message) => new ErrorResult(409, message);
  public static RepositoryResult InternalServerError(string message) => new ErrorResult(500, message);
}

public class RepositoryResult<T> : RepositoryResult , IRepositoryResult<T> where T : class
{
  public T? Data { get; private set; }
  public static RepositoryResult<T> Ok(T? data) => new()
  {
    Success = true,
    StatusCode = 200,
    Data = data 
  };
  public static RepositoryResult<T> Created(T? data) => new()
  {
    Success = true,
    StatusCode = 201,
    Data = data
  };
}

