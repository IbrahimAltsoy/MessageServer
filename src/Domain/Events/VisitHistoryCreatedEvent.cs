using Domain.Common;
using Domain.Entities;

namespace Domain.Events
{
    public class VisitHistoryCreatedEvent:BaseEvent
    {
        public VisitHistory VisitHistory { get; }
        public VisitHistoryCreatedEvent(VisitHistory visitHistory) { VisitHistory = visitHistory; }
    }
}
