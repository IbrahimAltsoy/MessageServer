using Domain.Enums;

namespace Application.Features.Customers.Queries.CustomerGetAllByUser
{
    public class CustomerGetAllByUserQueryResponse
    {
        public Guid Id { get; set; }
        public string NameSurname { get; set; }=string.Empty;
        public string Phone {  get; set; } = string.Empty;
        public string ProductName { get; set; } =string.Empty;
        public string Description { get; set; } = string.Empty ;
        public DateTime CreatedDate { get; set; } 
        public CustomerStatus Status { get; set; }
        public List<string> PhotoUrls { get; set; } = new();
        public int? Pointed { get; set; } = 1;
    }
}
