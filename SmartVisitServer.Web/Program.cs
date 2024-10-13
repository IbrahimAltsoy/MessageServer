using Application;
using Persistence;
using Domain;
using Infrastructure;
using Application.Abstract.Common;
using Persistence.Authentication;
using Core.Security;
using SmartVisitServer.Web;
using WebAPI;
using SmartVisitServer.Web.Services.Customers;
using SmartVisitServer.Web.Services.Panels;
using SmartVisitServer.Web.Services.OperationClaim;
using SmartVisitServer.Web.Services.AppSettings;
var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddPersistenceServices(builder.Configuration);
builder.Services.AddApplicationServices();
builder.Services.AddADomainServices();
builder.Services.AddInfrastructureServices();
builder.Services.AddSecurityServices();
builder.Services.AddWebMvcServices(builder.Configuration);

//
builder.Services.AddScoped<ICustomerService, CustomerService>();
builder.Services.AddScoped<IPanelService, PanelService>();
builder.Services.AddScoped<IOperationClaimService, OperationClaimService>();
builder.Services.AddScoped<IAppSettingsService, AppSettingsService>();
var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}
app.UseMiddleware<AuthMiddleware>();
app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Panel}/{action=Index}/{id?}");
app.UseCors();
app.Run();
