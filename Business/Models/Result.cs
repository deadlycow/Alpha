using Business.Interfaces;

namespace Business.Models;
public abstract class Result : IResult
{
  public bool Success { get; protected set; }
  public int StatusCode { get; protected set; }
  public string? Message { get; protected set; }
  public string? ErrorMessage { get; protected set; }

  public static Result Ok() => new SuccessResult(200);
  public static Result Ok(string? message) => new SuccessResult(200, message);
  public static Result Created() => new SuccessResult(201);
  public static Result Created(string? message) => new SuccessResult(201, message);
  public static Result BadRequest(string message) => new ErrorResult(400, message);
  public static Result NotFound(string message) => new ErrorResult(404, message);
  public static Result AlreadyExists(string message) => new ErrorResult(409, message);
  public static Result InternalServerError(string message) => new ErrorResult(500, message);
}

public class Result<T> : Result, IResult<T>
{
  public T? Data { get; private set; }
  public static Result<T> Ok(T? data)
  {
    return new Result<T>
    {
      Success = true,
      StatusCode = 200,
      Data = data
    };
  }
  public static Result<T> Created(T? data)
  {
    return new Result<T>
    {
      Success = true,
      StatusCode = 201,
      Data = data
    };
  }
}

public class SuccessResult : Result
{
  public SuccessResult(int statusCode, string? message = null)
  {
    Success = true;
    Message = message;
    StatusCode = statusCode;
  }
}
public class ErrorResult : Result
{
  public ErrorResult(int statusCode, string errorMessage)
  {
    Success = false;
    StatusCode = statusCode;
    ErrorMessage = errorMessage;
  }
}
