using Core.Persistence.Repositories;
using Domain.Entities;

namespace Application.Services.Repositories
{
    public interface IAppSettingRepository : IAsyncRepository<AppSetting, Guid>, IRepository<AppSetting, Guid>
    {
    }

}
