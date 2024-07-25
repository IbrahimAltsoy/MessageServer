using Core.Persistence.Repositories;
using Domain.Common;

namespace Domain.Entities
{
    public class Customer: BaseEntity<Guid>
    {
       public Guid UserId { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public string Phone { get; set; }
        public string Description { get; set; }

        public ICollection<Visit> Visits { get; set; }
        public ICollection<Feedback> Feedbacks { get; set; }
        public User User { get; set; }
        public ICollection<Sms>? Smses { get; set; }

    }
}
