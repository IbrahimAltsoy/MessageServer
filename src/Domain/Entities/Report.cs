using Core.Persistence.Repositories;
using Domain.Common;

namespace Domain.Entities
{
    public class Report: BaseEntity<Guid>
    {
       
        public Guid? UserId { get; set; }
        //public string ReportType { get; set; }
        //public DateTime GeneratedDate { get; set; }
        public string Content { get; set; }
        public string? Title { get; set; }
        public string CustomerListJson { get; set; }
        public int? SmsCount { get; set; }
        public int? CustomerCount { get; set; }
        public User? User { get; set; }

    }
}
