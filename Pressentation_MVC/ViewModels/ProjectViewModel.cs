using Domain.Models;

namespace Pressentation_MVC.ViewModels;
public class ProjectViewModel
{
  public string? ProjectImage { get; set; }
  public int Id { get; set; }
  public string Name { get; set; } = null!;
  public Client Client { get; set; } = null!;
  public string? Description { get; set; }
  public DateOnly StartDate { get; set; } = new DateOnly();
  public DateOnly? EndDate { get; set; }
  public IEnumerable<MemberViewModel> Members { get; set; } = [];
  public decimal Budget { get; set; }

  public string TimeLeft
  {
    get
    {
      if (EndDate == null)
        return "End date not set";

      var endDate = EndDate!.Value.ToDateTime(TimeOnly.MinValue);
      var daysLeft = (endDate - DateTime.Now).Days;

      return daysLeft switch
      {
        > 6 => $"{(daysLeft / 7)} week{(daysLeft / 7 > 1 ? "s" : "")} left",
        > 0 => $"{daysLeft} day{(daysLeft > 1 ? "s" : "")} left",
        0 => "Last day",
        _ => "Expired"
      };
      //var daysLeft = (EndDate.Date - DateTime.Now.Date).Days;

      //if (daysLeft > 7)
      //{
      //  var weeks = daysLeft / 7;
      //  return $"{weeks} week{(weeks > 1 ? "s" : "")} left";
      //}
      //else if (daysLeft > 0)
      //{
      //  return $"{daysLeft} day{(daysLeft > 1 ? "s" : "")} left";
      //}
      //else
      //{
      //  return "Expired";
      //}
    }
  }
}
