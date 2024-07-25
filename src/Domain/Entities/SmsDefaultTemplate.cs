using Core.Persistence.Repositories;
using Domain.Common;
using Domain.Enums;

namespace Domain.Entities
{
    public class SmsDefaultTemplate: BaseEntity<Guid>
    {
        public SmsEventType SmsEventType { get; set; }
        public string Content { get; set; }
    }
}
