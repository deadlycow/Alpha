namespace Data.Models;

public class ErrorResult : RepositoryResult
{
  public ErrorResult(int statusCode, string errorMessage)
  {
    Success = false;
    StatusCode = statusCode;
    ErrorMessage = errorMessage;
  }
}