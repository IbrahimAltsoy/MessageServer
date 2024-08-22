using Application.Features.Customers.Queries.CustomerGetById;
using MediatR;

namespace Application.Features.Customers.Queries.DeleteCustomerById
{
    public class DeleteCustomerByIdQueryRequest:IRequest<CustomerGetByIdQueryResponse>
    {
        public Guid Id { get; set; }
    }
}
