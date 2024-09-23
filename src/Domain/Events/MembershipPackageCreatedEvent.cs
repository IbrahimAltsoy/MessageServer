using Domain.Common;
using Domain.Entities;

namespace Domain.Events
{
    public class MembershipPackageCreatedEvent:BaseEvent
    {
        public MembershipPackage MembershipPackage { get; }
        public MembershipPackageCreatedEvent(MembershipPackage membershipPackage) { MembershipPackage = membershipPackage; }
    }
}
