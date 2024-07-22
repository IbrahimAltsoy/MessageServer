using Application.Abstract.Common;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;

namespace Persistence.Authentication
{
    public class CurrentUser : IUser
    {
        private readonly IHttpContextAccessor _httpContextAccessor;

        public CurrentUser(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor ?? throw new ArgumentNullException(nameof(httpContextAccessor));
        }

        public string? Name => _httpContextAccessor.HttpContext?.User?.FindFirst("fname")?.Value ?? "Unknown";
        public string? Id => _httpContextAccessor.HttpContext?.User?.FindFirst("uid")?.Value ?? "Unknown";
        public string? LastName => _httpContextAccessor.HttpContext?.User?.FindFirst("sname")?.Value ?? "Unknown";
        public string? Email => _httpContextAccessor.HttpContext?.User?.FindFirst("http://schemas.xmlsoap.org/ws/2005/05/identity/claims/emailaddress")?.Value ?? "Unknown";

        public IEnumerable<Claim>? Claims
        {
            get
            {
                var claims = _httpContextAccessor.HttpContext?.User?.Claims;
                return claims?.Any() == true ? claims : new List<Claim> { new Claim("Unknown", "Unknown") };
            }
        }
    }
}
