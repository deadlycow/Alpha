using Microsoft.AspNetCore.Authentication.Cookies;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllersWithViews();

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
