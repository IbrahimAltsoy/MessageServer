using Application.Services.Repositories;
using Core.Persistence.Repositories;
using Domain.Entities;
using Persistence.Contexts;

namespace Persistence.Repositories
{
    public class AppSettingRepository : EfRepositoryBase<AppSetting, Guid, BaseDbContext>, IAppSettingRepository
    {
        public AppSettingRepository(BaseDbContext context) : base(context)
        {
        }
    }
}
