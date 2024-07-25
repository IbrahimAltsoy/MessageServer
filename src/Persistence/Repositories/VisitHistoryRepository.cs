using Application.Services.Repositories;
using Core.Persistence.Repositories;
using Domain.Entities;
using Persistence.Contexts;

namespace Persistence.Repositories
{
    public class VisitHistoryRepository : EfRepositoryBase<VisitHistory, Guid, BaseDbContext>, IVisitHistoryRepository
    {
        public VisitHistoryRepository(BaseDbContext context) : base(context)
        {
        }
    }
}
