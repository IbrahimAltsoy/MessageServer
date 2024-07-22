using Core.Persistence.Repositories;

namespace Domain.Entities;

public class EmailVerificationToken : Entity<Guid>
{
    public string Email { get; set; }
    public string Token { get; set; }
    public DateTime? Expires { get; set; }

    public EmailVerificationToken()
    {
        Email = string.Empty;
        Token = string.Empty;
        Expires = null;
    }
    public EmailVerificationToken(string email, string token, DateTime expires)
    {
        Email = email;
        Token = token;
        Expires = expires;
    }
    public EmailVerificationToken(Guid id, string email, string token, DateTime expires) : base(id)
    {
        Email = email;
        Token = token;
        Expires = expires;
    }
}