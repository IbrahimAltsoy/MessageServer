using Core.Persistence.Repositories;
using Domain.Entities;

namespace Application.Services.Repositories
{
    public interface ISmsRepository : IAsyncRepository<Sms, Guid>, IRepository<Sms, Guid>
    {
    }
}
