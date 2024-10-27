using Application.Features.Users.Commands.UpdateProfile;
using Application.Features.Users.Commands.UpdateUserRole;
using Application.Features.Users.Queries.GetById;

namespace SmartVisitServer.Web.Services.Users
{
    public interface IUserService
    {
        Task<UpdateUserRoleCommandResponse> UpdateUserRoleAsync(UpdateUserRoleCommandRequest request);
        Task<UpdateProfileCommandResponse> UpdateProfileAsync(UpdateProfileCommandRequest request);
        Task<GetByIdUserResponse> GetProfileAsync();
      
    }
}
