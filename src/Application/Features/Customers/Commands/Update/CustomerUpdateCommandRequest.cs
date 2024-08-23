using MediatR;

namespace Application.Features.Customers.Commands.Update
{
    public class CustomerUpdateCommandRequest:IRequest<CustomerUpdateCommandResponse>
    {
        public Guid Id { get; set; }
        public string NameSurname { get; set; }
        public string ProductName { get; set; }
        public string Phone { get; set; }
        public string Description { get; set; }
    }
}
