using Core.Persistence.Repositories;
using Domain.Entities;

namespace Application.Services.Repositories
{
    public interface IReportRepository : IAsyncRepository<Report, Guid>, IRepository<Report, Guid> { }
}
