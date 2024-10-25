using Application.Features.OperationClaims.Command.Update;
using Application.Features.Panel.Command.UpdateUserStatus;
using Application.Features.Users.Commands.UpdateUserRole;
using Domain.Enums;
using Newtonsoft.Json;
using System.Text;

namespace SmartVisitServer.Web.Services.Users
{
    //http://localhost:5011/api/Users/UpdateUserRole
    public class UserService : IUserService
    {
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly string _apiUrl = "http://localhost:5011/api/Users";

        public UserService(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }

        public async Task<UpdateUserRoleCommandResponse> UpdateUserRoleAsync(UpdateUserRoleCommandRequest request)
        {
            var client = _httpClientFactory.CreateClient("SmartVisit");
            var apiUrl = $"{_apiUrl}/UpdateUserRole";
            var jsonContent = JsonConvert.SerializeObject(request);
            var httpContent = new StringContent(jsonContent, Encoding.UTF8, "application/json");
            var response = await client.PostAsync(apiUrl, httpContent);

            if (!response.IsSuccessStatusCode)
            {
                var errorContent = await response.Content.ReadAsStringAsync();
                throw new Exception($"API'den veri güncellenirken hata oluştu: {response.StatusCode}, {errorContent}");
            }
            var responseData = await response.Content.ReadAsStringAsync();
            var updatedResponse = JsonConvert.DeserializeObject<UpdateUserRoleCommandResponse>(responseData);
            return updatedResponse!;
        }
    }
}
