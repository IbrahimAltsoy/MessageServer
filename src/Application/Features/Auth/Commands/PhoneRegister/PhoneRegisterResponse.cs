using Application.Features.Auth.Dtos;
using Core.Security.JWT;

namespace Application.Features.Auth.Commands.PhoneRegister
{
    public class PhoneRegisterResponse
    {
        public string Code { get; set; }
    }
}
