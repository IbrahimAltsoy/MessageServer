using Application.Features.Customers.Queries.CustomerGetAllByUser;
using Core.Application.Responses;
using MediatR;

namespace Application.Features.Customers.Queries.CustomerSearchByPhoneOrName
{
    public class CustomerSearchByPhoneOrNameQueryRequest:IRequest<GetListResponse<CustomerGetAllByUserQueryResponse>>
    {
        public string SearchTerm { get; set; }
    }
}
