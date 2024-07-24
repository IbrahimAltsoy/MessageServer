using Domain.Common;
using Domain.Entities;

namespace Domain.Events
{
    public class FeedbackCreatedEvent:BaseEvent
    {
        public Feedback Feedback { get; }
        public FeedbackCreatedEvent(Feedback feedback) { Feedback = feedback; }
    }
}
