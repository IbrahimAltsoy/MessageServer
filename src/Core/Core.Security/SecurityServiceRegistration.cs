using Core.Security.EmailAuthenticator;
using Core.Security.JWT;
using Microsoft.Extensions.DependencyInjection;
using Core.Security.SmsAuthenticator;
using Core.Security.OtpAuthenticator.OtpNet;
using Core.Security.OtpAuthenticator;

namespace Core.Security;

public static class SecurityServiceRegistration
{
    public static IServiceCollection AddSecurityServices(this IServiceCollection services)
    {
        services.AddScoped<ITokenHelper, JwtHelper>();
        services.AddScoped<IEmailAuthenticatorHelper, EmailAuthenticatorHelper>();
        services.AddScoped<ISmsAuthenticatorHelper, SmsAuthenticatorHelper>();
        services.AddScoped<IOtpAuthenticatorHelper, OtpNetOtpAuthenticatorHelper>();
        return services;
    }
}