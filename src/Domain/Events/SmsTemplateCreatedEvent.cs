using Domain.Common;
using Domain.Entities;

namespace Domain.Events
{
    public class SmsTemplateCreatedEvent:BaseEvent
    {
        public SmsTemplate SmsTemplate { get; }
        public SmsTemplateCreatedEvent(SmsTemplate smsTemplate) { SmsTemplate = smsTemplate; }
    }
}
