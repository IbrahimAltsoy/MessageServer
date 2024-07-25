using Core.Persistence.Repositories;
using Domain.Common;

namespace Domain.Entities
{
    public class SmsTemplate: BaseEntity<Guid>
    {
       
        public Guid? UserId { get; set; }
        public string TemplateType { get; set; } // Örneğin: Teşekkür, Bildirim
        public string MessageTemplate { get; set; }
        public User? User { get; set; }

    }
}
