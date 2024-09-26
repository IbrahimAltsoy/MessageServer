using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Text;

namespace SmartVisitServer.Web
{
    public class AuthMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly string _secretKey = "sebepsiz bos yere ayrilacaksan.sebepsiz bos yere ayrilacaksan..sebepsiz bos yere ayrilacaksan...";

        public AuthMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task InvokeAsync(HttpContext context)
        {         
            var token = context.Request.Cookies["JwtToken"];

            if (string.IsNullOrEmpty(token))
            {
                if (!context.Request.Path.StartsWithSegments("/Auth/Login") &&
                    !context.Request.Path.StartsWithSegments("/Auth/Register") &&
                    !context.Request.Path.StartsWithSegments("/Auth/ConfirmEmail"))
                {
                    context.Response.Redirect("/Auth/Login");
                    return;
                }
            }
            else
            {               
                var tokenHandler = new JwtSecurityTokenHandler();
                var validationParameters = new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_secretKey)),
                    ValidateIssuer = false,
                    ValidateAudience = false
                };

                try
                {                  
                    var principal = tokenHandler.ValidateToken(token, validationParameters, out _);
                    context.User = principal;
                }
                catch (SecurityTokenExpiredException)
                {                    
                    context.Response.Redirect("/Auth/Login");
                    return;
                }
                catch (Exception)
                {
                    context.Response.Redirect("/Auth/Login");
                    return;
                }
            }
           
            await _next(context);
        }
    }
}

