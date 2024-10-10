using Application.Abstract.Common;
using Application.Helpers;
using Application.Services.Repositories;
using AutoMapper;
using Core.Application.Responses;
using Core.Persistence.Paging;
using Domain.Entities;
using Domain.Enums;
using MailKit.Search;
using MediatR;
using System.Linq.Expressions;

namespace Application.Features.Customers.Queries.CustomerGetAllByUser
{
    public class CustomerGetAllByUserQueryHandler : IRequestHandler<CustomerGetAllByUserQueryRequest, GetListResponse<CustomerGetAllByUserQueryResponse>>
    {
        readonly IUser _currentUser;
        readonly ICustomerRepository _customerRepository;
        readonly IMapper _mapper;

        public CustomerGetAllByUserQueryHandler(IUser currentUser, ICustomerRepository customerRepository, IMapper mapper)
        {
            _currentUser = currentUser;
            _customerRepository = customerRepository;
            _mapper = mapper;
        }

        public async Task<GetListResponse<CustomerGetAllByUserQueryResponse>> Handle(CustomerGetAllByUserQueryRequest request, CancellationToken cancellationToken)
        {
            if (!Guid.TryParse(_currentUser.Id, out Guid userId))
            {
                throw new ArgumentException("Current user ID is not a valid GUID.");
            }
            var (startDate, endDate) = DateRangeHelper.GetDateRange(request.TimePeriod ?? TimePeriodType.Daily);
            IPaginate<Customer> datas = await _customerRepository.GetPaginateListAsync(
                predicate: s => s.CreatedDate >= startDate && s.CreatedDate <= endDate && s.UserId == userId,
                orderBy: c=>c.OrderBy(c=>c.CreatedDate),
                size: request.PageRequest.PageSize,
                index: request.PageRequest.Page,
                cancellationToken: cancellationToken
            );
        IList<CustomerGetAllByUserQueryResponse> data = _mapper.Map<List<CustomerGetAllByUserQueryResponse>>(datas.Items).ToList();

            GetListResponse<CustomerGetAllByUserQueryResponse> responses = new GetListResponse<CustomerGetAllByUserQueryResponse>()
            {
                Index = datas.Index,
                Size = datas.Size,
                Count = datas.Count,
                Pages = datas.Pages,
                HasPrevious = datas.HasPrevious,
                HasNext = datas.HasNext,
                Items = data,
            };
            return responses;
        }
    }
}
