using Application.Services.Repositories;
using Core.Persistence.Repositories;
using Domain.Entities;
using Persistence.Contexts;

namespace Persistence.Repositories
{
    public class SmsRepository : EfRepositoryBase<Sms, Guid, BaseDbContext>, ISmsRepository
    {
        public SmsRepository(BaseDbContext context) : base(context)
        {
        }
    }
}
