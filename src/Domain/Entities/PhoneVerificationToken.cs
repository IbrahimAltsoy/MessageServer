using Core.Persistence.Repositories;

namespace Domain.Entities;

public class PhoneVerificationToken : Entity<Guid>
{
    public Guid UserId { get; set; }
    public string Token { get; set; }
    public DateTime? Expires { get; set; }

    public PhoneVerificationToken()
    {
        UserId = Guid.Empty;
        Token = string.Empty;
        Expires = null;
    }
    public PhoneVerificationToken(Guid userId, string token, DateTime expires)
    {
        UserId = userId;
        Token = token;
        Expires = expires;
    }
    public PhoneVerificationToken(Guid id, Guid userId, string token, DateTime expires) : base(id)
    {
        UserId = userId;
        Token = token;
        Expires = expires;
    }
}