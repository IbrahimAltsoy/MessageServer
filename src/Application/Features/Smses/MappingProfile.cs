using Application.Features.Smses.Commands.CreatedSmsDelivery;
using Application.Features.Smses.Queries.GetByUser;
using AutoMapper;
using Domain.Entities;

namespace Application.Features.Smses
{
    public class MappingProfile:Profile
    {
        public MappingProfile() 
        { 
            CreateMap<Sms,CreatedSmsDeliveryCommandRequest>().ReverseMap();
            //CreateMap<Sms, GetSmsByUserQueryResponse>().ReverseMap();
        }
    }
}
