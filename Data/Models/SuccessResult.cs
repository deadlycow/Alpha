namespace Data.Models;

public class SuccessResult : RepositoryResult
{
  public SuccessResult(int statusCode)
  {
    Success = true;
    StatusCode = statusCode;
  }
}
