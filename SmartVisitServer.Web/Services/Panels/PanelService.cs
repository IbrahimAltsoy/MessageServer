using Application.Features.Customers.Queries.CustomerGetAllByUser;
using Application.Features.Panel.Queries.UserMemberShipLastDay;
using Core.Application.Requests;
using Core.Application.Responses;
using Newtonsoft.Json;

namespace SmartVisitServer.Web.Services.Panels
{
    public class PanelService : IPanelService
    {
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly string _apiUrl = "http://localhost:5011/api/Panel";

        public PanelService(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
           
        }

        public async Task<GetListResponse<UserMemberShipLastDayQueryResponse>> UserMemberShipLastDayGetAllAsync(int page=0, int pageSize=2)
        {

            var client = _httpClientFactory.CreateClient("SmartVisit");
            var apiUrl = $"{_apiUrl}/UserMemberShipLastDays?PageRequest.Page={page}&PageRequest.PageSize={pageSize}";


            var response = await client.GetAsync(apiUrl);

            if (!response.IsSuccessStatusCode)
            {
                var errorContent = await response.Content.ReadAsStringAsync();
                throw new Exception($"API'den veri alınırken hata oluştu: {response.StatusCode}, {errorContent}");
            }

            var responseData = await response.Content.ReadAsStringAsync();
            var pagedResponse = JsonConvert.DeserializeObject<GetListResponse<UserMemberShipLastDayQueryResponse>>(responseData);
            return pagedResponse;
        }
    }
}
