using Application.Abstract.Common;
using Application.Helpers;
using Application.Services.Repositories;
using AutoMapper;
using Domain.Enums;
using MediatR;

namespace Application.Features.Customers.Queries.Summary
{
    public class CustomerSummaryQueryHandler : IRequestHandler<CustomerSummaryQueryRequest, CustomerSummaryQueryResponse>
    {
        readonly IUser _currentUser;
        readonly ICustomerRepository _customerRepository;
        readonly IMapper _mapper;

        public CustomerSummaryQueryHandler(IUser currentUser, ICustomerRepository customerRepository, IMapper mapper)
        {
            _currentUser = currentUser;
            _customerRepository = customerRepository;
            _mapper = mapper;
        }

        public async Task<CustomerSummaryQueryResponse> Handle(CustomerSummaryQueryRequest request, CancellationToken cancellationToken)
        {
            if (!Guid.TryParse(_currentUser.Id, out Guid userId))
            {
                throw new ArgumentException("Current user ID is not a valid GUID.");
            }
            var (startDate, endDate) = DateRangeHelper.GetDateRange(request.TimePeriod ?? TimePeriodType.Daily);
            var datas = await _customerRepository.GetListAsync(predicate: s => s.CreatedDate >= startDate && s.CreatedDate <= endDate && s.UserId == userId);
            if (datas != null)
            {
                return new CustomerSummaryQueryResponse()
                {
                    Delivered = datas.Count(s => s.Status == CustomerStatus.Delivered),
                    Waiting = datas.Count(s => s.Status == CustomerStatus.Waiting),
                    Canceled = datas.Count(s => s.Status == CustomerStatus.Canceled)
                };
            }
            else return new();
        }
    }
}
