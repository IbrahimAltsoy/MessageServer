using Domain.Entities;
using MediatR;

namespace Application.Features.Users.Commands.UpdateUserRole
{
    public class UpdateUserRoleCommandRequest:IRequest<UpdateUserRoleCommandResponse>
    {
        public Guid UserId { get; set; }
        public List<OperationClaimUpdate> OperationClaimUpdates { get; set; }

    }
    public class OperationClaimUpdate
    {
        public Guid OperationClaimId { get; set; } // Tıklanan rolün ID'si
        public bool IsAssigned { get; set; } // true: ekle, false: kaldır
    }
}
