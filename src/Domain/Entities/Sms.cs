using Domain.Common;
using System.Reflection;

namespace Domain.Entities
{
    public class Sms:BaseEntity<Guid>//Configrationları ayarlamayı unutma buranın contentini başka yerlerden doldur. 
    {
        public string Content { get; set; }

        public Guid? CustomerId { get; set; }
        public Guid? UserId { get; set; }    
        public Customer Customer { get; set; }
        public User User { get; set; }
       
    }
}
