namespace Application.Features.Customers.Commands.Delete
{
    public class CustomerDeleteCommandResponse
    {
        public Guid Id {  get; set; }
        public string NameSurname { get; set; } = string.Empty;
        public string ProductName { get; set; } = string.Empty ;
        public string Phone { get; set; } = string.Empty;
        public string Description { get; set; }= string.Empty;
    }
}
