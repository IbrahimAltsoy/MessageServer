namespace SmartVisitServer.Web.Models
{
    public class UserQrCode
    {
        public Guid Id { get; set; }
        public string Email { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string QRCode { get; set; } // QR kodu Base64 string olarak saklanır
    }
}
