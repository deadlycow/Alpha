using Data.Entities;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Data.Contexts;
public class DataContext(DbContextOptions<DataContext> options) : IdentityDbContext<MemberEntity>(options)
{
  public DbSet<AddressEntity> Address { get; set; }
  public DbSet<ClientEntity> Client { get; set; }
  public DbSet<MemberProjectEntity> MemberProject { get; set; }
  public DbSet<ProjectEntity> Project { get; set; }
  public DbSet<NotificationDismissedEntity> DismissedNotifications { get; set; }
  public DbSet<NotificationEntity> Notifications { get; set; }
  public DbSet<NotificationTypeEntity> NotificationTypes { get; set; }
  public DbSet<NotificationTargetGroupEntity> NotificationTargetGroups { get; set; }

  protected override void OnModelCreating(ModelBuilder modelBuilder)
  {
    base.OnModelCreating(modelBuilder);

    modelBuilder.Entity<AddressEntity>()
      .HasOne(a => a.Member)
      .WithOne(m => m.Address)
      .HasForeignKey<AddressEntity>(a => a.MemberId)
      .OnDelete(DeleteBehavior.Cascade);

    modelBuilder.Entity<ClientEntity>()
      .HasMany(c => c.Projects)
      .WithOne(p => p.Client)
      .HasForeignKey(p => p.ClientId)
      .OnDelete(DeleteBehavior.Restrict);

    modelBuilder.Entity<MemberProjectEntity>()
      .HasKey(mp => new { mp.ProjectId, mp.MemberId });

    modelBuilder.Entity<MemberProjectEntity>()
      .HasOne(mp => mp.Project)
      .WithMany(p => p.MemberProject)
      .HasForeignKey(mp => mp.ProjectId)
      .OnDelete(DeleteBehavior.Cascade);

    modelBuilder.Entity<MemberProjectEntity>()
      .HasOne(mp => mp.Member)
      .WithMany(m => m.Projects)
      .HasForeignKey(mp => mp.MemberId)
      .OnDelete(DeleteBehavior.Cascade);
  }
}
