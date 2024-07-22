using Core.Persistence.Repositories;
using Domain.Entities;

namespace Application.Services.Repositories;

public interface INotificationRepository : IRepository<Notification, Guid>, IAsyncRepository<Notification, Guid>
{
}
