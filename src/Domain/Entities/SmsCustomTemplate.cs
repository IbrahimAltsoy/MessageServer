using Domain.Common;

namespace Domain.Entities
{
    public class SmsCustomTemplate:BaseEntity<Guid>
    {
        public string Title { get; set; }
        public string Content { get; set; }

        public Guid? UserId { get; set; }
        public User? User { get; set; }
       
    }
}
  