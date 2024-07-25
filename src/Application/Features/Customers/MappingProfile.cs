using Application.Features.Customers.Commands.Create;
using AutoMapper;
using Domain.Entities;

namespace Application.Features.Customers
{
    public class MappingProfile:Profile
    {
        public MappingProfile()
        {
            CreateMap<Customer, CreateCustomerRequest>().ReverseMap();
        }
    }
}
