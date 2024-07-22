using Application.Services.PublicHolidaysService;
using Application.Services.SmsService;
using Infrastructure.Adapter.MasSms;
using Infrastructure.Adapter.PublicHolidays;
using Microsoft.Extensions.DependencyInjection;

namespace Infrastructure;

public static class InfrastructureServiceRegistration
{
    public static IServiceCollection AddInfrastructureServices(this IServiceCollection services)
    {
        services.AddHttpClient("MasSms", c => c.BaseAddress = new Uri("https://api.senagsm.com.tr/api/"));
        services.AddHttpClient("PublicHolidays", c => c.BaseAddress = new Uri("https://api.ubilisim.com/resmitatiller/"));
        services.AddTransient<ISmsService, MasSmsAdapter>();
        services.AddTransient<IPublicHolidaysService, PublicHolidaysAdapter>();
        return services;
    }
}
