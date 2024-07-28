using Application.Abstract.Common;
using Application.Features.Users.Queries.GetById;
using Application.Helpers;
using Application.Services.Repositories;
using AutoMapper;
using Core.Application.Responses;
using Core.Persistence.Paging;
using Domain.Dtos.Smses;
using Domain.Entities;
using Domain.Enums;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.Features.Smses.Queries.GetByUser
{
    public class GetSmsByUserQueryHandler : IRequestHandler<GetSmsByUserQueryRequest, IList<SmsGetDto>>
    {
        readonly ISmsRepository _smsRepository;
        readonly ICustomerRepository _customerRepository;
        readonly IUser _currentUser;
        readonly IMapper _mapper;

        public GetSmsByUserQueryHandler(
            ISmsRepository smsRepository,
            ICustomerRepository customerRepository,
            IUser cuurentUser,
            IMapper mapper)
        {
            _smsRepository = smsRepository;
            _customerRepository = customerRepository;
            _currentUser = cuurentUser;
            _mapper = mapper;
        }

        public async Task<IList<SmsGetDto>> Handle(GetSmsByUserQueryRequest request, CancellationToken cancellationToken)
        {
            if (!Guid.TryParse(_currentUser.Id, out Guid userId))
            {
                throw new ArgumentException("Current user ID is not a valid GUID.");
            }
            var (startDate, endDate) = DateRangeHelper.GetDateRange(request.TimePeriod ?? TimePeriodType.Daily);

            IPaginate<Sms> datas = await _smsRepository.GetPaginateListAsync(
                predicate: s => s.UserId == userId && s.CreatedDate >= startDate && s.CreatedDate <= endDate,

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
                    //CustomerId = sms.CustomerId
                };
            }).ToList();
            return response;
        }
    }
}
