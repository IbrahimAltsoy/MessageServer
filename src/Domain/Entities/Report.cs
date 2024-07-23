using Core.Persistence.Repositories;

namespace Domain.Entities
{
    public class Report:Entity<Guid>
    {
       
        public Guid? UserId { get; set; }
        public string ReportType { get; set; }
        public DateTime GeneratedDate { get; set; }
        public string Content { get; set; }
        public User? User { get; set; }

    }
}
