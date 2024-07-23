using Core.Persistence.Repositories;

namespace Domain.Entities
{
    public class Visit:Entity<Guid>
    {
       
        public Guid? CustomerId { get; set; }
        public Guid? UserId { get; set; }        
        public string? NameSurname { get; set; }
        public string? Phone { get; set; }

        public string? Description { get; set; }
        
        //public string Status { get; set; }
       
        public Customer? Customer { get; set; }
        public User? User { get; set; }
        public ICollection<VisitHistory> VisitHistories { get; set; }

    }
}
