using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Persistence.Configurations
{
    public class AppSettingConfigration : IEntityTypeConfiguration<AppSetting>
    {
        public void Configure(EntityTypeBuilder<AppSetting> builder)
        {
            builder.Property(e => e.Key).IsRequired();
            builder.Property(c => c.Value).IsRequired();
            //builder.Property(c => c.Value).HasMaxLength(50).HasAnnotation("MinLength", 3).IsRequired().IsRequired();
        }
    }
}
