using Application.Features.Notificatiton.Queries.GetByAuth;
using AutoMapper;
using Domain.Entities;

namespace Application.Features.Notificatiton.Profiles;

public class MappingProfiles : Profile
{
    public MappingProfiles()
    {
        CreateMap<Notification, GetByAuthResponse>().ReverseMap();
    }
}
