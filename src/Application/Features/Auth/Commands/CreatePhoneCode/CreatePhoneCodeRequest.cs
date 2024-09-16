using MediatR;

namespace Application.Features.Auth.Commands.CreatePhoneCode
{
    public class CreatePhoneCodeRequest: IRequest<CreatePhoneCodeResponse>
    {
        public string Phone {  get; set; }
    }
}
