using Application.Abstract.Common;
using Application.Services.Repositories;
using Domain.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json;
using SmartVisitServer.Web.Models;
using System.Diagnostics;
using System.IdentityModel.Tokens.Jwt;
using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;

namespace SmartVisitServer.Web.Controllers
{

    public class HomeController : BaseController
    {
        private readonly IHttpClientFactory _httpClientFactory; // IHttpClientFactory alanýný tanýmlayýn
       

        public HomeController(IHttpClientFactory httpClientFactory, IHttpContextAccessor httpContextAccessor)
            : base(httpClientFactory, httpContextAccessor)
        {
            _httpClientFactory = httpClientFactory; // IHttpClientFactory alanýný baþlatýn
           
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

                // AccessToken'ý cookie'ye ekleyin
                HttpContext.Response.Cookies.Append("JwtToken", loginResponse.AccessToken.Token);

                return RedirectToAction("Index", "Home");
            }

            ModelState.AddModelError("", "Kullanýcý adý veya þifre hatalý.");
            return View(model);
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {

            var apiUrl = "http://localhost:5011/api/Customer/Customers?TimePeriod=3&PageRequest.Page=0&PageRequest.PageSize=10";
            var client = _httpClientFactory.CreateClient("SmartVisit");
            var token = HttpContext.Request.Cookies["JwtToken"];
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

            var response = await client.GetAsync(apiUrl);
            var responseData = await response.Content.ReadAsStringAsync();
            var customers = JsonConvert.DeserializeObject<List<CustomerModel>>(responseData);

            return View(customers);
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }

}
