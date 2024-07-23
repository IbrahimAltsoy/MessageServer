using Core.Persistence.Repositories;

namespace Domain.Entities
{
    public class Customer:Entity<Guid>
    {
       
        public string Name { get; set; }
        public string Surname { get; set; }
        public string Phone { get; set; }

        public ICollection<Visit> Visits { get; set; }
        public ICollection<Feedback> Feedbacks { get; set; }

    }
}
