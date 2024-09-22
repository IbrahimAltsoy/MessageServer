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

    public class HomeController : Controller
    {
        private readonly IHttpClientFactory _httpClientFactory; 

        public HomeController(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            //var client = _httpClientFactory.CreateClient("SmartVisit");

            //var apiUrl = "http://localhost:5011/api/Customer/Customers?TimePeriod=3&PageRequest.Page=0&PageRequest.PageSize=10";
            //var response = await client.GetAsync(apiUrl);
            //response.EnsureSuccessStatusCode();
            //var responseData = await response.Content.ReadAsStringAsync();
            //var customers = JsonConvert.DeserializeObject<List<CustomerModel>>(responseData);
            return View();

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
