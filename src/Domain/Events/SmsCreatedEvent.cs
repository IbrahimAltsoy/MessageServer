using Domain.Common;
using Domain.Entities;

namespace Domain.Events
{
    public class SmsCreatedEvent:BaseEvent
    {
        public Sms Sms { get; set; }
        public SmsCreatedEvent(Sms sms) { Sms = sms; }
    }
}
