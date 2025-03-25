using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Data.Entities;
public class MemberEntity : IdentityUser
{
  [ProtectedPersonalData]
  [Required]
  [Column(TypeName = "nvarchar(50)")]
  public string FirstName { get; set; } = null!;
  [ProtectedPersonalData]
  [Required]
  [Column(TypeName = "nvarchar(50)")]
  public string LastName { get; set; } = null!;
  [ProtectedPersonalData]
  [Required]
  public DateOnly BirthDate { get; set; }
  [Required]
  public string Title { get; set; } = null!;
  [ProtectedPersonalData]
  [Column(TypeName = "nvarchar(255)")]
  public string? ProfileImage { get; set; }
  [ProtectedPersonalData]
  public AddressEntity? Address { get; set; }
}
