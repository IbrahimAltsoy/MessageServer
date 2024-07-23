using Core.Persistence.Repositories;

namespace Domain.Entities
{
    public class SmsTemplate:Entity<Guid>
    {
       
        public Guid? UserId { get; set; }
        public string TemplateType { get; set; } // Örneğin: Teşekkür, Bildirim
        public string MessageTemplate { get; set; }
        public User? User { get; set; }

    }
}
