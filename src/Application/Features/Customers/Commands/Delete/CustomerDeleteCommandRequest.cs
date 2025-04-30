using MediatR;

namespace Application.Features.Customers.Commands.Delete
{
    public class CustomerDeleteCommandRequest:IRequest<CustomerDeleteCommandResponse>
    {
        public Guid Id { get; set; }        
    }
}
