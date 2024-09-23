using Domain.Common;
using Domain.Entities;

namespace Domain.Events
{
    public class AppSettingCreatedEvent:BaseEvent
    {
        public AppSetting AppSetting { get; }
        public AppSettingCreatedEvent(AppSetting appSetting) { AppSetting = appSetting; }
    }
}
