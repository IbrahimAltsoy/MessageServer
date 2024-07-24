using Domain.Events;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Application.Features.Reports.EventHandlers
{
    public class ReportEventHandler : INotificationHandler<ReportCreatedEvent>
    {
        readonly ILogger<ReportEventHandler> _logger;

        public ReportEventHandler(ILogger<ReportEventHandler> logger)
        {
            _logger = logger;
        }

        public Task Handle(ReportCreatedEvent notification, CancellationToken cancellationToken)
        {
            var eventName = notification.GetType().Name;
            _logger.LogInformation($"ReportEventHandler is working. Event: {eventName}");
            return Task.CompletedTask;
        }
    }
}
