using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Persistence.Configurations
{
    public class MembershipPackageConfigration : IEntityTypeConfiguration<MembershipPackage>
    {
        public void Configure(EntityTypeBuilder<MembershipPackage> builder)
        {
            builder.Property(e => e.Name).IsRequired();
            builder.Property(e => e.AddDay).IsRequired();
            builder.Property(e => e.SmsCount).IsRequired();
           
        }
    }
}
