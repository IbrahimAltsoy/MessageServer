using Application.Services.Repositories;
using Domain.Entities;
using Domain.Enums;

namespace Application.Services.NotificationService;

public class NotificationManager : INotificationService
{
    private readonly INotificationRepository _notificationRepository;

    public NotificationManager(INotificationRepository notificationRepository)
    {
        _notificationRepository = notificationRepository;
    }

    public async Task AddNotification(Notification notification)
    {
        await _notificationRepository.AddAsync(notification);
    }

    public async Task AddConnectionRequestNotification(Guid userId, string name)
    {
        Notification nt = new()
        {
            UserId = userId,
            NotificationType = NotificationType.ConntectionRequest,
            Title = "Yeni iletişim talebi aldınız",
            Content = $"{name} iletişim talebinde bulundu"
        };
        await AddNotification(nt);
    }

    public async Task DeleteNotification(Notification notification)
    {
        await _notificationRepository.DeleteAsync(notification);
    }

    public async Task<ICollection<Notification>> GetNotificationsAsync(Guid userId)
    {
        var list = await _notificationRepository.GetListAsync(predicate: n => n.UserId == userId);
        return list;
    }

    public async Task UpdateNotification(Notification notification)
    {
        await _notificationRepository.UpdateAsync(notification);
    }

    public async Task ReadNotification(Guid id)
    {
        Notification? notification = await _notificationRepository.GetAsync(n => n.Id == id);
        // TODO: Business rules
        if (notification is not null)
        {
            if (notification.Read == false)
            {
                notification.Read = true;
                await _notificationRepository.UpdateAsync(notification);
            }
        }

    }

    public async Task ReadAllNotification(Guid userId)
    {
        var notifications = await _notificationRepository.GetListAsync(n => n.UserId == userId);
        foreach (var notification in notifications)
        {
            if (!notification.Read)
                notification.Read = true;
        }

        await _notificationRepository.UpdateRangeAsync(notifications);
    }

    public async Task<Notification> GetById(Guid id)
    {
        Notification? notification = await _notificationRepository.GetAsync(n => n.Id == id);
        // TODO: BusinessRules
        return notification;
    }

    public async Task DeleteUserNotifications(Guid userId)
    {
        var notifications = await _notificationRepository.GetListAsync(n => n.UserId == userId);

        await _notificationRepository.DeleteRangeAsync(notifications, true);
    }
}
