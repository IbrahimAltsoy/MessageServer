using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Persistence.Configurations
{
    public class CustomerConfigration : IEntityTypeConfiguration<Customer>
    {
        public void Configure(EntityTypeBuilder<Customer> builder)
        {
            builder.Property(e => e.UserId).IsRequired();
            builder.Property(c => c.Name).HasMaxLength(50).HasAnnotation("MinLength", 3).IsRequired().IsRequired();
            builder.Property(c => c.Surname).HasMaxLength(50);
            builder.Property(c=>c.Phone).HasMaxLength(11).HasAnnotation("MinLength", 10).IsRequired();
            builder.Property(e=>e.Description).HasMaxLength(500); 
            builder.HasQueryFilter(p => p.DeletedDate == null);
        }
    }
}
