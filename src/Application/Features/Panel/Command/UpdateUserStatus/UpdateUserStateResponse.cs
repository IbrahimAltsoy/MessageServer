using Domain.Enums;

namespace Application.Features.Panel.Command.UpdateUserStatus
{
    public class UpdateUserStateResponse
    {
        public Guid Id { get; set; }
        public UserStatus UserStatus { get; set; }
    }
}
