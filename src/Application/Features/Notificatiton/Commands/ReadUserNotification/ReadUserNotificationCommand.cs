using Application.Services.NotificationService;
using MediatR;

namespace Application.Features.Notificatiton.Commands.ReadUserNotification;

public class ReadUserNotificationCommand : IRequest
{
    public Guid Id { get; set; }

    public class ReadUserNotificationCommandHandler : IRequestHandler<ReadUserNotificationCommand>
    {
        private readonly INotificationService _notificationService;

        public ReadUserNotificationCommandHandler(INotificationService notificationService)
        {
            _notificationService = notificationService;
        }
        public async Task Handle(ReadUserNotificationCommand request, CancellationToken cancellationToken)
        {
            await _notificationService.ReadNotification(request.Id);            
        }
    }
}
