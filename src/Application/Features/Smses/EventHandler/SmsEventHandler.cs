using Domain.Events;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Application.Features.Smses.EventHandler
{
    public class SmsEventHandler : INotificationHandler<SmsCreatedEvent>
    {
        readonly ILogger<SmsEventHandler> _logger;

        public SmsEventHandler(ILogger<SmsEventHandler> logger)
        {
            _logger = logger;
        }

        public Task Handle(SmsCreatedEvent notification, CancellationToken cancellationToken)
        {
            var eventName = notification.GetType().Name;
            _logger.LogInformation($"SmsCreatedEvent is working. Event: {eventName}");
            return Task.CompletedTask;
        }
    }

}
