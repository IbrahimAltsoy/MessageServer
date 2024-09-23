using Core.Persistence.Repositories;

namespace Domain.Entities
{
    public class Membership:Entity<Guid>
    {
        public Guid? UserId { get; set; }
        public int? SmsCount { get; set; }
        public DateTime? LastDay { get; set; }
        public int? CompanyCount { get; set; }

        public virtual User? User { get; set; } = null!;
    }
}
