using Core.Persistence.Repositories;
using Domain.Entities;

namespace Application.Services.Repositories
{
    public interface IMembershipRepository : IAsyncRepository<Membership, Guid>, IRepository<Membership, Guid> {}
}
