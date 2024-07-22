using Core.Persistence.Repositories;

namespace Domain.Entities;

public class UserOperationClaim : Entity<Guid>
{
    public Guid UserId { get; set; }
    public Guid OperationClaimId { get; set; }

    public virtual User User { get; set; } = null!;
    public virtual OperationClaim OperationClaim { get; set; } = null!;

    public UserOperationClaim(Guid userId, Guid operationClaimId)
    {
        UserId = userId;
        OperationClaimId = operationClaimId;
    }

    public UserOperationClaim(Guid id, Guid userId, Guid operationClaimId) : base(id)
    {
        UserId = userId;
        OperationClaimId = operationClaimId;
    }
}
