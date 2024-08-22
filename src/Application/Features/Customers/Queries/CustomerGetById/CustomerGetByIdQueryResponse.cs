namespace Application.Features.Customers.Queries.CustomerGetById
{
    public class CustomerGetByIdQueryResponse
    {
        public Guid Id { get; set; }
        public string NameSurname { get; set; }
        public string ProductName { get; set; }
        public string Phone { get; set; }
        public string Description { get; set; }
    }
}
