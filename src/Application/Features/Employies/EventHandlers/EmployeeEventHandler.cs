using Domain.Events;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Application.Features.Employies.EventHandlers
{
    public class EmployeeEventHandler : INotificationHandler<EmployeeCratedEvent>
    {
        readonly ILogger<EmployeeEventHandler> _logger;

        public EmployeeEventHandler(ILogger<EmployeeEventHandler> logger)
        {
            _logger = logger;
        }

        public Task Handle(EmployeeCratedEvent notification, CancellationToken cancellationToken)
        {
            var eventName = notification.GetType().Name;
            _logger.LogInformation($"EmployeeEventHandler is working. Event: {eventName}");
            return Task.CompletedTask;
        }
    }
}
