
using Core.Persistence.Repositories;

namespace Domain.Entities
{
    public class Feedback:Entity<Guid>
    {
       
        public Guid? CustomerId { get; set; }
        public Guid? UserId { get; set; }
        public Guid? VisitId { get; set; }
        public int? Rating { get; set; }
        public string? Comments { get; set; }
        public Customer? Customer { get; set; }
        public User? User { get; set; }
        public Visit? Visit { get; set; }

    }
}
