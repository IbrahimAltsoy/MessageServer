using Core.Persistence.Repositories;
using Domain.Entities;

namespace Application.Services.Repositories
{
    public interface IMembershipPackageRepository : IAsyncRepository<MembershipPackage, Guid>, IRepository<MembershipPackage, Guid>{}
}
