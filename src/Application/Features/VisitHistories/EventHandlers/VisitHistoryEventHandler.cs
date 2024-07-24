using Domain.Events;
using MediatR;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.VisitHistories.EventHandlers
{
    public class VisitHistoryEventHandler : INotificationHandler<VisitHistoryCreatedEvent>
    {
        readonly ILogger<VisitHistoryEventHandler> _logger;

        public VisitHistoryEventHandler(ILogger<VisitHistoryEventHandler> logger)
        {
            _logger = logger;
        }

        public Task Handle(VisitHistoryCreatedEvent notification, CancellationToken cancellationToken)
        {
            var eventName = notification.GetType().Name;
            _logger.LogInformation($"VisitHistoryEventHandler is working. Event: {eventName}");
            return Task.CompletedTask;
        }
    }
}
