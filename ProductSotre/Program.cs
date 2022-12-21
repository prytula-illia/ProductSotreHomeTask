using DAL.Data;
using DAL.Data.Repository;
using DAL.Entities;
using Microsoft.EntityFrameworkCore;
using ProductSotre.Middleware;
using Serilog;
using Microsoft.AspNetCore.Identity;
using ProductSotre.Data;
using System.Configuration;
using Microsoft.AspNetCore.Authentication.OpenIdConnect;
using Microsoft.Identity.Web;
using Microsoft.Identity.Web.UI;
using Microsoft.AspNetCore.Http;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<ProductSotreContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("ProductSotreContext") ?? throw new InvalidOperationException("Connection string 'ProductSotreContext' not found.")));

builder.Services.AddDbContext<PSContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("PSContextConnection") ?? throw new InvalidOperationException("Connection string 'ProductSotreContext' not found.")));

builder.Services.AddDefaultIdentity<IdentityUser>(options => options.SignIn.RequireConfirmedAccount = true)
    .AddRoles<IdentityRole>()
    .AddEntityFrameworkStores<PSContext>();

builder.Services.AddScoped<IProductsRepository, ProductsRepository>();
builder.Services.AddScoped<ICategoriesRepository, CategoriesRepository>();
builder.Services.AddScoped<IRepository<Supplier>, SupplierRepository>();

// builder.Services.Configure<CookiePolicyOptions>(options =>
// {
//     // This lambda determines whether user consent for non-essential cookies is needed for a given request.
//     options.CheckConsentNeeded = context => true;
//     options.MinimumSameSitePolicy = SameSiteMode.Unspecified;
//     // Handling SameSite cookie according to https://learn.microsoft.com/aspnet/core/security/samesite?view=aspnetcore-3.1
//     options.HandleSameSiteCookieCompatibility();
// });

// Configuration to sign-in users with Azure AD B2C
// builder.Services.AddMicrosoftIdentityWebAppAuthentication(builder.Configuration, "AzureAdB2C");

builder.Services.AddControllersWithViews();
//    .AddMicrosoftIdentityUI();
builder.Services.AddResponseCaching();

builder.Services.AddRazorPages();
var mvcBuilder = builder.Services.AddRazorPages();
// Configuring appsettings section AzureAdB2C, into IOptions
// builder.Services.AddOptions();
// builder.Services.Configure<OpenIdConnectOptions>(builder.Configuration.GetSection("AzureAdB2C"));


Log.Logger = new LoggerConfiguration()
    .WriteTo.File($"{AppDomain.CurrentDomain.BaseDirectory}logs\\log.txt")
    .CreateLogger();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
    mvcBuilder.AddRazorRuntimeCompilation();
}

app.UseCors();
app.UseResponseCaching();

app.UseImageCaching();

app.UseHttpsRedirection();
app.UseStaticFiles();

// app.UseCookiePolicy();
app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();

app.UseEndpoints(endpoints =>
{
    endpoints.MapControllerRoute(
        name: "default",
        pattern: "{controller=Home}/{action=Index}/{id?}");
    endpoints.MapRazorPages();
});

Log.Information($"Application started. Application path: {AppDomain.CurrentDomain.BaseDirectory}");
app.Run();