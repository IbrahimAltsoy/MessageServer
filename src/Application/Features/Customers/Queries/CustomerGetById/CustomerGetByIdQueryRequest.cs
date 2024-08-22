using MediatR;
namespace Application.Features.Customers.Queries.CustomerGetById
{
    public class CustomerGetByIdQueryRequest:IRequest<CustomerGetByIdQueryResponse>
    {
        public Guid Id { get; set; }
    }
}
