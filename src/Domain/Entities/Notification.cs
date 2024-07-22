using Core.Persistence.Repositories;
using Domain.Enums;

namespace Domain.Entities;

public class Notification : Entity<Guid>
{
    public Guid UserId { get; set; }
    public NotificationType NotificationType { get; set; }
    public string Title { get; set; }
    public string Content { get; set; }
    public string Url { get; set; }
    public bool Read { get; set; }

    public virtual User? User { get; set; }

    public Notification()
    {
        Title = string.Empty;
        Content = string.Empty;
        Url = string.Empty;
        Read = false;
    }

    public Notification(Guid userId, NotificationType notification, string title, string content, string url)
    {
        UserId = userId;
        NotificationType = notification;
        Title = title;
        Content = content;
        Url = url;
    }

    public Notification(Guid id, Guid userId, NotificationType notification, string title, string content, string url) : base(id) 
    {
        UserId = userId;
        NotificationType = notification;
        Title = title;
        Content = content;
        Url = url;
    }
}
