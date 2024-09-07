using Application.Features.Customers.Queries.CustomerGetAllByUser;
using Application.Services.Repositories;
using Domain.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Newtonsoft.Json;
using Persistence.Configurations;
using SmartVisitServer.Web.Models;
using SmartVisitServer.Web.Models.Paginate;
using System.Drawing.Printing;

namespace SmartVisitServer.Web.Controllers
{
   
    public class CustomerController : Controller
    {
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly string _apiUrl = "http://localhost:5011/api/Customer";
        private readonly IUserRepository _userRepository; 
        private readonly ICustomerRepository _customerRepository;

        public CustomerController(IHttpClientFactory httpClientFactory, IUserRepository userRepository, ICustomerRepository customerRepository)
        {
            _httpClientFactory = httpClientFactory;
            _userRepository = userRepository;
            _customerRepository = customerRepository;
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
        [HttpGet]
        public async Task<IActionResult> ListUsersWithQrCodes()
        {

            var users = await _userRepository.GetListAsync();
            List<UserQrCode> userQrCodeList = users.Select(user => new UserQrCode
            {
                FirstName = user.FirstName,
                LastName = user.LastName,
                Email = user.Email,
                QRCode = user.QRCode
            }).ToList();

            return View(userQrCodeList);
        }
        [HttpGet]
        public async Task<IActionResult> Form(Guid id)
        {
            // Id'ye göre veriyi alın (örneğin, kullanıcı bilgilerini)
            Domain.Entities.User? user =await _userRepository.GetAsync(x=>x.Id==id);

            if (user == null)
            {
                return NotFound();
            }
            CustomerModel customer = new CustomerModel()
            {
                Id = user.Id,
            };
            return View(customer); // Model ile formu döndürün
        }
        [HttpPost("form/submit")]
        public async Task<IActionResult> SubmitForm([FromForm] CustomerModel model)
        {
            // Id'yi ve diğer form verilerini işleyin
            var user = await _userRepository.GetAsync(x=>x.Id==model.Id);

            if (user == null)
            {
                return NotFound();
            }

            Customer customer = new Customer()
            {
                NameSurname = model.NameSurname,
                Phone = model.Phone,
                ProductName = model.ProductName,
                Description = model.Description,
                UserId = user.Id,
            };
            await _customerRepository.AddAsync(customer);
           


            return RedirectToAction("Home"); // Başarı sayfasına yönlendir
        }

    }
}
