using Application.Services.Repositories;
using Core.Persistence.Repositories;
using Domain.Entities;
using Persistence.Contexts;

namespace Persistence.Repositories
{
    public class NotificationSettingsRepository : EfRepositoryBase<NotificationSettings, Guid, BaseDbContext>, INotificationSettingsRepository
    {
        public NotificationSettingsRepository(BaseDbContext context) : base(context)
        {
        }
    }
}
