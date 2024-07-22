using Core.Application.Responses;
using Core.Security.JWT;
using Domain.Enums;

namespace Application.Features.Users.Commands.UpdateFromAuth;

public class UpdatedUserFromAuthResponse : IResponse
{
    public Guid Id { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string Email { get; set; }
    public DateTime EmailVerified { get; set; }
    public string Phone { get; set; }
    public DateTime PhoneVerified { get; set; }
    public AuthenticatorType AuthenticatorType { get; set; }
    public UserStatus UserStatus { get; set; }
    //public AccessToken AccessToken { get; set; }
}
