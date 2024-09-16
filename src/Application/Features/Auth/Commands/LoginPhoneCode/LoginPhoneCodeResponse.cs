using Application.Features.Auth.Dtos;
using Core.Security.JWT;
using Domain.Enums;

namespace Application.Features.Auth.Commands.RegisterPhoneCode
{
    public class LoginPhoneCodeResponse
    {
        public AccessToken? AccessToken { get; set; }
        public LoginPhoneUserDto? User { get; set; }
        public AuthenticatorType? RequiredAuthenticatorType { get; set; }
    }
}
