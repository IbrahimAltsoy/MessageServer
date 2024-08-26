using Application.Features.Customers.Queries.CustomerGetAllByUser;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Newtonsoft.Json;
using SmartVisitServer.Web.Models.Paginate;
using System.Drawing.Printing;

namespace SmartVisitServer.Web.Controllers
{
    public class CustomerController : Controller
    {
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly string _apiUrl = "http://localhost:5011/api/Customer";

        public CustomerController(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }

        [HttpGet]
        public async Task<IActionResult> Customers(int page = 1, int pageSize = 2)
        {
            var client = _httpClientFactory.CreateClient("SmartVisit");
            var apiUrl = $"{_apiUrl}/Customers?TimePeriod=3&PageRequest.Page={page - 1}&PageRequest.PageSize={pageSize}";

            var response = await client.GetAsync(apiUrl);

            if (!response.IsSuccessStatusCode)
            {
                var errorContent = await response.Content.ReadAsStringAsync();
                return StatusCode((int)response.StatusCode, $"API'den veri alınırken hata oluştu: {response.StatusCode}, {errorContent}");
            }

            var responseData = await response.Content.ReadAsStringAsync();
            var customers = JsonConvert.DeserializeObject<List<CustomerGetAllByUserQueryResponse>>(responseData);

           
            var totalCount =customers.Count;
            var pagedList = new PagedList<CustomerGetAllByUserQueryResponse>(customers, totalCount, page, pageSize);

            return View(pagedList);
        }
    }
}
