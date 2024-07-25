using Application.Services.Repositories;
using MediatR;

namespace Application.Features.Customers.Commands.Create
{
    public class CreateCustomerRequest:IRequest<CreateCustomerResponse>
    {
        //public Guid UserId { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public string Phone { get; set; }
        public string Description { get; set; }
    }
}
