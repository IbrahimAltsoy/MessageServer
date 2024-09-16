using MediatR;

namespace Application.Features.Auth.Commands.RegisterPhoneCode
{
    public class LoginPhoneCodeRequest:IRequest<LoginPhoneCodeResponse>
    {
        public string Phone { get; set; }
        public string AuthenticatorCode { get; set; }
        public string IPAddress { get; set; }
    }
}
