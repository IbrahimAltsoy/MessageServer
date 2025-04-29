using Core.Persistence.Repositories;
using Domain.Common;
using Domain.Enums;

namespace Domain.Entities
{
    public class Customer: BaseEntity<Guid>
    {
       public Guid UserId { get; set; }
        public string NameSurname { get; set; }
        public string ProductName { get; set; }
        public string Phone { get; set; }
        public string Description { get; set; }
        //public string? QRCode { get; set; }
        public CustomerStatus Status { get; set; }
        public int? Pointed { get; set; } = 1;
       
        public ICollection<Feedback> Feedbacks { get; set; }
        public User User { get; set; }
        public ICollection<Sms>? Smses { get; set; } = new List<Sms>();
        public ICollection<CustomerPhoto>? CustomerPhotos { get; set; } = new List<CustomerPhoto>();
    }
}
