using Application.Services.Repositories;
using Core.Persistence.Repositories;
using Domain.Entities;
using Persistence.Contexts;

namespace Persistence.Repositories
{
    public class MembershipPackageRepository : EfRepositoryBase<MembershipPackage, Guid, BaseDbContext>, IMembershipPackageRepository    {
        public MembershipPackageRepository(BaseDbContext context) : base(context)
        {
        }
    }
}
