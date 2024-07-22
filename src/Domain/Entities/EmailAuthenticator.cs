using Core.Persistence.Repositories;

namespace Core.Security.Entities;

public class EmailAuthenticator : Entity<Guid>
{
    public Guid UserId { get; set; }
    public string? ActivationKey { get; set; }
    public DateTime? Expires { get; set; }

    public EmailAuthenticator()
    {
        UserId = Guid.Empty;
        ActivationKey = String.Empty;
    }

    public EmailAuthenticator(Guid userId, string? activationKey)
    {
        UserId = userId;
        ActivationKey = activationKey;
    }

    public EmailAuthenticator(Guid id, Guid userId, string? activationKey) : this()
    {
        Id = id;
        UserId = userId;
        ActivationKey = activationKey;
    }
}