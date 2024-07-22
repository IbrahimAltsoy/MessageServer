using AutoMapper;
using U=Domain.Entities;

namespace Domain.Dtos.User
{
    public class MappingProfile:Profile
    {
        public MappingProfile()
        {
            CreateMap<U.User, UserDto>().ReverseMap();
            CreateMap<U.User, UserUpdateDto>().ReverseMap();
        }
    }
}
