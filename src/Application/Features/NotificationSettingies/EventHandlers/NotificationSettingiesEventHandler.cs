using Domain.Events;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Application.Features.NotificationSettingies.EventHandlers
{
    public class NotificationSettingiesEventHandler : INotificationHandler<NotificationSettingsCreatedEvent>
    {
        readonly ILogger<NotificationSettingiesEventHandler> _logger;

        public NotificationSettingiesEventHandler(ILogger<NotificationSettingiesEventHandler> logger)
        {
            _logger = logger;
        }

        public Task Handle(NotificationSettingsCreatedEvent notification, CancellationToken cancellationToken)
        {
            var eventName = notification.GetType().Name;
            _logger.LogInformation($"NotificationSettingiesEventHandler is working. Event: {eventName}");
            return Task.CompletedTask;
        }
    }
}
