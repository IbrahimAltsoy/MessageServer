using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Persistence.Configurations
{
    public class SmsTemplateConfigration : IEntityTypeConfiguration<SmsDefaultTemplate>
    {
        public void Configure(EntityTypeBuilder<SmsDefaultTemplate> builder)
        {
            builder.Property(e=>e.Content).IsRequired();
            //builder.Property(e=>e.Nam).IsRequired();

            builder.HasQueryFilter(p => p.DeletedDate == null);
        }
    }
}
