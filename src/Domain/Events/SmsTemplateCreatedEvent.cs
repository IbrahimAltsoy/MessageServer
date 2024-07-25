using Domain.Common;
using Domain.Entities;

namespace Domain.Events
{
    public class SmsTemplateCreatedEvent:BaseEvent
    {
        public SmsDefaultTemplate SmsTemplate { get; }
        public SmsTemplateCreatedEvent(SmsDefaultTemplate smsTemplate) { SmsTemplate = smsTemplate; }
    }
}
