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
    modelBuilder.Entity<AddressEntity>()
      .HasOne(a => a.Member)
      .WithOne(m => m.Address)
      .HasForeignKey<MemberEntity>(m => m.Id);

    modelBuilder.Entity<ClientEntity>()
      .HasMany(c => c.Projects)
      .WithOne(p => p.Client)
      .HasForeignKey(p => p.ClientId);

    modelBuilder.Entity<MemberEntity>()
      .HasOne(m => m.Title)
      .WithMany(t => t.Members)
      .HasForeignKey(m => m.TitleId);

    modelBuilder.Entity<MemberEntity>()
      .HasOne(m => m.Role)
      .WithMany(r => r.Members)
      .HasForeignKey(m => m.RoleId);

    modelBuilder.Entity<MemberProjectEntity>()
      .HasKey(mp => new { mp.ProjectId, mp.MemberId });

    modelBuilder.Entity<NotificationEntity>()
      .HasOne(n => n.Project)
      .WithMany(p => p.Notifications)
      .HasForeignKey(n => n.ProjectId);

    modelBuilder.Entity<NotificationEntity>()
      .HasOne(n => n.Member)
      .WithMany(m => m.Notifications)
      .HasForeignKey(n => n.MemberId);

    modelBuilder.Entity<NotificationRoleEntity>()
      .HasOne(nr => nr.Role)
      .WithMany(n => n.NotificationRoles)
      .HasForeignKey(nr => nr.RoleId)
      .OnDelete(DeleteBehavior.Restrict);

    modelBuilder.Entity<NotificationRoleEntity>()
      .HasKey(nr => new { nr.RoleId, nr.NotificationId });
  }
}
