using Core.Persistence.Repositories;
using S=Domain.Entities;

namespace Application.Services.Repositories
{
    public interface ISmsSettingRepository : IAsyncRepository<S.SmsSettings, Guid>, IRepository<S.SmsSettings, Guid>
    {
    }
}
