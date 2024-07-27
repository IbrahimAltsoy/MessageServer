using AutoMapper;
using Domain.Entities;

namespace Domain.Dtos.Customers
{
    public class MappingProfile:Profile
    {
        public MappingProfile() 
        {
            CreateMap<Customer, CustomerGetPhoneDto>().ReverseMap();
        }
    }
}
