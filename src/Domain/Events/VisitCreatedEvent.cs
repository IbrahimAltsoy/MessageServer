using Domain.Common;
using Domain.Entities;

namespace Domain.Events
{
    public class VisitCreatedEvent:BaseEvent
    {
        public Visit Visit { get; }
        public VisitCreatedEvent(Visit visit) { Visit = visit; }
    }
}
