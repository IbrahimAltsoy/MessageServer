using Application.Features.Smses.Commands.CreatedSmsDelivery;
using AutoMapper;
using Domain.Entities;

namespace Application.Features.Smses
{
    public class MappingProfile:Profile
    {
        public MappingProfile() 
        { 
            CreateMap<Sms,CreatedSmsDeliveryCommandRequest>().ReverseMap();
        }
    }
}
