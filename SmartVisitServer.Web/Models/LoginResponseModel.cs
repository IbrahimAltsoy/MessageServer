namespace SmartVisitServer.Web.Models
{
    public class LoginResponseModel
    {
        public AccessToken AccessToken { get; set; }
        public User User { get; set; }
    }
    public class AccessToken
    {
        public string Token { get; set; }
        public string RefreshToken { get; set; }
        public DateTime Expiration { get; set; }
    }
    public class User
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
    }
}
