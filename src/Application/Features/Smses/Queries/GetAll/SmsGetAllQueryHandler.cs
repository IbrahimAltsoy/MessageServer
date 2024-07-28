using Application.Helpers;
using Application.Services.Repositories;
using Core.Persistence.Paging;
using Domain.Dtos.Smses;
using Domain.Entities;
using Domain.Enums;
using MediatR;

namespace Application.Features.Smses.Queries.GetAll
{
    public class SmsGetAllQueryHandler : IRequestHandler<SmsGetAllQueryRequest, IList<SmsGetDto>>
    {
        readonly ISmsRepository _smsRepository;
        readonly ICustomerRepository _customerRepository;

        public SmsGetAllQueryHandler(ISmsRepository smsRepository, ICustomerRepository customerRepository)
        {
            _smsRepository = smsRepository;
            _customerRepository = customerRepository;
        }

        public async Task<IList<SmsGetDto>> Handle(SmsGetAllQueryRequest request, CancellationToken cancellationToken)
        {
            var (startDate, endDate) = DateRangeHelper.GetDateRange(request.TimePeriod ?? TimePeriodType.Daily);
            IPaginate<Sms> datas = await _smsRepository.GetPaginateListAsync(
                predicate: s =>  s.CreatedDate >= startDate && s.CreatedDate <= endDate,

                size: request.PageRequest.PageSize,
                index: request.PageRequest.Page,
                cancellationToken: cancellationToken
            );
            var smsList = datas.Items;
            var customerIds = smsList
       .Select(sms => sms.CustomerId)
       .Where(customerId => customerId.HasValue)
       .Distinct()
       .ToList();

            var customers = await _customerRepository.GetListAsync();
            var customerDict = customers.ToDictionary(c => c.Id, c => c);

            var response = smsList.Select(sms =>
            {
                var customer = customerDict.TryGetValue(sms.Customer.Id, out var cust) ? cust : null;

                return new SmsGetDto
                {
                    NameSurname = sms.Customer.NameSurname,
                    ProductName = sms.Customer.ProductName,
                    Description = sms.Customer.Description,
                    Phone = sms.Customer.Phone,
                    Content = sms.Content,
                };
            }).ToList();
            return response;
        }
    }
}
