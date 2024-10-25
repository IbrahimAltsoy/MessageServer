using Application.Features.Users.Commands.UpdateUserRole;

namespace SmartVisitServer.Web.Services.Users
{
    public interface IUserService
    {
        Task<UpdateUserRoleCommandResponse> UpdateUserRoleAsync(UpdateUserRoleCommandRequest request);
    }
}
