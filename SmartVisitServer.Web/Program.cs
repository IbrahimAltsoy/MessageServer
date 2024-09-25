using Application;
using Persistence;
using Domain;
using Infrastructure;
using Application.Abstract.Common;
using Persistence.Authentication;
using Core.Security;
using SmartVisitServer.Web;
using WebAPI;
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
    pattern: "{controller=Home}/{action=privacy}/{id?}");

app.UseCors();
app.Run();
