namespace SmartVisitServer.Web.Models
{
    public class CustomerModel
    {
        public Guid Id { get; set; }
        public string NameSurname { get; set; }       
        public string? ProductName { get; set; }       
        public string Phone { get; set; }
        public string? Description { get; set; }
        public Guid? UserId { get; set; }
    }
}
