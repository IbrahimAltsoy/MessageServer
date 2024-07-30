using Application.Abstract.Common;
using Application.Features.Customers.Queries.CustomerGetAllByUser;
using Application.Helpers;
using Application.Services.Repositories;
using AutoMapper;
using Core.Persistence.Paging;
using Domain.Entities;
using Domain.Enums;
using MediatR;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace Application.Features.Reports.Queries.ReportsCustomerDetail
{
    public class ReportCustomerDetailQueryHandler : IRequestHandler<ReportCustomerDetailQueryRequest, ReportCustomerDetailQueryResponse>
    {
        readonly IUser _currentUser;
        readonly ICustomerRepository _customerRepository;
        readonly ISmsRepository _smsRepository;
        readonly IReportRepository _reportRepository;
        readonly IMapper _mapper;

        public ReportCustomerDetailQueryHandler(IUser currentUser, ICustomerRepository customerRepository, ISmsRepository smsRepository, IReportRepository reportRepository, IMapper mapper)
        {
            _currentUser = currentUser;
            _customerRepository = customerRepository;
            _smsRepository = smsRepository;
            _reportRepository = reportRepository;
            _mapper = mapper;
        }

        public async Task<ReportCustomerDetailQueryResponse> Handle(ReportCustomerDetailQueryRequest request, CancellationToken cancellationToken)
        {
            if (!Guid.TryParse(_currentUser.Id, out Guid userId))
            {
                throw new ArgumentException("Current user ID is not a valid GUID.");
            }
            var (startDate, endDate) = DateRangeHelper.GetDateRange(request.TimePeriod ?? TimePeriodType.Daily);
            IPaginate<Customer> customers = await _customerRepository.GetPaginateListAsync(
                predicate: s => s.CreatedDate >= startDate && s.CreatedDate <= endDate && s.UserId == userId,
                orderBy: c => c.OrderBy(c => c.CreatedDate),
                size: request.PageRequest.PageSize,
                index: request.PageRequest.Page,
                cancellationToken: cancellationToken
            );
            IPaginate<Sms> smses = await _smsRepository.GetPaginateListAsync(
                predicate: s => s.CreatedDate >= startDate && s.CreatedDate <= endDate && s.UserId == userId,
                orderBy: c => c.OrderBy(c => c.CreatedDate),
                size: request.PageRequest.PageSize,
                index: request.PageRequest.Page,
                cancellationToken: cancellationToken
            );
            string formattedStartDate = startDate.ToString("dd.MM.yyyy");
            string formattedEndDate = DateTime.UtcNow.ToString("dd.MM.yyyy");

            var responseMessage = $"{formattedStartDate} - {formattedEndDate} tarihleri arasında {customers.Count} müşteri kaydedildi ve {smses.Count} sms gönderildi.";



            IList<ReportCustomerDetailQueryResponse.CustomerDetail> customerData = customers.Items.Select(customer => new ReportCustomerDetailQueryResponse.CustomerDetail
            {
             
                NameSurname = customer.NameSurname,
                Phone = customer.Phone,
            }).ToList();

            if (request.SaveReport)
            {
                var options = new JsonSerializerOptions
                {
                    Encoder = System.Text.Encodings.Web.JavaScriptEncoder.Create(System.Text.Unicode.UnicodeRanges.All),
                    WriteIndented = false 
                };
                var customerListJson = JsonSerializer.Serialize(customerData, options);
                var report = new Report
                {
                    Title = "Müşreti Detay Raporu",
                    UserId = userId,
                    SmsCount = smses.Count,
                    CustomerCount = customers.Count,
                   Content = responseMessage,
                   CustomerListJson = customerListJson

                };
                await _reportRepository.AddAsync(report);
                responseMessage += " Rapor başarıyla kaydedildi.";

            }
            return new ReportCustomerDetailQueryResponse
            {
                Message = responseMessage,
                Customers = customerData
            };

        }
    }
}
