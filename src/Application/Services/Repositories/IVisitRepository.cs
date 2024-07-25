using Core.Persistence.Repositories;
using Domain.Entities;

namespace Application.Services.Repositories
{
    public interface IVisitRepository : IAsyncRepository<Visit, Guid>, IRepository<Visit, Guid> { }
}
