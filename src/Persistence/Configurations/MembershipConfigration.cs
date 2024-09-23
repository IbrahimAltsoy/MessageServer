using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Persistence.Configurations
{
    public class MembershipConfigration : IEntityTypeConfiguration<Membership>
    {
        public void Configure(EntityTypeBuilder<Membership> builder)
        {
            builder.Property(e => e.UserId).IsRequired();
            builder.Property(e => e.LastDay).IsRequired();
            builder.Property(e => e.SmsCount).IsRequired();
        }
    }
}
