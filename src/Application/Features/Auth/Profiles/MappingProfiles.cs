using Application.Features.Auth.Commands.PhoneRegister;
using Application.Features.Auth.Commands.RevokeToken;
using Application.Features.Auth.Dtos;
using AutoMapper;
using Domain.Entities;

namespace Application.Features.Auth.Profiles;

public class MappingProfiles : Profile
{
    public MappingProfiles()
    {
        CreateMap<RefreshToken, RevokedTokenResponse>().ReverseMap();
        CreateMap<User, RegisterPhoneRequest>().ReverseMap();
        CreateMap<User, LoginPhoneUserDto>().ReverseMap();
    }
}
