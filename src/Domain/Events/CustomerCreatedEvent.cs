using Domain.Common;
using Domain.Entities;

namespace Domain.Events
{
    public class CustomerCreatedEvent:BaseEvent
    {
        public Customer Customer { get; }
        public CustomerCreatedEvent(Customer customer) { Customer = customer; }
    }
}
