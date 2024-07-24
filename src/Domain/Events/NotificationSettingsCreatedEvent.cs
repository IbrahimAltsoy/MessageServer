using Domain.Common;
using Domain.Entities;

namespace Domain.Events
{
    public class NotificationSettingsCreatedEvent:BaseEvent
    {
        public NotificationSettings NotificationSettings { get; }
        public NotificationSettingsCreatedEvent(NotificationSettings notificationSettings) { NotificationSettings = notificationSettings; }
    }
}
