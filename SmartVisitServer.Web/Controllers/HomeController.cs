using Application.Abstract.Common;
using Application.Services.Repositories;
using Domain.Entities;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using SmartVisitServer.Web.Models;
using System.Diagnostics;

namespace SmartVisitServer.Web.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly HttpClient _httpClient;
        
        private readonly IUser _user;

        public HomeController(ILogger<HomeController> logger, HttpClient httpClient, IUser user)
        {
            _logger = logger;
            _httpClient = httpClient;
            _user = user;
        }

        public async Task<IActionResult> Index()
        {
            //giri� yapan kullan�c�y� yakalam�yor o y�zden hata veriyor nu hatay� userId ye g�re yapmasan response ba�ar�l�
            var apiUrl = "http://localhost:5011/api/Customer/getCustomers?TimePeriod=3&PageRequest.Page=0&PageRequest.PageSize=10";
            
            var us = _user.Id;

            try
            {
                var response = await _httpClient.GetAsync(apiUrl);
                response.EnsureSuccessStatusCode(); // Hata durumunda exception f�rlat�r
                var responseData = await response.Content.ReadAsStringAsync();
                var customers = JsonConvert.DeserializeObject<List<CustomerModel>>(responseData);

                return View(customers);
            }
            catch (HttpRequestException ex)
            {
                _logger.LogError($"API �a�r�s� s�ras�nda bir hata olu�tu: {ex.Message}");
                // Hata ile ilgili kullan�c�ya bilgi verme veya alternatif bir i�lem yapma
                return View("Error"); // veya uygun bir hata sayfas�na y�nlendirme
            }
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
