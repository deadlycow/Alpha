using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Data.Entities;
public class AddressEntity
{
  [Key]
  public int Id { get; set; }
  [Required]
  [Column(TypeName = "nvarchar(100)")]
  public required string Street { get; set; }
  [Required]
  [Column(TypeName = "nvarchar(50)")]
  public required string City { get; set; }
  [Required]
  public int PostalCode { get; set; }

  public MemberEntity? Member { get; set; }
}
