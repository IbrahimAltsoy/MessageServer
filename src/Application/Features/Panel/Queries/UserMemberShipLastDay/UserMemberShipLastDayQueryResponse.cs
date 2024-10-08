using Domain.Enums;

namespace Application.Features.Panel.Queries.UserMemberShipLastDay
{
    public class UserMemberShipLastDayQueryResponse
    {
        public Guid Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string? CompanyName { get; set; }
        public string? QRCode { get; set; }
        public string? Phone { get; set; }
        public UserStatus UserStatus { get; set; }
        public DateTime? LastDay { get; set; }
    }
}
