namespace Business.Interfaces;
public interface IResult
{
  bool Success { get; }
  int StatusCode { get; }
  string? ErrorMessage { get; }
}
public interface IResult<T> : IResult
{
  T? Data { get; }
}