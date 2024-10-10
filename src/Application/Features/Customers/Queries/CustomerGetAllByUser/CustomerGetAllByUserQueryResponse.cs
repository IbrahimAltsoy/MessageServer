namespace Application.Features.Customers.Queries.CustomerGetAllByUser
{
    public class CustomerGetAllByUserQueryResponse
    {
        public Guid Id { get; set; }
        public string NameSurname { get; set; }
        public string Phone {  get; set; }
        public string ProductName { get; set; }
        public string Description { get; set; }
        public DateTime CreatedDate { get; set; }

    }
}
