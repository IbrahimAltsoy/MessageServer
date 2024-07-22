using Application.Services.NotificationService;
using MediatR;

namespace Application.Features.Notificatiton.Commands.ReadAllUserNotification;

public class ReadAllUserNotificationCommand : IRequest
{
    public Guid UserId { get; set; }

    public class ReadAllUserNotificationCommandHandler : IRequestHandler<ReadAllUserNotificationCommand>
    {
        private readonly INotificationService _notificationService;

        public ReadAllUserNotificationCommandHandler(INotificationService notificationService)
        {
            _notificationService = notificationService;
        }

        public async Task Handle(ReadAllUserNotificationCommand request, CancellationToken cancellationToken)
        {
            await _notificationService.ReadAllNotification(request.UserId);
        }
    }
}
