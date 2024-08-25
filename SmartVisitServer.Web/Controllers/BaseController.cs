using Microsoft.AspNetCore.Mvc;
using System.Net.Http.Headers;

namespace SmartVisitServer.Web.Controllers
{
    public class BaseController : Controller
    {
        private readonly IHttpClientFactory _httpClientFactory;

        protected HttpClient Client { get; private set; }
        public BaseController(IHttpClientFactory httpClientFactory, IHttpContextAccessor httpContextAccessor)
        {
            _httpClientFactory = httpClientFactory;
            var token = httpContextAccessor.HttpContext.Request.Cookies["JwtToken"];
            Client = _httpClientFactory.CreateClient("SmartVisit");
            if (!string.IsNullOrEmpty(token))
            {
                Client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
            }
        }
    }
}
