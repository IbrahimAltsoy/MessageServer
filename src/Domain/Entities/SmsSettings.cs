using Domain.Common;
using Domain.Enums;

namespace Domain.Entities
{
    public class SmsSettings:BaseEntity<Guid>
    {
        public bool ProductReceived { get; set; }
        public bool ProductIsReady { get; set; }
        public Guid? UserId { get; set; }
        public User? User { get; set; }
    }
}
