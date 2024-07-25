using Core.Persistence.Repositories;
using Domain.Common;

namespace Domain.Entities
{
    public class VisitHistory: BaseEntity<Guid>
    {
       
        public Guid? VisitId { get; set; }
        public DateTime StatusChangeTime { get; set; }
        public string NewStatus { get; set; }
        public Visit? Visit { get; set; }

    }
}
