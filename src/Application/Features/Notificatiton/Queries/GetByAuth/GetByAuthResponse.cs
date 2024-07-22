using Core.Application.Responses;
using Domain.Enums;

namespace Application.Features.Notificatiton.Queries.GetByAuth;

public class GetByAuthResponse : IResponse
{
    public Guid Id { get; set; }
    public Guid UserId { get; set; }
    public NotificationType NotificationType { get; set; }
    public string Title { get; set; }
    public string Content { get; set; }
    public string Url { get; set; }
    public bool Read { get; set; }
    public DateTime CreatedDate { get; set; }
}