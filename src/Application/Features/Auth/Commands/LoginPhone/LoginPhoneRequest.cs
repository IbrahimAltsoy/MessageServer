using MediatR;

namespace Application.Features.Auth.Commands.LoginPhone
{
    public class LoginPhoneRequest:IRequest<LoginPhoneResponse>
    {
        public string Phone { get; set; }
        public string Password { get; set; }
        public string? AuthenticatorCode { get; set; }
        public string IPAddress { get; set; }
    }
}
