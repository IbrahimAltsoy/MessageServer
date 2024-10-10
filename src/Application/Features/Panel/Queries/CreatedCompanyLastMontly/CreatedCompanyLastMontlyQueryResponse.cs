using Domain.Enums;

namespace Application.Features.Panel.Queries.CreatedCompanyLastMontly
{
    public class CreatedCompanyLastMontlyQueryResponse
    {
        public Guid Id { get; set; }
        public string? CompanyName { get; set; }
        public string? QRCode { get; set; }
        public string? Phone { get; set; }
        public UserStatus UserStatus { get; set; }        
    }
}
