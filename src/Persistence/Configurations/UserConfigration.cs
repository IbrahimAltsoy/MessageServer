using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Persistence.Configurations
{
    public class UserConfigration : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.Property(e => e.FirstName).IsRequired();
            builder.Property(e=>e.LastName).IsRequired();
            builder.Property(e=>e.Phone).IsRequired();
            builder.HasCheckConstraint("CK_AmountOfSms", "\"AmountOfSms\" >= 0");

            // builder.HasQueryFilter(p => p.DeletedDate == null);
        }
    }
}
