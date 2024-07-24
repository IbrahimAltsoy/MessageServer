using Domain.Events;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Application.Features.Customers.EventHandlers
{
    public class CustomerCreatedEventHandler : INotificationHandler<CustomerCreatedEvent>
    {
        readonly ILogger<CustomerCreatedEventHandler> _logger;

        public CustomerCreatedEventHandler(ILogger<CustomerCreatedEventHandler> logger)
        {
            _logger = logger;
        }

        public Task Handle(CustomerCreatedEvent notification, CancellationToken cancellationToken)
        {
            var eventName = notification.GetType().Name;
            _logger.LogInformation($"CustomerCreatedEventHandler is working. Event: {eventName}");
            return Task.CompletedTask;
        }
    }
}
