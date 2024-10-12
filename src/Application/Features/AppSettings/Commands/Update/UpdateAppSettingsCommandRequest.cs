using MediatR;

namespace Application.Features.AppSettings.Commands.Update
{
    public class UpdateAppSettingsCommandRequest:IRequest<UpdateAppSettingsCommandResponse>
    {
        public Guid Id { get; set; }
        public string Key { get; set; }
        public string Value { get; set; }

    }
}
