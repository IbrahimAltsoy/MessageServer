using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Persistence.Configurations
{
    public class NotificationSettingsConfigration : IEntityTypeConfiguration<NotificationSettings>
    {
        public void Configure(EntityTypeBuilder<NotificationSettings> builder)
        {
            builder.Property(e=>e.SmsProvider).IsRequired();
           
            builder.HasQueryFilter(p => p.DeletedDate == null);
        }
    }
}
