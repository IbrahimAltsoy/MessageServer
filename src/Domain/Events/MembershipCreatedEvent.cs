using Domain.Common;
using Domain.Entities;

namespace Domain.Events
{
    public class MembershipCreatedEvent:BaseEvent
    {
        public Membership Membership { get; }
        public MembershipCreatedEvent(Membership membership) { Membership = membership; }
    }
}
