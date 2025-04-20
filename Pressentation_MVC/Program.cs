using Business.Services;
using Data.Contexts;
using Data.Entities;
using Data.Interfaces;
using Data.Repositories;
using Data.Seeders;
using Domain.Models;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddControllersWithViews();

builder.Services.AddDbContext<DataContext>(x => x.UseSqlServer(builder.Configuration.GetConnectionString("dbAlpha")));

builder.Services.AddIdentity<MemberEntity, IdentityRole>(options =>
{
  options.SignIn.RequireConfirmedAccount = false;
  options.User.RequireUniqueEmail = true;
  options.Password.RequiredLength = 8;
})
  .AddEntityFrameworkStores<DataContext>()
  .AddDefaultTokenProviders();

builder.Services.ConfigureApplicationCookie(options =>
{
  options.LoginPath = "/Auth/SignIn";
  options.Cookie.SameSite = SameSiteMode.None;
  options.Cookie.SecurePolicy = CookieSecurePolicy.Always;
  options.ExpireTimeSpan = TimeSpan.FromMinutes(30);
  options.SlidingExpiration = true;
});

builder.Services.AddAuthentication(options => { options.DefaultScheme = CookieAuthenticationDefaults.AuthenticationScheme; })
  .AddCookie()
  .AddGoogle(options =>
  {
    options.ClientId = builder.Configuration["Authentication:Google:ClientId"]!;
    options.ClientSecret = builder.Configuration["Authentication:Google:ClientSecret"]!;
  })
  .AddGitHub(options =>
  {
    options.ClientId = builder.Configuration["Authentication:GitHub:ClientId"]!;
    options.ClientSecret = builder.Configuration["Authentication:GitHub:ClientSecret"]!;
    options.Scope.Add("user:email");
    options.Scope.Add("read:user");

    options.Events.OnCreatingTicket = async context =>
    {
      await Task.Delay(0);
      if (context.User.TryGetProperty("name", out var name))
      {
        var fullname = name.GetString();
        if (!string.IsNullOrEmpty(fullname))
        {
          var names = fullname.Split(' ', 2, StringSplitOptions.RemoveEmptyEntries);
          if (names.Length > 0)
          {
            context.Identity?.AddClaim(new Claim(ClaimTypes.GivenName, names[0]));
          }
          if (names.Length > 1)
          {
            context.Identity?.AddClaim(new Claim(ClaimTypes.Surname, names[1]));
          }
        }
      }
    };
  })
  .AddMicrosoftAccount(options =>
  {
    options.ClientId = builder.Configuration["Authentication:Microsoft:ClientId"]!;
    options.ClientSecret = builder.Configuration["Authentication:Microsoft:ClientSecret"]!;
    
  });


builder.Services.AddScoped<AuthService>();
builder.Services.AddScoped<MemberService>();
builder.Services.AddScoped<ClientService>();
builder.Services.AddScoped<ProjectService>();
builder.Services.AddScoped<ImageService>();
builder.Services.AddScoped<MpService>();

builder.Services.AddScoped<IAddressRepository, AddressRepository>();
builder.Services.AddScoped<IClientRepository, ClientRepository>();
builder.Services.AddScoped<IMemberRepository, MemberRepository>();
builder.Services.AddScoped<IProjectRepository, ProjectRepository>();
builder.Services.AddScoped<IMemberProjectRepository, MemberProjectRepository>();

var app = builder.Build();

app.UseHsts();

app.UseHttpsRedirection();
app.UseRouting();

await DatabaseSeeder.SeedRolesAndAdminAsync(app.Services);

app.UseAuthentication();
app.UseAuthorization();

app.MapStaticAssets();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Projects}/{action=Project}/{id?}")
    .WithStaticAssets();

app.Run();
