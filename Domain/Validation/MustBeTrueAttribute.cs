using System.ComponentModel.DataAnnotations;

namespace Domain;
public class MustBeTrueAttribute : ValidationAttribute
{
  public override bool IsValid(object? value)
  {
    return value is bool b && b;
  }
  public override string FormatErrorMessage(string name)
  {
    return $"{name} must be accepted.";
  }
}