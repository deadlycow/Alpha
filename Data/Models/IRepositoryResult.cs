namespace Data.Models
{
  public interface IRepositoryResult
  {
    string? ErrorMessage { get; }
    int StatusCode { get; }
    bool Success { get; }

    static abstract RepositoryResult AlreadyExists(string message);
    static abstract RepositoryResult BadRequest(string message);
    static abstract RepositoryResult Created();
    static abstract RepositoryResult InternalServerError(string message);
    static abstract RepositoryResult NotFound(string message);
    static abstract RepositoryResult Ok();
  }
  public interface IRepositoryResult<T> : IRepositoryResult
  {
    T? Data { get; }
  }
}