using Core.Persistence.Repositories;
using Domain.Entities;

namespace Application.Services.Repositories
{
    public interface INotificationSettingsRepository : IAsyncRepository<NotificationSettings, Guid>, IRepository<NotificationSettings, Guid> { }
}
