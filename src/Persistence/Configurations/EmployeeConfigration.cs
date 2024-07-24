using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Persistence.Configurations
{
    public class EmployeeConfigration : IEntityTypeConfiguration<Employee>
    {
        public void Configure(EntityTypeBuilder<Employee> builder)
        {
            builder.Property(e=>e.Name).HasMaxLength(50).HasAnnotation("MinLength", 2).IsRequired();
            builder.Property(e=>e.Surname).HasMaxLength(50);
            builder.Property(e=>e.Phone).HasMaxLength(11).HasAnnotation("MinLength", 10).IsRequired();
            builder.Property(e=>e.Role).HasMaxLength(50);

            builder.HasQueryFilter(p => p.DeletedDate == null);
        }
    }
}
