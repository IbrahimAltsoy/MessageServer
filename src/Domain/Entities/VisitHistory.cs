using Core.Persistence.Repositories;

namespace Domain.Entities
{
    public class VisitHistory:Entity<Guid>
    {
       
        public Guid? VisitId { get; set; }
        public DateTime StatusChangeTime { get; set; }
        public string NewStatus { get; set; }
        public Visit? Visit { get; set; }

    }
}
