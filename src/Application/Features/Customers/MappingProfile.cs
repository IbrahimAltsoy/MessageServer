using Application.Features.Customers.Commands.Create;
using Application.Features.Customers.Commands.Delete;
using Application.Features.Customers.Commands.Update;
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
            CreateMap<Customer, CustomerGetByIdQueryResponse>().ForMember(dest => dest.PhotoUrls, opt => opt.MapFrom(src => src.CustomerPhotos!.Select(p => p.PhotoUrl).ToList())).ReverseMap();
            CreateMap<Customer, CustomerGetAllByUserQueryResponse>()
     .ForMember(dest => dest.PhotoUrls, opt => opt.MapFrom(src => src.CustomerPhotos!.Select(p => p.PhotoUrl).ToList())).ReverseMap();

            CreateMap<Customer, CreateCustomerRequest>().ReverseMap();
            CreateMap<Customer, CustomerUpdateCommandRequest>().ReverseMap();
            CreateMap<Customer, CustomerUpdateCommandResponse>().ReverseMap();
            CreateMap<Customer, CustomerDeleteCommandResponse>().ReverseMap();
           

        }
    }
}
