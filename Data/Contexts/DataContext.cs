using Data.Entities;
using Microsoft.EntityFrameworkCore;

namespace Data.Contexts;
public class DataContext(DbContextOptions options) : DbContext(options)
{
  public DbSet<AddressEntity> Address { get; set; }
  public DbSet<ClientEntity> Client { get; set; }
  public DbSet<MemberEntity> Member { get; set; }
  public DbSet<MemberProjectEntity> MemberProject { get; set; }
  public DbSet<NotificationEntity> Notification { get; set; }
  public DbSet<NotificationRoleEntity> NotificationRole { get; set; }
  public DbSet<ProjectEntity> Project { get; set; }
  public DbSet<RoleEntity> Role { get; set; }
  public DbSet<TitleEntity> Title { get; set; }

  protected override void OnModelCreating(ModelBuilder modelBuilder)
  {
  }
}
