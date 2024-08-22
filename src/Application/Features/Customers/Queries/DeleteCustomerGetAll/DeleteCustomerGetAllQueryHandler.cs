
using Application.Abstract.Common;
using Application.Features.Customers.Queries.CustomerGetAllByUser;
using Application.Helpers;
using Application.Services.Repositories;
using AutoMapper;
using Core.Persistence.Paging;
using Domain.Entities;
using Domain.Enums;
using MediatR;

namespace Application.Features.Customers.Queries.DeleteCustomerGetAll
{
    public class DeleteCustomerGetAllQueryHandler : IRequestHandler<DeleteCustomerGetAllQueryRequest, IList<CustomerGetAllByUserQueryResponse>>
    {
        readonly IUser _currentUser;
        readonly ICustomerRepository _customerRepository;
        readonly IMapper _mapper;

        public DeleteCustomerGetAllQueryHandler(IUser currentUser, ICustomerRepository customerRepository, IMapper mapper)
        {
            _currentUser = currentUser;
            _customerRepository = customerRepository;
            _mapper = mapper;
        }

        public async Task<IList<CustomerGetAllByUserQueryResponse>> Handle(DeleteCustomerGetAllQueryRequest request, CancellationToken cancellationToken)
        {
            if (!Guid.TryParse(_currentUser.Id, out Guid userId))
            {
                throw new ArgumentException("Current user ID is not a valid GUID.");
            }
            var (startDate, endDate) = DateRangeHelper.GetDateRange(request.TimePeriod ?? TimePeriodType.Daily);
            IPaginate<Customer> datas = await _customerRepository.GetPaginateListAsync(
                predicate: s => s.CreatedDate >= startDate && s.CreatedDate <= endDate && s.UserId == userId && s.DeletedDate!=null,
                withDeleted:true,
                orderBy: c => c.OrderBy(c => c.CreatedDate),
                size: request.PageRequest.PageSize,
                index: request.PageRequest.Page,
                cancellationToken: cancellationToken
            );
            IList<CustomerGetAllByUserQueryResponse> data = _mapper.Map<List<CustomerGetAllByUserQueryResponse>>(datas.Items).ToList();
            return data;
            
        }
    }
}
