using Application.Services.Repositories;
using Core.Persistence.Repositories;
using Domain.Entities;
using Persistence.Contexts;

namespace Persistence.Repositories
{
    public class ReportRepository : EfRepositoryBase<Report, Guid, BaseDbContext>, IReportRepository
    {
        public ReportRepository(BaseDbContext context) : base(context)
        {
        }
    }
}
