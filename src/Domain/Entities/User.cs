using Core.Persistence.Repositories;
using Domain.Enums;

namespace Domain.Entities;

public class User : Entity<Guid>
{
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string? CompanyName { get; set; }
    public string Email { get; set; }
    public string? IbanNumber { get; set; }
    public string? Adress { get; set; }
    public string? LogoUrl { get; set; }
    public int? AmountOfSms { get; set; }
    public DateTime? EmailVerified { get; set; }
    public string? Phone { get; set; }
    public DateTime? PhoneVerified { get; set; }
    public byte[] PasswordSalt { get; set; }
    public byte[] PasswordHash { get; set; }
    public AuthenticatorType AuthenticatorType { get; set; } = AuthenticatorType.None;
    public UserStatus UserStatus { get; set; } = UserStatus.Passive;

    public virtual ICollection<UserOperationClaim> UserOperationClaims { get; set; } = null!;
    public virtual ICollection<RefreshToken> RefreshTokens { get; set; } = null!;
    public virtual ICollection<Notification> Notifications { get; set; } = null!;


    public ICollection<Employee>? Visits { get; set; }
    public ICollection<Feedback>? Feedbacks { get; set; }
    public NotificationSettings? NotificationSettings { get; set; }   
    public ICollection<Customer>? Customers { get; set; }
    public ICollection<SmsSettings>? SmsSettingies { get; set; }
    public ICollection<SmsCustomTemplate>? SmsCustomTemplates { get; set; }
    public ICollection<Sms>? Smses { get; set; }

    // TODO: abonelik

    public User()
    {
        FirstName = string.Empty;
        LastName = string.Empty;
        Email = string.Empty;
        Phone = string.Empty;
        PasswordHash = Array.Empty<byte>();
        PasswordSalt = Array.Empty<byte>();
        EmailVerified = null;
        PhoneVerified = null;
        UserOperationClaims = new HashSet<UserOperationClaim>();
        RefreshTokens = new HashSet<RefreshToken>();
        AuthenticatorType = AuthenticatorType.None;
        UserStatus = UserStatus.Passive;
    }

    public User(
        string firstName,
        string lastName,
        string email,
        byte[] passwordSalt,
        byte[] passwordHash,
        string phone,
        UserStatus status,
        AuthenticatorType authenticatorType
    )
    {
        FirstName = firstName;
        LastName = lastName;
        Email = email;
        PasswordSalt = passwordSalt;
        PasswordHash = passwordHash;
        EmailVerified = null;
        PhoneVerified = null;
        Phone = phone;
        UserStatus = status;
        AuthenticatorType = authenticatorType;
    }

    public User(
        Guid id,
        string firstName,
        string lastName,
        string email,
        byte[] passwordSalt,
        byte[] passwordHash,
        string phone,
        UserStatus status,
        AuthenticatorType authenticatorType
    ) : base(id)
    {
        FirstName = firstName;
        LastName = lastName;
        Email = email;
        PasswordSalt = passwordSalt;
        PasswordHash = passwordHash;
        Phone = phone;
        UserStatus = status;
        AuthenticatorType = authenticatorType;
    }
}
