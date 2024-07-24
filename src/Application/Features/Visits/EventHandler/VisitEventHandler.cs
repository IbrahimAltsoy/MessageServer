using Domain.Events;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Application.Features.Visits.EventHandler
{
    public class VisitEventHandler : INotificationHandler<VisitCreatedEvent>
    {
        readonly ILogger<VisitEventHandler> _logger;

        public VisitEventHandler(ILogger<VisitEventHandler> logger)
        {
            _logger = logger;
        }

        public Task Handle(VisitCreatedEvent notification, CancellationToken cancellationToken)
        {
            var eventName = notification.GetType().Name;
            _logger.LogInformation($"VisitEventHandler is working. Event: {eventName}");
            return Task.CompletedTask;
        }
    }
}
