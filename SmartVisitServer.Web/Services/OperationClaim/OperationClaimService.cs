﻿using Application.Features.AppSettings.Commands.Create;
using Application.Features.OperationClaims.Command.Create;
using Application.Features.OperationClaims.Command.Update;
using Application.Features.OperationClaims.Queries.GetAll;
using Application.Features.OperationClaims.Queries.GetAllUsersRole;
using Application.Features.OperationClaims.Queries.GetById;
using Core.Application.Responses;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Newtonsoft.Json;
using System.Drawing.Printing;
using System.Text;

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

        public async Task<IList<OperationClaimGetAllQueryResponse>> GetAllOperationClaimsAsync()
        {
            var client = _httpClientFactory.CreateClient("SmartVisit");
            var apiUrl = $"{_apiUrl}/GetAll";
            var response = await client.GetAsync(apiUrl);
            if (!response.IsSuccessStatusCode)
            {
                var errorContent = await response.Content.ReadAsStringAsync();
                throw new Exception($"API'den veri alınırken hata oluştu: {response.StatusCode}, {errorContent}");
            }
            var responseData = await response.Content.ReadAsStringAsync();
            var pagedResponse = JsonConvert.DeserializeObject<IList<OperationClaimGetAllQueryResponse>>(responseData);
            return pagedResponse!;
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
            return pagedResponse!;
        }

        public async Task<OperationClaimGetByIdQueryResponse> GetByIdUserRoleAsync(Guid id)
        {
            
            var client = _httpClientFactory.CreateClient("SmartVisit");
            var apiUrl = $"{_apiUrl}/GetById? Id ={id}";
            var response = await client.GetAsync(apiUrl);
            if (!response.IsSuccessStatusCode)
            {
                var errorContent = await response.Content.ReadAsStringAsync();
                throw new Exception($"API'den veri alınırken hata oluştu: {response.StatusCode}, {errorContent}");
            }
            var responseData = await response.Content.ReadAsStringAsync();
            var pagedResponse = JsonConvert.DeserializeObject<OperationClaimGetByIdQueryResponse>(responseData);
            return pagedResponse!;
        }

        public async Task<UpdateOperationClaimCommandResponse> UpdateRolAsync( UpdateOperationClaimCommandRequest request)
        {
            var client = _httpClientFactory.CreateClient("SmartVisit");
            var apiUrl = $"{_apiUrl}/Update";
            var jsonContent = JsonConvert.SerializeObject(request);
            var httpContent = new StringContent(jsonContent, Encoding.UTF8, "application/json");
            var response = await client.PutAsync(apiUrl, httpContent);

            if (!response.IsSuccessStatusCode)
            {
                var errorContent = await response.Content.ReadAsStringAsync();
                throw new Exception($"API'den veri güncellenirken hata oluştu: {response.StatusCode}, {errorContent}");
            }

            var responseData = await response.Content.ReadAsStringAsync();
            var updatedResponse = JsonConvert.DeserializeObject<UpdateOperationClaimCommandResponse>(responseData);

            return updatedResponse!;
        }
        public async Task<object> AddRoleAsync(string name)
        {
            var client = _httpClientFactory.CreateClient("SmartVisit");
            var apiUrl = $"{_apiUrl}/Create";
            var requestData = new { Name = name };
            var jsonContent = JsonConvert.SerializeObject(requestData);
            var httpContent = new StringContent(jsonContent, Encoding.UTF8, "application/json");
            var response = await client.PostAsync(apiUrl, httpContent);

            if (!response.IsSuccessStatusCode)
            {
                var errorContent = await response.Content.ReadAsStringAsync();
                throw new Exception($"API'den veri alınırken hata oluştu: {response.StatusCode}, {errorContent}");
            }
            var responseData = await response.Content.ReadAsStringAsync();
            var pagedResponse = JsonConvert.DeserializeObject<object>(responseData);
            return pagedResponse!;
        }


    }
}
