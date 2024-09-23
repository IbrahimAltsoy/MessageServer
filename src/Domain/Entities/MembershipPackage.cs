using Core.Persistence.Repositories;
using Domain.Enums;

namespace Domain.Entities
{
    public class MembershipPackage:Entity<Guid>
    {
        public string? ImageUrl { get; set; }
        public string? Name { get; set; }
        public string? Description { get; set; }
        public int? SmsCount { get; set; }
        public int? AddDay { get; set; }
        public int? CompanyCount { get; set; }
        public decimal? Price { get; set; }
        public MembershipPackageType? MembershipPackageType { get; set; }
    }
}
