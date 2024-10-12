using Application.Features.Customers.Queries.CustomerGetAllByUser;
using Application.Features.OperationClaims.Queries.GetAll;
using Application.Features.OperationClaims.Queries.GetAllUsersRole;
using Core.Application.Responses;
using MailKit.Search;
using Newtonsoft.Json;

namespace SmartVisitServer.Web.Services.OperationClaim
{
    public class OperationClaimService:IOperationClaimService
    {
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly string _apiUrl = "http://localhost:5011/api/OperationClaims";
        public OperationClaimService(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }

        public async Task<GetListResponse<OperationClaimGetAllQueryResponse>> GetAllOperationClaimsAsync(int page, int pageSize)
        {
            var client = _httpClientFactory.CreateClient("SmartVisit");
            var apiUrl = $"{_apiUrl}/GetAll?PageRequest.Page={page}&PageRequest.PageSize={pageSize}";
            var response = await client.GetAsync(apiUrl);
            if (!response.IsSuccessStatusCode)
            {
                var errorContent = await response.Content.ReadAsStringAsync();
                throw new Exception($"API'den veri alınırken hata oluştu: {response.StatusCode}, {errorContent}");
            }
            var responseData = await response.Content.ReadAsStringAsync();
            var pagedResponse = JsonConvert.DeserializeObject<GetListResponse<OperationClaimGetAllQueryResponse>>(responseData);
            return pagedResponse;
        }

        public async Task<GetListResponse<GetAllUsersRoleQueryResponse>> GetAllUsersRoleAsync(int page, int pageSize)
        {
            var client = _httpClientFactory.CreateClient("SmartVisit");
            var apiUrl = $"{_apiUrl}/GetAllUserRole?PageRequest.Page={page}&PageRequest.PageSize={pageSize}";
            var response = await client.GetAsync(apiUrl);
            if (!response.IsSuccessStatusCode)
            {
                var errorContent = await response.Content.ReadAsStringAsync();
                throw new Exception($"API'den veri alınırken hata oluştu: {response.StatusCode}, {errorContent}");
            }
            var responseData = await response.Content.ReadAsStringAsync();
            var pagedResponse = JsonConvert.DeserializeObject<GetListResponse<GetAllUsersRoleQueryResponse>>(responseData);
            return pagedResponse;
        }
    }
}
