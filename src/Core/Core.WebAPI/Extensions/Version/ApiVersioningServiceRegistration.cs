using Microsoft.Extensions.DependencyInjection;

namespace Core.WebAPI.Extensions.Version;


// TODO: Init api version system
public static class ApiVersioningServiceRegistration
{
    public static IServiceCollection ConfigureVersioning(this IServiceCollection services)
    {
        //services.AddApiVersioning();
        return services;
    }
}
