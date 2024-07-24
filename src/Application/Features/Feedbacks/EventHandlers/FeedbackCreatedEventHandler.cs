using Domain.Events;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Application.Features.Feedbacks.EventHandlers
{
    public class FeedbackCreatedEventHandler : INotificationHandler<FeedbackCreatedEvent>
    {
        readonly ILogger<FeedbackCreatedEventHandler> _logger;

        public FeedbackCreatedEventHandler(ILogger<FeedbackCreatedEventHandler> logger)
        {
            _logger = logger;
        }

        public Task Handle(FeedbackCreatedEvent notification, CancellationToken cancellationToken)
        {
            var eventName = notification.GetType().Name;
            _logger.LogInformation($"FeedbackCreatedEventHandler is working. Event: {eventName}");
            return Task.CompletedTask;
        }
    }
}
