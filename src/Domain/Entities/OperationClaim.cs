using Core.Persistence.Repositories;

namespace Domain.Entities;

public class OperationClaim : Entity<Guid>
{
    public string Name { get; set; }

    public virtual ICollection<UserOperationClaim> UserOperationClaims { get; set; } = null!;

    public OperationClaim()
    {
        Name = string.Empty;
    }

    public OperationClaim(string name)
    {
        Name = name;
    }

    public OperationClaim(Guid id, string name) : base(id)
    {
        Name = name;
    }
}