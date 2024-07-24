using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Persistence.Configurations
{
    public class VisitHistoryConfigration : IEntityTypeConfiguration<VisitHistory>
    {
        public void Configure(EntityTypeBuilder<VisitHistory> builder)
        {
            builder.HasQueryFilter(p => p.DeletedDate == null);
        }
    }
}
