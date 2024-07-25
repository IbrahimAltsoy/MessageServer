using Core.Persistence.Repositories;
using Domain.Entities;

namespace Application.Services.Repositories
{
    public interface IVisitHistoryRepository : IAsyncRepository<VisitHistory, Guid>, IRepository<VisitHistory, Guid> { }
}
