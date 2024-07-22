using Core.Application.Responses;
using Domain.Entities;
using Core.Security.JWT;
using Application.Features.Auth.Dtos;
using Domain.Enums;

namespace Application.Features.Auth.Commands.Register;

public class RegisteredResponse : IResponse
{
    public AccessToken AccessToken { get; set; }
    public LoginUserDto User { get; set; }

    public LoggedHttpResponse ToHttpResponse() =>
    new()
    {
        AccessToken = AccessToken,
        User = User,
    };

    public class LoggedHttpResponse
    {
        public AccessToken? AccessToken { get; set; }
        public LoginUserDto User { get; set; }
        public AuthenticatorType? RequiredAuthenticatorType { get; set; }
    }
}
