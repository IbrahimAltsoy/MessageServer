using MediatR;

namespace Application.Features.AppSettings.Commands.Delete
{
    public class DeleteAppSettingCommandRequest:IRequest<DeleteAppSettingCommandResponse>
    {
        public Guid Id { get; set; }
    }
}
