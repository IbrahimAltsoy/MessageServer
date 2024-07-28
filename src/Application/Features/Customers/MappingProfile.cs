using Application.Features.Customers.Commands.Create;
using Application.Features.Customers.Queries.CustomerGetAllByUser;
using AutoMapper;
using Core.Persistence.Paging;
using Domain.Entities;

namespace Application.Features.Customers
{
    public class MappingProfile:Profile
    {
        public MappingProfile()
        {
            CreateMap<Customer, CreateCustomerRequest>().ReverseMap();
            CreateMap<Customer, CustomerGetAllByUserQueryResponse>().ReverseMap();

        }
    }
}
