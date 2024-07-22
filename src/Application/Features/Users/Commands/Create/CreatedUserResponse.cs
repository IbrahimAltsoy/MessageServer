using Core.Application.Responses;
using Domain.Enums;

namespace Application.Features.Users.Commands.Create;

public class CreatedUserResponse : IResponse
{
    public Guid Id { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string Email { get; set; }
    public DateTime? EmailVerified { get; set; }
    public string? Phone { get; set; }
    public DateTime? PhoneVerified { get; set; }
    public AuthenticatorType AuthenticatorType { get; set; }
    public UserStatus UserStatus { get; set; }
}
