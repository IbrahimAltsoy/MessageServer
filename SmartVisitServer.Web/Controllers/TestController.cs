using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using SmartVisitServer.Web.Models;
using System.Net.Http;
using System.Text;

namespace SmartVisitServer.Web.Controllers
{
    public class TestController : Controller
    {
        private readonly IHttpClientFactory _httpClientFactory;

        public TestController(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }

        [HttpGet]
        public IActionResult Giris()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Giris(LoginViewModel model)
        {

            if (!ModelState.IsValid)
                return View(model);

            var client = _httpClientFactory.CreateClient("SmartVisit");
            var loginRequest = new { Email = model.Email, Password = model.Password };
            var content = new StringContent(System.Text.Json.JsonSerializer.Serialize(loginRequest), Encoding.UTF8, "application/json");

            var response = await client.PostAsync("http://localhost:5011/api/Auth/Login", content);

            if (response.IsSuccessStatusCode)
            {
                var responseBody = await response.Content.ReadAsStringAsync();

                var loginResponse = JsonConvert.DeserializeObject<LoginResponseModel>(responseBody);

                // AccessToken'ı cookie'ye ekleyin
                HttpContext.Response.Cookies.Append("JwtToken", loginResponse.AccessToken.Token);

                return RedirectToAction("Index", "Home");
            }

            ModelState.AddModelError("", "Kullanıcı adı veya şifre hatalı.");
            return View(model);
        }
        
    }
}
