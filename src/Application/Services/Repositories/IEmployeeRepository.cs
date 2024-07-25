using Core.Persistence.Repositories;
using Domain.Entities;

namespace Application.Services.Repositories
{
    public interface IEmployeeRepository : IAsyncRepository<Employee, Guid>, IRepository<Employee, Guid> { }
    
}
