﻿using Application.Features.Customers.Queries.CustomerGetAllByUser;
using Core.Application.Responses;
using Domain.Enums;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Newtonsoft.Json;
using System.Drawing.Printing;

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

        public async Task<GetListResponse<CustomerGetAllByUserQueryResponse>> CustomerSearchByPhoneOrNameSurnameAsync(string searchTerm)
        {
        
            var client = _httpClientFactory.CreateClient("SmartVisit");
            var apiUrl = $"{_apiUrl}/CustomerSearchByPhoneOrNameSurname?SearchTerm={searchTerm}";
            var response = await client.GetAsync(apiUrl);
            if (!response.IsSuccessStatusCode)
            {
                var errorContent = await response.Content.ReadAsStringAsync();
                throw new Exception($"API'den veri alınırken hata oluştu: {response.StatusCode}, {errorContent}");
            }
            var responseData = await response.Content.ReadAsStringAsync();
            var pagedResponse = JsonConvert.DeserializeObject<GetListResponse<CustomerGetAllByUserQueryResponse>>(responseData);
            return pagedResponse!;
        }

        public async Task<GetListResponse<CustomerGetAllByUserQueryResponse>> GetCustomersAsync(int page, int pageSize, TimePeriodType? periodType)
        {
            var client = _httpClientFactory.CreateClient("SmartVisit");           
            var apiUrl = $"{_apiUrl}/Customers?TimePeriod={periodType}&PageRequest.Page={page}&PageRequest.PageSize={pageSize}";


            var response = await client.GetAsync(apiUrl);

            if (!response.IsSuccessStatusCode)
            {
                var errorContent = await response.Content.ReadAsStringAsync();
                throw new Exception($"API'den veri alınırken hata oluştu: {response.StatusCode}, {errorContent}");
            }

            var responseData = await response.Content.ReadAsStringAsync();
            var pagedResponse = JsonConvert.DeserializeObject<GetListResponse<CustomerGetAllByUserQueryResponse>>(responseData);
            return pagedResponse!;
        }


    }
}
