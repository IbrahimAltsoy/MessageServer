using Application.Services.Repositories;
using Core.Persistence.Repositories;
using S=Domain.Entities;
using Persistence.Contexts;

namespace Persistence.Repositories
{
    public class SmsSettingsRepository : EfRepositoryBase<S.SmsSettings, Guid, BaseDbContext>, ISmsSettingRepository
    {
        public SmsSettingsRepository(BaseDbContext context) : base(context)
        {
        }
    }
}
