
using Application.Features.Panel.Queries.CreatedCompanyLastMontly;
using Application.Features.Reports.Queries.ReportCustomer;
using Application.Features.Reports.Queries.ReportsCustomerDetail;
using Application.Features.Reports.Queries.ReportSms;
using Core.Application.Responses;
using Domain.Enums;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Newtonsoft.Json;
using System.Drawing.Printing;

namespace SmartVisitServer.Web.Services.Reports
{
    public class ReportService : IReportService
    {
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly string _apiUrl = "http://localhost:5011/api/Reports";

        public ReportService(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }
        //http://localhost:5011/api/Reports/SmsReports?TimePeriod=3
        //http://localhost:5011/api/Reports/CustomerReports?TimePeriod=3
        public async Task<ReportCustomerQueryResponse> CustomerReportsAsync(TimePeriodType? periodType)
        {
            var client = _httpClientFactory.CreateClient("SmartVisit");
            var apiUrl = $"{_apiUrl}/CustomerReports?TimePeriod={periodType}";
            var response = await client.GetAsync(apiUrl);
            if (!response.IsSuccessStatusCode)
            {
                var errorContent = await response.Content.ReadAsStringAsync();
                throw new Exception($"API'den veri alınırken hata oluştu: {response.StatusCode}, {errorContent}");
            }
            var responseData = await response.Content.ReadAsStringAsync();
            var pagedResponse = JsonConvert.DeserializeObject<ReportCustomerQueryResponse>(responseData);
            return pagedResponse!;
        }
        //Raporlama kısmı oluşturulacaktır
        public async Task<ReportSmsQueryResponse> SmsReportsAsync(TimePeriodType? periodType)
        {
            var client = _httpClientFactory.CreateClient("SmartVisit");
            var apiUrl = $"{_apiUrl}/SmsReports?TimePeriod={periodType}";
            var response = await client.GetAsync(apiUrl);
            if (!response.IsSuccessStatusCode)
            {
                var errorContent = await response.Content.ReadAsStringAsync();
                throw new Exception($"API'den veri alınırken hata oluştu: {response.StatusCode}, {errorContent}");
            }
            var responseData = await response.Content.ReadAsStringAsync();
            var pagedResponse = JsonConvert.DeserializeObject<ReportSmsQueryResponse>(responseData);
            return pagedResponse!;
        }
    }
}
