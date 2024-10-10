using Application.Features.Panel.Command.UpdateUserStatus;
using Application.Features.Panel.Queries.CreatedCompanyLastMontly;
using Application.Features.Panel.Queries.UserMemberShipLastDay;
using AutoMapper;
using Domain.Entities;

namespace Application.Features.Panel
{
    public class MappingProfile:Profile
    {
        public MappingProfile()
        {
            CreateMap<User, UserMemberShipLastDayQueryResponse>()
      .ForMember(dest => dest.LastDay, opt => opt.MapFrom(src => src.Membership.LastDay))
      .ReverseMap();

            CreateMap<User, UpdateUserStateResponse>().ReverseMap();

            CreateMap<User, CreatedCompanyLastMontlyQueryResponse>().ReverseMap();
        }
    }
}
