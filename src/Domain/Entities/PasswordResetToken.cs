using Core.Persistence.Repositories;

namespace Domain.Entities;

public class PasswordResetToken : Entity<Guid>
{
    public string Email { get; set; }
    public string Token { get; set; }
    public DateTime? Expires { get; set; }

    public PasswordResetToken()
    {
        Email = string.Empty;
        Token = string.Empty;
        Expires = null;
    }
    public PasswordResetToken(string email, string token, DateTime expires)
    {
        Email = email;
        Token = token;
        Expires = expires;
    }
    public PasswordResetToken(Guid id, string email, string token, DateTime expires) : base(id)
    {
        Email = email;
        Token = token;
        Expires = expires;
    }
}