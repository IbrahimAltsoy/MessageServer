using Domain.Enums;
using MediatR;

namespace Application.Features.Panel.Command.UpdateUserStatus
{
    public class UpdateUserStateRequest:IRequest<UpdateUserStateResponse>
    {
        public Guid Id { get; set; }
        public UserStatus UserStatus { get; set; }
    }
}
