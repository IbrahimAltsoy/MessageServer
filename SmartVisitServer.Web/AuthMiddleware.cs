using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Text;

namespace SmartVisitServer.Web
{
    public class AuthMiddleware
    {
        private readonly RequestDelegate _next;

        public AuthMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            // JWT token’ı kontrol et
            var token = context.Request.Cookies["JwtToken"];

            if (string.IsNullOrEmpty(token))
            {
                // Eğer token yoksa, kullanıcıyı giriş sayfasına yönlendir
                if (!context.Request.Path.StartsWithSegments("/Auth/Login") && !context.Request.Path.StartsWithSegments("/Auth/Register") && !context.Request.Path.StartsWithSegments("/Auth/ConfirmEmail"))
                {
                    context.Response.Redirect("/Auth/Login");
                    return;
                }
            }

            // Eğer token varsa, bir sonraki middleware’e geç
            await _next(context);

        }
    }
}
