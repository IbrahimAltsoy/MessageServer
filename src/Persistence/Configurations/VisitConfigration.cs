using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Persistence.Configurations
{
    public class VisitConfigration : IEntityTypeConfiguration<Visit>
    {
        public void Configure(EntityTypeBuilder<Visit> builder)
        {
           builder.Property(e=>e.NameSurname).HasMaxLength(50);
            builder.Property(e=>e.Phone).HasMaxLength(11).HasAnnotation("MinLength", 10).IsRequired();
            builder.Property(e => e.Description).HasMaxLength(500);

            builder.HasQueryFilter(p => p.DeletedDate == null);
        }
    }
}
