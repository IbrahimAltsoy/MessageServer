using Application.Features.OperationClaims.Queries.GetAllUsersRole;
using Application.Features.Users.Commands.Create;
using Application.Features.Users.Commands.Delete;
using Application.Features.Users.Commands.Update;
using Application.Features.Users.Commands.UpdateFromAuth;
using Application.Features.Users.Commands.UpdateProfile;
using Application.Features.Users.Commands.UpdateUserRole;
using Application.Features.Users.Queries.GetById;
using Application.Features.Users.Queries.GetList;
using AutoMapper;
using Core.Application.Responses;
using Core.Persistence.Paging;
using Domain.Entities;

namespace Application.Features.Users.Profiles;

public class MappingProfiles : Profile
{
    public MappingProfiles()
    {
        CreateMap<User, CreateUserCommand>().ReverseMap();
        CreateMap<User, CreatedUserResponse>().ReverseMap();
        CreateMap<User, UpdateUserCommand>().ReverseMap();
        CreateMap<User, UpdatedUserResponse>().ReverseMap();
        CreateMap<User, UpdateUserFromAuthCommand>().ReverseMap();
        CreateMap<User, UpdatedUserFromAuthResponse>().ReverseMap();
        CreateMap<User, DeleteUserCommand>().ReverseMap();
        CreateMap<User, DeletedUserResponse>().ReverseMap();
        CreateMap<User, GetByIdUserResponse>().ReverseMap();
        CreateMap<User, GetListUserListItemDto>().ReverseMap();
        CreateMap<IPaginate<User>, GetListResponse<GetListUserListItemDto>>().ReverseMap();

        CreateMap<User, UpdateProfileCommandRequest>().ReverseMap();
        CreateMap<User, UpdateProfileCommandResponse>().ReverseMap();

        CreateMap<User, UpdateUserRoleCommandResponse>()
    .ForMember(dest => dest.RoleName, opt => opt.MapFrom(src => string.Join(", ", src.UserOperationClaims.Select(x => x.OperationClaim.Name))))
    .ReverseMap();
    }
}
