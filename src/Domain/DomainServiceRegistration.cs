using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace Domain;

public static class DomainServiceRegistration
{
    public static IServiceCollection AddADomainServices(this IServiceCollection services)
    {
        services.AddAutoMapper(Assembly.GetExecutingAssembly());
        return services;
    }
}
