using Domain.Common;

namespace Domain.Entities
{
    public class CustomerPhoto:BaseEntity<Guid>
    {
        public Guid? CustomerId { get; set; }
        public string? PhotoUrl { get; set; }
        public Customer? Customer { get; set; }
    }
}
