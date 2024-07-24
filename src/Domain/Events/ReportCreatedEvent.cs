using Domain.Common;
using Domain.Entities;

namespace Domain.Events
{
    public class ReportCreatedEvent:BaseEvent
    {
        public Report Report { get; }
        public ReportCreatedEvent(Report report) { Report = report; }
    }
}
