namespace Domain.Models;
public class Address
{
  public string MemberId { get; set; } = null!;
  public string Street { get; set; } = null!;
  public string City { get; set; } = null!;
  public int PostalCode { get; set; }
  public Member Member { get; set; } = null!;
}
