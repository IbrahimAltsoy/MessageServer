using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Persistence.Configurations
{
    public class SmsTemplateConfigration : IEntityTypeConfiguration<SmsTemplate>
    {
        public void Configure(EntityTypeBuilder<SmsTemplate> builder)
        {
            builder.Property(e=>e.TemplateType).IsRequired();
            builder.Property(e=>e.MessageTemplate).IsRequired();

            builder.HasQueryFilter(p => p.DeletedDate == null);
        }
    }
}
