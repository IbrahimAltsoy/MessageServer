using Application.Abstract.Common;
using Application.Helpers;
using Application.Services.Repositories;
using Core.Persistence.Paging;
using Domain.Dtos.Smses;
using Domain.Entities;
using Domain.Enums;
using MediatR;

namespace Application.Features.Smses.Queries.GetByCustomer
{
    public class GetSmsByCustomerQueryHandler : IRequestHandler<GetSmsByCustomerQueryRequest, IList<SmsGetDto>>
    {
        readonly ISmsRepository _smsRepository;
        readonly IUser _currentUser;
        readonly ICustomerRepository _customerRepository;
        

        public GetSmsByCustomerQueryHandler(ISmsRepository smsRepository, IUser cuurentUser, ICustomerRepository customerRepository)
        {
            _smsRepository = smsRepository;
            _currentUser = cuurentUser;
            _customerRepository = customerRepository;           
        }

        public async Task<IList<SmsGetDto>> Handle(GetSmsByCustomerQueryRequest request, CancellationToken cancellationToken)
        {
            if (!Guid.TryParse(_currentUser.Id, out Guid userId))
            {
                throw new ArgumentException("Current user ID is not a valid GUID.");
            }
            var (startDate, endDate) = DateRangeHelper.GetDateRange(request.TimePeriod ?? TimePeriodType.Daily);
            IPaginate<Sms> datas = await _smsRepository.GetPaginateListAsync(
                predicate: s => s.CustomerId ==request.CustomerId && s.CreatedDate >= startDate && s.CreatedDate <= endDate,

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
