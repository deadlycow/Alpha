using Data.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;

namespace Data.Seeders;
public class DatabaseSeeder
{
  public static async Task SeedRolesAndAdminAsync(IServiceProvider serviceProvider)
  {
    using var scope = serviceProvider.CreateScope();
    var roleManager = scope.ServiceProvider.GetRequiredService<RoleManager<IdentityRole>>();
    string[] roleNames = ["Admin", "User"];

    foreach (var roleName in roleNames)
    {
      if (!await roleManager.RoleExistsAsync(roleName))
      {
        await roleManager.CreateAsync(new IdentityRole(roleName));
      }
    }

    var userManager = scope.ServiceProvider.GetRequiredService<UserManager<MemberEntity>>();
    string adminEmail = "admin@domain.com";
    var existingAdmin = await userManager.FindByEmailAsync(adminEmail);

    if (existingAdmin == null)
    {
      var adminUser = new MemberEntity
      {
        UserName = adminEmail,
        Email = adminEmail,
        FirstName = "System",
        LastName = "Admin",
      };
      var result = await userManager.CreateAsync(adminUser, "Admin!123");

      if (result.Succeeded)
        await userManager.AddToRoleAsync(adminUser, "Admin");
    }
  }
}
