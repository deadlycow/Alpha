using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Data.Entities;
public class ProjectEntity
{
  [Key]
  public int Id { get; set; }
  [Required]
  [Column(TypeName = "nvarchar(50)")]
  public string Name { get; set; } = null!;
  [Column(TypeName = "nvarchar(255)")]
  public string Description { get; set; } = null!;
  [Required]
  [Column(TypeName = "date")]
  public DateOnly StartDate { get; set; } = new DateOnly();
  [Column(TypeName = "date")]
  public DateOnly? EndDate { get; set; }
  [Required]
  [Column(TypeName = "money")]
  public decimal Budget { get; set; }
  [Column(TypeName = "nvarchar(255)")]
  public string? ProjectImage { get; set; }
  [Required]
  [Column(TypeName = "bit")]
  public bool Status { get; set; } = false;

  [Required]
  public int ClientId { get; set; }
  [Required]
  public ClientEntity? Client { get; set; }
  
  public ICollection<MemberProjectEntity>? MemberProject { get; set; }
  
}
