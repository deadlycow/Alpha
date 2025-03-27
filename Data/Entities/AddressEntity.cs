using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Data.Entities;
public class AddressEntity
{
  [Key, ForeignKey("Member")]
  public string MemberId { get; set; } = null!;
  [Required]
  [Column(TypeName = "nvarchar(100)")]
  public string Street { get; set; } = null!;
  [Required]
  [Column(TypeName = "nvarchar(50)")]
  public string City { get; set; } = null!;
  [Required]
  public int PostalCode { get; set; }

  public MemberEntity Member { get; set; } = null!;
}
