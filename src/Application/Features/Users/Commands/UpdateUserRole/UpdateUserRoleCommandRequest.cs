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
        public Guid OldOperationClaimId { get; set; }
        public Guid NewOperationClaimId { get; set; }
    }
}
