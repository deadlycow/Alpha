using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics.CodeAnalysis;

namespace Data.Entities;
public class MemberEntity
{
  [Key]
  public int Id { get; set; }
  [Required]
  [Column(TypeName = "nvarchar(50)")]
  public string Name { get; set; } = null!;
  [Required]
  [Column(TypeName = "nvarchar(50)")]
  public string LastName { get; set; } = null!;

  [Required]
  [Column(TypeName = "nvarchar(255)")]
  [RegularExpression(@"^[\w.-]+@[a-zA-Z\d.-]+\.[a-zA-Z]{2,}$", ErrorMessage = "Invalid email format.")]
  public string Email { get; set; } = null!;

  [RegularExpression(@"^\+?\d{7,15}$", ErrorMessage = "Invalid phone number.")]
  public string? Phone { get; set; }
  [Required]
  public DateOnly Date { get; set; }
  [Required]
  public int TitleId { get; set; }
  public TitleEntity? Title {  get; set; }

  [Required]
  public int RoleId { get; set; }
  public RoleEntity? Role { get; set; }

  [Column(TypeName = "nvarchar(255)")]
  public string? ProfileImage { get; set; }
}
