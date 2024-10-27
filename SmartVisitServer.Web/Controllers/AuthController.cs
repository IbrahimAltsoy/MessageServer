using Core.Application.Dtos;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using SmartVisitServer.Web.Models;
using SmartVisitServer.Web.Models.Auth;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;

namespace SmartVisitServer.Web.Controllers
{
    public class AuthController : BaseController
    {
        private readonly IHttpClientFactory _httpClientFactory;
        public AuthController(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }

        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(LoginViewModel model)
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

                HttpContext.Response.Cookies.Append("JwtToken", loginResponse.AccessToken.Token);

                return RedirectToAction("Index", "Home");
            }

            ModelState.AddModelError("", "Kullanıcı adı veya şifre hatalı.");

            return View(model);
        }
        [HttpGet]
        public IActionResult Register()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Register(RegisterViewModel model)
        {
            if (!ModelState.IsValid)
            {
                // Eğer formdaki bilgiler geçersizse sayfayı tekrar göster
                return View(model);
            }

            // API'ye gönderilecek olan DTO modelini oluştur
            var userForRegisterDto = new
            {
                FirstName = model.FirstName,
                LastName = model.LastName,
                Email = model.Email,
                Password = model.Password
            };

            using (var client = new HttpClient())
            {
                // API'nin adresini burada tanımla
                client.BaseAddress = new Uri($"{apiUrl}/Auth/Register");
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                // API'ye POST isteği gönder
                var response = await client.PostAsJsonAsync("Register", userForRegisterDto);

                if (response.IsSuccessStatusCode)
                {
                    // API başarılıysa, kullanıcıyı bir sayfaya yönlendir
                    return RedirectToAction("ConfirmEmail"); // Kullanıcıyı başka bir sayfaya yönlendir
                }
                else
                {
                    // Hata durumunda formu tekrar göster ve hata mesajını ekle
                    ModelState.AddModelError(string.Empty, "Kayıt işlemi başarısız oldu.");
                    return View(model);
                }
            }
        }
        [HttpGet]
        public IActionResult ConfirmEmail()
        {
            return View();
        }

        [HttpGet]
        public async Task<IActionResult> ConfirmedEmail(ConfirmEmailViewModel model)
        {
            if (string.IsNullOrWhiteSpace(model.ActivationKey))
            {
                ModelState.AddModelError("ActivationKey", "Geçersiz doğrulama anahtarı.");
                return View(model);
            }

            using (var client = new HttpClient())
            {
                // API'nin adresini burada tanımla
                var uri = $"{apiUrl}/Auth/VerifyEmail?ActivationKey={model.ActivationKey}";
                var response = await client.GetAsync(uri);

                if (response.IsSuccessStatusCode)
                {
                    // API başarılıysa, kullanıcıyı başka bir sayfaya yönlendir
                    return RedirectToAction("EmailConfirmed");
                }
                else
                {
                    // Hata durumunda formu tekrar göster ve hata mesajını ekle
                    ModelState.AddModelError(string.Empty, "E-posta doğrulama işlemi başarısız oldu.");
                    return View(model);
                }
            }
        }




        public IActionResult EmailConfirmed()
        {
            return View();
        }
        
        [HttpPost]
        public IActionResult Logout()
        {            
            Response.Cookies.Delete("JwtToken");
            return RedirectToAction("Login", "Auth");
        }
    }
}
