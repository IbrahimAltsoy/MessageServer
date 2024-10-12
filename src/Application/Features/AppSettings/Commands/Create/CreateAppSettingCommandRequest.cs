using MediatR;

namespace Application.Features.AppSettings.Commands.Create
{
    public class CreateAppSettingCommandRequest:IRequest<CreateAppSettingCommandResponse>
    {
        public string Value { get; set; }
        public string Key { get; set; }
    }
}
