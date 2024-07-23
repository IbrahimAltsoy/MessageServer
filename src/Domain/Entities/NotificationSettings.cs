using Core.Persistence.Repositories;

namespace Domain.Entities
{
    public class NotificationSettings:Entity<Guid>
    {
       
        public Guid? UserId { get; set; }
        public bool? EnableEmail { get; set; }
        public bool? EnableSms { get; set; }
        public string SmsProvider { get; set; }
        public string ApiKey { get; set; }
        public User? User { get; set; }

    }
}
