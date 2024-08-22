using Application.Features.Customers.Commands.Create;
using Application.Features.Customers.Commands.Delete;
using Application.Features.Customers.Queries.CustomerGetAllByUser;
using Application.Features.Customers.Queries.CustomerGetById;
using AutoMapper;
using Core.Persistence.Paging;
using Domain.Entities;

namespace Application.Features.Customers
{
    public class MappingProfile:Profile
    {
        public MappingProfile()
        {
            CreateMap<Customer, CustomerGetByIdQueryResponse>().ReverseMap();
            CreateMap<Customer, CustomerGetAllByUserQueryResponse>().ReverseMap();
            CreateMap<Customer, CreateCustomerRequest>().ReverseMap();
            CreateMap<Customer, CustomerDeleteCommandResponse>().ReverseMap();
           

        }
    }
}
