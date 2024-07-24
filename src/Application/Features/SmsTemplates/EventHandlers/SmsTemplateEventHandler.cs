using Domain.Events;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Application.Features.SmsTemplates.EventHandlers
{
    public class SmsTemplateEventHandler : INotificationHandler<SmsTemplateCreatedEvent>
    {
        readonly ILogger<SmsTemplateEventHandler> _logger;

        public SmsTemplateEventHandler(ILogger<SmsTemplateEventHandler> logger)
        {
            _logger = logger;
        }

        public Task Handle(SmsTemplateCreatedEvent notification, CancellationToken cancellationToken)
        {
            var eventName = notification.GetType().Name;
            _logger.LogInformation($"SmsTemplateEventHandler is working. Event: {eventName}");
            return Task.CompletedTask;
        }
    }
}
