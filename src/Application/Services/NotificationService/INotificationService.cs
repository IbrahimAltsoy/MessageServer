using Domain.Entities;

namespace Application.Services.NotificationService;

public interface INotificationService
{
    public Task<Notification> GetById(Guid id);
    public Task<ICollection<Notification>> GetNotificationsAsync(Guid userId);
    public Task AddNotification(Notification notification);
    public Task DeleteNotification(Notification notification);
    public Task DeleteUserNotifications(Guid userId);
    public Task UpdateNotification(Notification notification);
    public Task AddConnectionRequestNotification(Guid userId, string name);
    public Task ReadNotification(Guid id);
    public Task ReadAllNotification(Guid userId);
}
