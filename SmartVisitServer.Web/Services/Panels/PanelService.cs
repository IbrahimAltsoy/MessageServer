using Application.Features.Panel.Command.UpdateUserStatus;
using Application.Features.Panel.Queries.CreatedCompanyLastMontly;
using Application.Features.Panel.Queries.UserMemberShipLastDay;
using Core.Application.Responses;
using Domain.Enums;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Newtonsoft.Json;
using System.Drawing.Printing;
using System.Text;

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
        public async Task<GetListResponse<CreatedCompanyLastMontlyQueryResponse>> CreatedCompanyLastMontlyAsync(int page, int pageSize)
        {
            var client = _httpClientFactory.CreateClient("SmartVisit");
            var apiUrl = $"{_apiUrl}/CreatedCompanyLastMontly?PageRequest.Page={page}&PageRequest.PageSize={pageSize}";
            var response = await client.GetAsync(apiUrl);
            if (!response.IsSuccessStatusCode)
            {
                var errorContent = await response.Content.ReadAsStringAsync();
                throw new Exception($"API'den veri alınırken hata oluştu: {response.StatusCode}, {errorContent}");
            }
            var responseData = await response.Content.ReadAsStringAsync();
            var pagedResponse = JsonConvert.DeserializeObject<GetListResponse<CreatedCompanyLastMontlyQueryResponse>>(responseData);
            return pagedResponse!;
        }

        public async Task<UpdateUserStateResponse> UpdateUserStateAsync(Guid id, UserStatus userStatus)
        {
            var client = _httpClientFactory.CreateClient("SmartVisit");
            var apiUrl = $"{_apiUrl}/UpdateUserStatu";
            var requestData = new
            {
                Id = id,
                UserStatus = userStatus
            };
            var jsonData = JsonConvert.SerializeObject(requestData);
            var content = new StringContent(jsonData, Encoding.UTF8, "application/json");
            var response = await client.PostAsync(apiUrl, content);
            if (!response.IsSuccessStatusCode)
            {
                var errorContent = await response.Content.ReadAsStringAsync();
                Console.WriteLine($"API hatası: {errorContent}");
                throw new Exception($"API'den veri alınırken hata oluştu: {response.StatusCode}, {errorContent}");
            }
            var responseData = await response.Content.ReadAsStringAsync();
            var result = JsonConvert.DeserializeObject<UpdateUserStateResponse>(responseData);
            return result!;
        }
        public async Task<GetListResponse<UserMemberShipLastDayQueryResponse>> UserMemberShipLastDayGetAllAsync(int page=0, int pageSize=5)
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
            return pagedResponse!;
        }
    }
}
