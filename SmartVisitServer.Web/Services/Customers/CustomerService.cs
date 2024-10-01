using Application.Features.Customers.Queries.CustomerGetAllByUser;
using Domain.Enums;
using Newtonsoft.Json;
using SmartVisitServer.Web.Models.Paginate;

namespace SmartVisitServer.Web.Services.Customers
{
    public class CustomerService:ICustomerService
    {
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly string _apiUrl = "http://localhost:5011/api/Customers";

        public CustomerService(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }
        public async Task<List<CustomerGetAllByUserQueryResponse>> GetCustomersAsync(int page, int pageSize, TimePeriodType? periodType)
        {
            var client = _httpClientFactory.CreateClient("SmartVisit");
            var apiUrl = $"{_apiUrl}/Customers?TimePeriod={periodType}&PageRequest.Page={page - 1}&PageRequest.PageSize={pageSize}";

            var response = await client.GetAsync(apiUrl);

            if (!response.IsSuccessStatusCode)
            {
                var errorContent = await response.Content.ReadAsStringAsync();
                throw new Exception($"API'den veri alınırken hata oluştu: {response.StatusCode}, {errorContent}");
            }

            var responseData = await response.Content.ReadAsStringAsync();
            var pagedResponse = JsonConvert.DeserializeObject<List<CustomerGetAllByUserQueryResponse>>(responseData);

            // Toplam müşteri sayısını API'den alın
            var totalCount = pagedResponse.Count; // API'den gelen toplam müşteri sayısı

            

            return pagedResponse;
        }


    }
}
