using Application.Abstract.Common;
using Application.Features.Customers.Queries.CustomerGetAllByUser;
using Application.Services.Repositories;
using AutoMapper;
using Core.Application.Responses;
using Core.Persistence.Paging;
using Domain.Entities;
using MailKit.Search;
using MediatR;

namespace Application.Features.Customers.Queries.CustomerSearchByPhoneOrName
{
    public class CustomerSearchByPhoneOrNameQueryHandler : IRequestHandler<CustomerSearchByPhoneOrNameQueryRequest, GetListResponse<CustomerGetAllByUserQueryResponse>>
    {
        readonly IMapper _mapper;
        readonly ICustomerRepository _customerRepository;
        readonly IUser _currentUser;

        public CustomerSearchByPhoneOrNameQueryHandler(IMapper mapper, ICustomerRepository customerRepository, IUser currentUser)
        {
            _mapper = mapper;
            _customerRepository = customerRepository;
            _currentUser = currentUser;
        }

        public async Task<GetListResponse<CustomerGetAllByUserQueryResponse>> Handle(CustomerSearchByPhoneOrNameQueryRequest request, CancellationToken cancellationToken)
        {
            if (!Guid.TryParse(_currentUser.Id, out Guid userId))
            {
                throw new ArgumentException("Current user ID is not a valid GUID.");
            }
            var searchTerm = request.SearchTerm?.ToLower();            
            IPaginate<Customer>? datas = await _customerRepository.GetPaginateListAsync(
                x => x.UserId == userId &&
                    (string.IsNullOrEmpty(searchTerm) ||
                    x.NameSurname.ToLower().Contains(searchTerm) ||
                    x.Phone.ToLower().Contains(searchTerm))
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
