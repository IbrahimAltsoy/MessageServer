using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Persistence.Configurations
{
    public class ReportConfigration : IEntityTypeConfiguration<Report>
    {
        public void Configure(EntityTypeBuilder<Report> builder)
        {
            builder.Property(e => e.Title).HasMaxLength(100);

            builder.HasQueryFilter(p => p.DeletedDate == null);
        }
    }
}
