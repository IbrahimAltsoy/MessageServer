using Application.Services.Repositories;
using Core.Persistence.Repositories;
using Domain.Entities;
using Persistence.Contexts;

namespace Persistence.Repositories
{
    public class EmployeeRepository : EfRepositoryBase<Employee, Guid, BaseDbContext>, IEmployeeRepository
    {
        public EmployeeRepository(BaseDbContext context) : base(context)
        {
        }
    }
}
