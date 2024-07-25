using Application.Services.Repositories;
using Core.Persistence.Repositories;
using Domain.Entities;
using Persistence.Contexts;

namespace Persistence.Repositories
{
    public class VisitRepository : EfRepositoryBase<Visit, Guid, BaseDbContext>, IVisitRepository
    {
        public VisitRepository(BaseDbContext context) : base(context)
        {
        }
    }
}
