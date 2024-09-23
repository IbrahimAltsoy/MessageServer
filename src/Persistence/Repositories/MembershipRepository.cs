using Application.Services.Repositories;
using Core.Persistence.Repositories;
using Domain.Entities;
using Persistence.Contexts;

namespace Persistence.Repositories
{
    public class MembershipRepository : EfRepositoryBase<Membership, Guid, BaseDbContext>, IMembershipRepository
    {
        public MembershipRepository(BaseDbContext context) : base(context)
        {
        }
    }
}
