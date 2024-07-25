using Application.Services.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Persistence.Contexts;
using Persistence.Interceptors;
using Persistence.Repositories;
using System.Reflection;

namespace Persistence;

public static class PersistenceServiceRegistration
{
    public static IServiceCollection AddPersistenceServices(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddScoped<ISaveChangesInterceptor, AuditableEntityInterceptor>();
      
        services.AddDbContext<BaseDbContext>((sp, options) =>
        {
          options.AddInterceptors(sp.GetServices<ISaveChangesInterceptor>()); // CuurentUser ı Claimler üzerinden bul sonra burayı aç
            options.UseNpgsql(configuration.GetConnectionString("Default"));
        });
        services.AddSingleton(TimeProvider.System);// CureentUser için eklendi
       

       
        //services.AddScoped<IVisitRepository, VisitRepository>();
        var assembly = Assembly.GetExecutingAssembly();
       

        var types = assembly.GetTypes()
                            .Where(t => t.Name.EndsWith("Repository") && !t.IsInterface && !t.IsAbstract);

        foreach (var type in types)
        {
            var interfaceType = type.GetInterfaces().FirstOrDefault(i => i.Name.EndsWith("Repository"));
            if (interfaceType != null)
            {
                services.AddScoped(interfaceType, type);
            }
        }

        return services;
    }
}