using Application.Services.NotificationService;
using MediatR;

namespace Application.Features.Notificatiton.Commands.DeleteAllUserNotification;

public class DeleteAllUserNotificationCommand : IRequest
{
    public Guid UserId { get; set; }

    public class DeleteAllUserNotificationCommandHandler : IRequestHandler<DeleteAllUserNotificationCommand>
    {
        private readonly INotificationService _notificationService;

        public DeleteAllUserNotificationCommandHandler(INotificationService notificationService)
        {
            _notificationService = notificationService;
        }

        public async Task Handle(DeleteAllUserNotificationCommand request, CancellationToken cancellationToken)
        {
            await _notificationService.DeleteUserNotifications(request.UserId);
        }
    }
}
