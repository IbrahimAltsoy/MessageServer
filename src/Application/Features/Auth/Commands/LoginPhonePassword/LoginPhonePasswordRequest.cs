using Domain.Enums;
using MediatR;

namespace Application.Features.Auth.Commands.LoginPhone
{
    public class LoginPhonePasswordRequest : IRequest<LoginPhonePasswordResponse>
    {
        public string Phone { get; set; }
        public string? Password { get; set; }
        public string? IPAddress { get; set; }
    }
}
