using Core.Persistence.Repositories;
using Domain.Common;

namespace Domain.Entities
{
    public class Employee:BaseEntity<Guid>
    {      
        public Guid? UserId { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public string Phone { get; set; }
        public string Role { get; set; }
        public User? User { get; set; }

    }
}
