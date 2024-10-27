using Application.Abstract.Common;
using Application.Features.OperationClaims.Command.Update;
using Application.Features.Panel.Command.UpdateUserStatus;
using Application.Features.Panel.Queries.CreatedCompanyLastMontly;
using Application.Features.Users.Commands.UpdateProfile;
using Application.Features.Users.Commands.UpdateUserRole;
using Application.Features.Users.Queries.GetById;
using Core.Application.Responses;
using Domain.Enums;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Newtonsoft.Json;
using Org.BouncyCastle.Asn1.Ocsp;
using System.Drawing.Printing;
using System.Text;

namespace SmartVisitServer.Web.Services.Users
{
    //http://localhost:5011/api/Users/UpdateUserRole
    public class UserService : IUserService
    {
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly IUser _currentUser;
        private readonly string _apiUrl = "http://localhost:5011/api/Users";

        public UserService(IHttpClientFactory httpClientFactory, IUser currentUser)
        {
            _httpClientFactory = httpClientFactory;
            _currentUser = currentUser;
        }

        public async Task<GetByIdUserResponse> GetProfileAsync()
        {
            var client = _httpClientFactory.CreateClient("SmartVisit");
            var apiUrl = $"{_apiUrl}/Profile?Id={_currentUser.Id}";
            var response = await client.GetAsync(apiUrl);
            if (!response.IsSuccessStatusCode)
            {
                var errorContent = await response.Content.ReadAsStringAsync();
                throw new Exception($"API'den veri alınırken hata oluştu: {response.StatusCode}, {errorContent}");
            }
            var responseData = await response.Content.ReadAsStringAsync();
            var pagedResponse = JsonConvert.DeserializeObject<GetByIdUserResponse>(responseData);
            return pagedResponse!;
        }

        public async Task<UpdateProfileCommandResponse> UpdateProfileAsync(UpdateProfileCommandRequest request)
        {
            var client = _httpClientFactory.CreateClient("SmartVisit");
            var apiUrl = $"{_apiUrl}/UpdateUserProfile";
            var jsonContent = JsonConvert.SerializeObject(request);
            var httpContent = new StringContent(jsonContent, Encoding.UTF8, "application/json");
            var response = await client.PostAsync(apiUrl, httpContent);

            if (!response.IsSuccessStatusCode)
            {
                var errorContent = await response.Content.ReadAsStringAsync();
                throw new Exception($"API'den veri güncellenirken hata oluştu: {response.StatusCode}, {errorContent}");
            }
            var responseData = await response.Content.ReadAsStringAsync();
            var updatedResponse = JsonConvert.DeserializeObject<UpdateProfileCommandResponse>(responseData);
            return updatedResponse!;
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
