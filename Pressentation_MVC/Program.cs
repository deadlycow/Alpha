using Data.Contexts;
using Data.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

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
  options.LoginPath = "/auth/login";
  options.SlidingExpiration = true;
});

//builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
//    .AddCookie(options =>
//    {
//      options.LoginPath = "/Account/Login";
//      options.AccessDeniedPath = "/Account/AccessDenied";
//    });
//builder.Services.AddAuthorization();

var app = builder.Build();

app.UseHsts();

app.UseHttpsRedirection();
app.UseRouting();

//app.UseAuthentication();
app.UseAuthorization();

app.MapStaticAssets();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Project}/{id?}")
    .WithStaticAssets();

app.Run();
