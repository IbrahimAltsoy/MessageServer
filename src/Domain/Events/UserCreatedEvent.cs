using Domain.Common;
using Domain.Entities;

namespace Domain.Events
{
    public class UserCreatedEvent:BaseEvent
    {
        public User User { get; }
        public UserCreatedEvent(User user) { User = user; }
    }
}
