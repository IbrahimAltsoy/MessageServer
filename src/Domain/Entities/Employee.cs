using Core.Persistence.Repositories;

namespace Domain.Entities
{
    public class Employee:Entity<Guid>
    {      
        public Guid? UserId { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public string Phone { get; set; }
        public string Role { get; set; }
        public User? User { get; set; }

    }
}
