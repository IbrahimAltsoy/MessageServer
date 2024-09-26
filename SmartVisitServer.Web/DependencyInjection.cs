using Application.Abstract.Common;
using Core.Security.Encryption;
using Core.Security.JWT;
using Core.WebAPI.Extensions.Swagger;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using Microsoft.Net.Http.Headers;
using Microsoft.OpenApi.Models;
using Persistence.Authentication;
using System.Net.Http.Headers;
using System.Reflection;

namespace SmartVisitServer.Web
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddWebMvcServices(this IServiceCollection services, IConfiguration configuration)
        {
           

            const string tokenOptionsConfigurationSection = "TokenOptions";
            TokenOptions tokenOptions =
                configuration.GetSection(tokenOptionsConfigurationSection).Get<TokenOptions>()
                ?? throw new InvalidOperationException($"\"{tokenOptionsConfigurationSection}\" section cannot found in configuration.");

            services
                .AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                .AddJwtBearer(options =>
                {
                    options.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateIssuer = true,
                        ValidateAudience = true,
                        ValidateLifetime = true,
                        ValidIssuer = tokenOptions.Issuer,
                        ValidAudience = tokenOptions.Audience,
                        ValidateIssuerSigningKey = true,
                        IssuerSigningKey = SecurityKeyHelper.CreateSecurityKey(tokenOptions.SecurityKey),
                        NameClaimType = "http://schemas.xmlsoap.org/ws/2005/05/identity/claims/name",
                        RoleClaimType = "http://schemas.microsoft.com/ws/2008/06/identity/claims/role"
                    };
                });
  
            services.AddScoped<IUser, CurrentUser>();
            services.AddHttpContextAccessor();
            services.AddAuthorization();
            services.AddControllersWithViews();

            services.AddSwaggerGen(opt =>
            {
                opt.AddSecurityDefinition(
                    name: "Bearer",
                    securityScheme: new OpenApiSecurityScheme
                    {
                        Name = "Authorization",
                        Type = SecuritySchemeType.ApiKey,
                        Scheme = "Bearer",
                        BearerFormat = "JWT",
                        In = ParameterLocation.Header,
                        Description =
                            "JWT Authorization header using the Bearer scheme. \r\n\r\n Enter 'Bearer' [space] and then your token in the text input below.\r\n\r\n`Enter your token in the text input below.`"
                    }
                );
                opt.OperationFilter<BearerSecurityRequirementOperationFilter>();
                var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
                var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
                opt.IncludeXmlComments(xmlPath);
            });
            
            services.AddHttpClient();
            services.AddHttpClient("SmartVisit", httpClient =>
            {
                httpClient.BaseAddress = new Uri(configuration["APIs:SmartVisit"]);
                httpClient.DefaultRequestHeaders.Add(HeaderNames.Accept, "application/json");
            })
            .ConfigureHttpClient((serviceProvider, client) =>
            {
                var contextAccessor = serviceProvider.GetRequiredService<IHttpContextAccessor>();
                var token = contextAccessor.HttpContext?.Request.Cookies["JwtToken"];

                if (!string.IsNullOrEmpty(token))
                {
                    client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
                }
            });

            return services;
        }
    }
}
